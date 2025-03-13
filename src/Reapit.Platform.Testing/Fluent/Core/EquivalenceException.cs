namespace Reapit.Platform.Testing.Fluent.Core;

/// <summary>Representation of an equivalence assertion failure.</summary>
public class EquivalenceException : XunitException
{
    /// <summary>Initializes a new instance of the <see cref="EquivalenceException"/> class.</summary>
    /// <param name="message">The message that describes the error.</param>
    public EquivalenceException(string message) : base(message)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="EquivalenceException"/> class.</summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public EquivalenceException(string message, Exception? innerException) : base(message, innerException)
    {
    }

    internal static EquivalenceException ForExceededDepth(int depth, string member)
        => new($"Exceeded the maximum depth {depth} ('{member}'); check for infinite recursion or circular reference");

    internal static EquivalenceException ForMemberMismatch(object? expected, object? actual, string member)
        => ForMemberMismatch(expected, actual, member, null);

    internal static EquivalenceException ForMemberMismatch(object? expected, object? actual, string member, Exception? innerException)
        => new($"Expected {member} to be {expected.Format()} but found {actual.Format()}.", innerException);

    internal static EquivalenceException ForCircularReference(string member)
        => new($"Circular reference found in '{member}'.");

    internal static EquivalenceException ForTypeMismatch(Type expected, Type actual, string member)
        => new($"Type mismatch for '{member}' (Expected {expected.Name}, Actual {actual.Name})");

    internal static EquivalenceException ForGroupingValueMismatch(object? expected, object? actual, string key)
        => new($"Key [{key}] contains '{actual.Format()}' but expected '{expected.Format()}'");

    internal static EquivalenceException ForMemberListMismatch(IEnumerable<string> missingKeys)
        => new($"Expected keys not found: {missingKeys.Format()}");

    internal static EquivalenceException ForCollectionValueMissing(object? missing)
        => new($"Value not found in collection: {missing.Format()}.");

    internal static EquivalenceException ForExtraCollectionValue(IEnumerable<object?> extra)
        => new($"Unexpected values in collection: {extra.Format()}.");
}