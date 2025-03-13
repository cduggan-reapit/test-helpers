using System.Collections;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;

namespace Reapit.Platform.Testing.Fluent.Core;

/// <summary>Helper methods for evaluating object equivalence.</summary>
public static class EquivalenceAnalyzer
{
    private const int MaxCompareDepth = 10;

    private static readonly ConcurrentDictionary<Type, Dictionary<string, Func<object?, object?>>> TypeAccessors = new();

    private static readonly Lazy<Assembly[]> AppDomainAssemblies = new(AppDomain.CurrentDomain.GetAssemblies);

    private static readonly Lazy<Type?> FileSystemInfoType = new(() => GetTypeByName("System.IO.FileSystemInfo"));

    private static readonly Lazy<PropertyInfo?> FileSystemInfoFullNameProperty = new(() => FileSystemInfoType.Value?.GetProperty("FullName"));

    /// <summary>Verify the equivalence of two objects.</summary>
    /// <param name="expected">The expected object.</param>
    /// <param name="actual">The actual object.</param>
    /// <param name="strict">Flag indicating whether strict equivalence is required.</param>
    /// <returns></returns>
    internal static EquivalenceException? VerifyEquivalence(object? expected, object? actual, bool strict)
        => VerifyEquivalence(expected, actual, strict, string.Empty, [], [], 1);

    /// <summary>Verify the equivalence of two objects.</summary>
    /// <param name="expected">The expected object.</param>
    /// <param name="actual">The actual object.</param>
    /// <param name="strict">Flag indicating whether strict equivalence is required.</param>
    /// <param name="prefix">Path describing the current evaluations location within a nested object.</param>
    /// <param name="expectedReferences">The objects in the expected object which have already been analyzed.</param>
    /// <param name="actualReferences">The objects in the actual object which have already been analyzed.</param>
    /// <param name="depth">The depth of the current test in the nested object.</param>
    internal static EquivalenceException? VerifyEquivalence(
        object? expected,
        object? actual,
        bool strict,
        string prefix,
        HashSet<object> expectedReferences,
        HashSet<object> actualReferences,
        int depth)
    {
        // This throws - it's an unrecoverable breakout and renders other checks moot.  Same for circular references.
        if (depth > MaxCompareDepth)
            throw EquivalenceException.ForExceededDepth(MaxCompareDepth, prefix);

        expected = UnwrapLazy(expected, out var expectedType);
        actual = UnwrapLazy(actual, out var actualType);

        if (expected == null)
            return actual == null ? null : EquivalenceException.ForMemberMismatch(expected, actual, prefix);

        if (actual == null)
            return EquivalenceException.ForMemberMismatch(expected, actual, prefix);

        if (ReferenceEquals(expected, actual))
            return null;

        if (expectedReferences.Contains(expected))
            throw EquivalenceException.ForCircularReference($"{nameof(expected)}.{prefix}");

        if (actualReferences.Contains(actual))
            throw EquivalenceException.ForCircularReference($"{nameof(actual)}.{prefix}");

        try
        {
            // Now we actually compare them...
            expectedReferences.Add(expected);
            actualReferences.Add(actual);
            return AnalyzeEquivalence(expectedType, expected, actualType, actual, strict, prefix, expectedReferences, actualReferences, depth);
        }
        finally
        {
            expectedReferences.Remove(expected);
            actualReferences.Remove(actual);
        }
    }

    private static EquivalenceException? AnalyzeEquivalence(
        Type expectedType,
        object expected,
        Type actualType,
        object actual,
        bool strict,
        string prefix,
        HashSet<object> expectedReferences,
        HashSet<object> actualReferences,
        int depth)
    {
        if (IsPrimitive(expectedType))
            return AnalyzeIntrinsicEquivalence(expected, actual, prefix);

        if (IsDateTime(expectedType))
            return AnalyzeDateTimeEquivalence(expected, actual, prefix);

        if (IsFileSystemInfoType(expectedType) && IsFileSystemInfoType(actualType))
            return AnalyzeFileSystemInfoEquivalence(expectedType, expected, actualType, actual, strict, prefix, expectedReferences, actualReferences, depth);

        if (expected is Uri expectedUri && actual is Uri actualUri)
            return AnalyzeUriEquivalence(expectedUri, actualUri, prefix);

        // IGrouping<TKey, TValue>
        var expectedGroupingTypes = GetGroupingTypes(expected);
        var actualGroupingTypes = GetGroupingTypes(actual);
        if (expectedGroupingTypes != null && actualGroupingTypes != null)
            return AnalyzeGroupingEquivalence(expected, expectedGroupingTypes, actual, actualGroupingTypes, strict);

        // IEnumerable
        if (expected is IEnumerable enumerableExpected && actual is IEnumerable enumerableActual)
            return AnalyzeEnumerableEquivalence(enumerableExpected, enumerableActual, strict, expectedReferences, actualReferences, depth);

        return AnalyzeReferenceEquivalence(expected, actual, strict, prefix, expectedReferences, actualReferences, depth);
    }

    /*
     * Helpers
     */

    private static object? UnwrapLazy(object? value, out Type valueType)
    {
        if (value == null)
        {
            valueType = typeof(object);
            return null;
        }

        valueType = value.GetType();

        // If it's not Lazy<T>, return it
        if (!valueType.IsGenericType || valueType.GetGenericTypeDefinition() != typeof(Lazy<>))
            return value;

        // If it is, but we can't get the value out of it, return it as-is
        var property = valueType.GetRuntimeProperty("Value");
        if (property == null)
            return value;

        // Otherwise unwrap it
        valueType = valueType.GenericTypeArguments.First();
        return property.GetValue(value);
    }

    private static bool IsPrimitive(Type type)
        => type.IsPrimitive
           || type.IsEnum
           || type == typeof(string)
           || type == typeof(decimal)
           || type == typeof(Guid);

    private static bool IsDateTime(Type type) =>
        type == typeof(DateTime) || type == typeof(DateTimeOffset);

    private static bool IsFileSystemInfoType(Type type)
        => FileSystemInfoType.Value != null && FileSystemInfoType.Value.IsAssignableFrom(type);

    private static bool TryConvert(object value, Type targetType, [NotNullWhen(true)] out object? converted)
    {
        try
        {
            converted = Convert.ChangeType(value, targetType, CultureInfo.CurrentCulture);
            return true;
        }
        catch (Exception ex) when (ex is InvalidCastException or FormatException)
        {
            converted = null;
            return false;
        }
    }

    private static Type[]? GetGroupingTypes(object? obj)
        => obj?.GetType()
            .GetInterfaces()
            .Where(i => i.IsGenericType)
            .Select(i => i.GetGenericTypeDefinition())
            .FirstOrDefault(gt => gt == typeof(IGrouping<,>))
            ?.GenericTypeArguments;

    private static Dictionary<string, Func<object?, object?>> GetAccessorsForType(Type type)
        => TypeAccessors.GetOrAdd(type, subject =>
        {
            var fields = subject.GetRuntimeFields()
                .Where(f => f is { IsPublic: true, IsStatic: false })
                .Select(f => new { name = f.Name, accessor = (Func<object?, object?>)f.GetValue });

            var properties = subject.GetRuntimeProperties()
                .Where(p => p.CanRead
                            && p.GetMethod != null
                            && p.GetMethod.IsPublic
                            && !p.GetMethod.IsStatic
                            && !p.GetMethod.ReturnType.IsByRefLike
                            && p.GetIndexParameters().Length == 0
                            && !p.GetCustomAttributes<ObsoleteAttribute>().Any()
                            && !p.GetMethod.GetCustomAttributes<ObsoleteAttribute>().Any())
                .Select(p => new { name = p.Name, accessor = (Func<object?, object?>)p.GetValue });

            return fields.Concat(properties).ToDictionary(g => g.name, g => g.accessor);
        });

    private static Type? GetTypeByName(string name)
    {
        try
        {
            foreach (var assembly in AppDomainAssemblies.Value)
            {
                var type = assembly.GetType(name);
                if (type != null)
                    return type;
            }

            return null;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Unable to find type: {name}", ex);
        }
    }

    private static int? FindObjectInCollection(
        object? expectedValue,
        List<object?> actualValues,
        bool strict,
        HashSet<object> expectedReferences,
        HashSet<object> actualReferences,
        int depth)
    {
        var actualIndex = 0;
        for (; actualIndex < actualValues.Count; ++actualIndex)
        {
            if (VerifyEquivalence(expectedValue, actualValues[actualIndex], strict, string.Empty, expectedReferences, actualReferences, depth) == null)
                return actualIndex;
        }

        return null;
    }

    /*
     * Analyzers
     */

    private static EquivalenceException? AnalyzeIntrinsicEquivalence(object expected, object actual, string prefix)
    {
        var result = expected.Equals(actual);

        // If they're not equal, try converting expectation to type of actual and comparing those
        if (!result && TryConvert(expected, actual.GetType(), out var converted))
            result = converted.Equals(actual);

        // If they're still not equal, try converting actual to type of expectation and comparing those
        if (!result && TryConvert(actual, expected.GetType(), out converted))
            result = converted.Equals(expected);

        return result ? null : EquivalenceException.ForMemberMismatch(expected, actual, prefix);
    }

    private static EquivalenceException? AnalyzeDateTimeEquivalence(object expected, object actual, string prefix)
    {
        try
        {
            if (expected is IComparable expectedComparable)
            {
                return expectedComparable.CompareTo(actual) == 0
                    ? null
                    : EquivalenceException.ForMemberMismatch(expected, actual, prefix);
            }

            if (actual is IComparable actualComparable)
            {
                return actualComparable.CompareTo(expected) == 0
                    ? null
                    : EquivalenceException.ForMemberMismatch(expected, actual, prefix);
            }
        }
        catch (Exception ex)
        {
            return EquivalenceException.ForMemberMismatch(expected, actual, prefix, ex);
        }

        throw new InvalidOperationException($"Cannot analyze date-time equivalence for non-datetime objects (received: {expected.GetType().Name}, {actual.GetType().Name})");
    }

    private static EquivalenceException? AnalyzeFileSystemInfoEquivalence(
        Type expectedType,
        object expected,
        Type actualType,
        object actual,
        bool strict,
        string prefix,
        HashSet<object> expectedReferences,
        HashSet<object> actualReferences,
        int depth)
    {
        if (FileSystemInfoFullNameProperty.Value == null)
            throw new InvalidOperationException("Couldn't find 'FullName' on 'System.IO.FileSystemInfo'.");

        if (expectedType != actualType)
            return EquivalenceException.ForTypeMismatch(expectedType, actualType, prefix);

        var fullName = FileSystemInfoFullNameProperty.Value.GetValue(expected);
        var anonymousExpectation = new { FullName = fullName };

        return AnalyzeReferenceEquivalence(anonymousExpectation, actual, strict, prefix, expectedReferences, actualReferences, depth);

    }

    private static EquivalenceException? AnalyzeUriEquivalence(Uri expected, Uri actual, string prefix)
        => expected.OriginalString != actual.OriginalString
            ? EquivalenceException.ForMemberMismatch(expected, actual, prefix)
            : null;

    private static EquivalenceException? AnalyzeGroupingEquivalence(
        object expected,
        Type[] expectedGroupingTypes,
        object actual,
        Type[] actualGroupingTypes,
        bool strict)
    {
        var expectedKey = typeof(IGrouping<,>).MakeGenericType(expectedGroupingTypes).GetRuntimeProperty("Key")?.GetValue(expected);
        var actualKey = typeof(IGrouping<,>).MakeGenericType(actualGroupingTypes).GetRuntimeProperty("Key")?.GetValue(actual);

        var keyError = VerifyEquivalence(expectedKey, actualKey, strict: false);
        if (keyError != null)
            return keyError;

        var toArrayMethod = typeof(Enumerable).GetRuntimeMethods().FirstOrDefault(m => m is { IsStatic: true, IsPublic: true, Name: nameof(Enumerable.ToArray) } && m.GetParameters().Length == 1)
            ?? throw new InvalidOperationException("Could not find ToArray method.");

        var expectedToArray = toArrayMethod.MakeGenericMethod(expectedGroupingTypes[1]);
        var expectedValues = expectedToArray.Invoke(null, [expected]);

        var actualToArray = toArrayMethod.MakeGenericMethod(actualGroupingTypes[1]);
        var actualValues = actualToArray.Invoke(null, [actual]);

        if (VerifyEquivalence(expectedValues, actualValues, strict) != null)
            throw EquivalenceException.ForGroupingValueMismatch(expectedValues, actualValues, expectedKey?.ToString() ?? string.Empty);

        return null;
    }

    private static EquivalenceException? AnalyzeEnumerableEquivalence(
        IEnumerable expected,
        IEnumerable actual,
        bool strict,
        HashSet<object> expectedReferences,
        HashSet<object> actualReferences,
        int depth)
    {
        var expectedValues = expected.Cast<object?>().ToList();
        var actualValues = actual.Cast<object?>().ToList();

        foreach (var expectedValue in expectedValues)
        {
            var matchIndex = FindObjectInCollection(expectedValue, actualValues, strict, expectedReferences, actualReferences, depth);
            if (matchIndex is null)
                return EquivalenceException.ForCollectionValueMissing(expectedValue);

            actualValues.RemoveAt(matchIndex.Value);
        }

        if (strict && actualValues.Count != 0)
            return EquivalenceException.ForExtraCollectionValue(actualValues);

        return null;
    }

    private static EquivalenceException? AnalyzeReferenceEquivalence(
        object expected,
        object actual,
        bool strict,
        string prefix,
        HashSet<object> expectedReferences,
        HashSet<object> actualReferences,
        int depth)
    {
        var formattedPrefix = prefix.Length == 0 ? string.Empty : $"{prefix}.";

        var expectedAccessors = GetAccessorsForType(expected.GetType());
        var actualAccessors = GetAccessorsForType(actual.GetType());

        if (strict && expectedAccessors.Count != actualAccessors.Count)
            return EquivalenceException.ForMemberListMismatch(expectedAccessors.Keys.Except(actualAccessors.Keys));

        foreach (var kvp in expectedAccessors)
        {
            if (!actualAccessors.TryGetValue(kvp.Key, out var actualAccessor))
                return EquivalenceException.ForMemberListMismatch([kvp.Key]);

            var expectedValue = kvp.Value(expected);
            var actualValue = actualAccessor(actual);

            var ex = VerifyEquivalence(expectedValue, actualValue, strict, $"{formattedPrefix}{kvp.Key}", expectedReferences, actualReferences, depth + 1);
            if (ex != null)
                return ex;
        }

        return null;
    }
}