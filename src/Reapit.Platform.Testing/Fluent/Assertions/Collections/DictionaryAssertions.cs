using Reapit.Platform.Testing.Fluent.Core;
using Reapit.Platform.Testing.Fluent.Failures;

namespace Reapit.Platform.Testing.Fluent.Assertions.Collections;

/// <summary>Contains assertions for a dictionary of values.</summary>
/// <param name="subject">The assertion subject.</param>
/// <typeparam name="TCollection">The dictionary type.</typeparam>
/// <typeparam name="TKey">The key type in the dictionary.</typeparam>
/// <typeparam name="TValue">The value type in the dictionary.</typeparam>
[DebuggerNonUserCode]
public class DictionaryAssertions<TCollection, TKey, TValue>(TCollection? subject)
    : DictionaryAssertions<TCollection, TKey, TValue, DictionaryAssertions<TCollection, TKey, TValue>>(subject)
    where TCollection : IEnumerable<KeyValuePair<TKey, TValue>>;

/// <summary>Contains assertions for a dictionary of values.</summary>
/// <param name="subject">The assertion subject.</param>
/// <typeparam name="TCollection">The type of enumerable supporting the dictionary.</typeparam>
/// <typeparam name="TKey">The key type in the dictionary.</typeparam>
/// <typeparam name="TValue">The value type in the dictionary.</typeparam>
/// <typeparam name="TAssertions">The type of assertions to return in continuation objects.</typeparam>
[DebuggerNonUserCode]
public class DictionaryAssertions<TCollection, TKey, TValue, TAssertions>(TCollection? subject)
    : CollectionAssertions<TCollection, KeyValuePair<TKey, TValue>, TAssertions>(subject)
    where TCollection : IEnumerable<KeyValuePair<TKey, TValue>>
    where TAssertions : DictionaryAssertions<TCollection, TKey, TValue, TAssertions>
{
    /// <inheritdoc />
    protected override string Context => "dictionary";

    /// <summary>Asserts that the subject contains the expected value pair.</summary>
    /// <param name="key">The expected key.</param>
    /// <param name="value">The expected value.</param>
    public AndOperator<TAssertions> Contain(TKey key, TValue value)
        => Contain(new KeyValuePair<TKey, TValue>(key, value));

    /// <summary>Asserts that the subject contains the expected value.</summary>
    /// <param name="expected">The expected value.</param>
    public override AndOperator<TAssertions> Contain(KeyValuePair<TKey, TValue> expected)
    {
        if (Subject is not null && SubjectContains(expected))
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetContextData("expected", expected)
            .SetMessageTemplate("Expected {context} to contain {expected}.")
            .Build();
    }

    /// <summary>Asserts that the subject does not contain the given value pair.</summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    public AndOperator<TAssertions> NotContain(TKey key, TValue value)
        => NotContain(new KeyValuePair<TKey, TValue>(key, value));

    /// <summary>Asserts that the subject does not contain the <paramref name="unexpected"/> value.</summary>
    /// <param name="unexpected">The unexpected value.</param>
    public override AndOperator<TAssertions> NotContain(KeyValuePair<TKey, TValue> unexpected)
    {
        if (Subject is null || !SubjectContains(unexpected))
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetContextData("expected", unexpected)
            .SetMessageTemplate("Expected {context} not to contain {expected}.")
            .Build();
    }

    /// <summary>Asserts that the dictionary contains a given key.</summary>
    /// <param name="expected">The key to lookup.</param>
    public AndOperator<TAssertions> ContainKey(TKey expected)
    {
        if (Subject is not null && GetKeys().Contains(expected))
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("expected", expected)
            .SetContextData("keys", Subject is null ? null : GetKeys())
            .SetMessageTemplate("Expected {context} to contain key {expected}, but found {keys}.")
            .Build();
    }

    /// <summary>Asserts that the dictionary does not contain a given key.</summary>
    /// <param name="expected">The key to lookup.</param>
    public AndOperator<TAssertions> NotContainKey(TKey expected)
    {
        if (Subject is null || !GetKeys().Contains(expected))
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("expected", expected)
            .SetContextData("keys", GetKeys())
            .SetMessageTemplate("Expected {context} to not to contain key {expected}, but found {keys}.")
            .Build();
    }

    /// <summary>Asserts that the dictionary contains a given value.</summary>
    /// <param name="expected">The value to lookup.</param>
    public AndOperator<TAssertions> ContainValue(TValue expected)
    {
        if (Subject is not null && GetValues().Contains(expected))
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("expected", expected)
            .SetContextData("keys", Subject is null ? null : GetKeys())
            .SetMessageTemplate("Expected {context} to contain value {expected}, but found {keys}.")
            .Build();
    }

    /// <summary>Asserts that the dictionary does not contain a given value.</summary>
    /// <param name="expected">The value to lookup.</param>
    public AndOperator<TAssertions> NotContainValue(TValue expected)
    {
        if (Subject is null || !GetValues().Contains(expected))
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("expected", expected)
            .SetContextData("keys", GetKeys())
            .SetMessageTemplate("Expected {context} to not to contain value {expected}, but found {keys}.")
            .Build();
    }

    /*
     * Private methods
     */

    private IEnumerable<TKey> GetKeys()
        => Subject?.Select(c => c.Key) ?? [];

    private IEnumerable<TValue> GetValues()
            => Subject?.Select(c => c.Value) ?? [];

    private bool SubjectContains(KeyValuePair<TKey, TValue> value)
    {
        if (Subject is null)
            return false;

        foreach (var pair in Subject)
        {
            var keyEqual = Equals(pair.Key, value.Key);
            var valueEqual = Equals(pair.Value, value.Value);
            if (keyEqual && valueEqual)
                return true;
        }

        return false;
    }
}