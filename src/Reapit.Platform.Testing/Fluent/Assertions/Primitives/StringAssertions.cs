using Reapit.Platform.Testing.Fluent.Assertions.Abstract;
using Reapit.Platform.Testing.Fluent.Core;
using Reapit.Platform.Testing.Fluent.Failures;

namespace Reapit.Platform.Testing.Fluent.Assertions.Primitives;

/// <summary>Contains assertions for strings.</summary>
/// <param name="subject">The assertion subject.</param>
[DebuggerNonUserCode]
public class NullableStringAssertions(string? subject) : NullableStringAssertions<NullableStringAssertions>(subject);

/// <summary>Contains assertions for strings.</summary>
/// <param name="subject">The assertion subject.</param>
/// <typeparam name="TAssertions">The type of assertions to return in continuation objects.</typeparam>
[DebuggerNonUserCode]
public class NullableStringAssertions<TAssertions>(string? subject)
    : NullableReferenceTypeAssertions<string, TAssertions>(subject)
    where TAssertions : NullableStringAssertions<TAssertions>
{
    /// <inheritdoc />
    protected override string Context => "string";

    /// <summary>Asserts that the subject is equal to the <paramref name="compareTo"/> value.</summary>
    /// <param name="compareTo">The value against which the subject is compared.</param>
    public AndOperator<TAssertions> Be(string compareTo)
    {
        if (compareTo.Equals(Subject))
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetContextData("expected", compareTo)
            .SetMessageTemplate("Expected {context} to be {expected}, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject is not equal to the <paramref name="compareTo"/> value.</summary>
    /// <param name="compareTo">The value against which the subject is compared.</param>
    public AndOperator<TAssertions> NotBe(string compareTo)
    {
        if (!compareTo.Equals(Subject))
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetContextData("expected", compareTo)
            .SetMessageTemplate("Expected {context} not to be {expected}, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject is equivalent to the <paramref name="compareTo"/> value.</summary>
    /// <param name="compareTo">The value against which the subject is compared.</param>
    public AndOperator<TAssertions> BeEquivalentTo(string compareTo)
    {
        if (compareTo.Equals(Subject, StringComparison.OrdinalIgnoreCase))
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetContextData("expected", compareTo)
            .SetMessageTemplate("Expected {context} to be equivalent to {expected}, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject is not equivalent to the <paramref name="compareTo"/> value.</summary>
    /// <param name="compareTo">The value against which the subject is compared.</param>
    public AndOperator<TAssertions> NotBeEquivalentTo(string compareTo)
    {
        if (!compareTo.Equals(Subject, StringComparison.OrdinalIgnoreCase))
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetContextData("expected", compareTo)
            .SetMessageTemplate("Expected {context} not to be equivalent to {expected}, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject is an empty string.</summary>
    public AndOperator<TAssertions> BeEmpty()
    {
        if (Subject?.Length == 0)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetMessageTemplate("Expected {context} to be empty, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject is not an empty string.</summary>
    public AndOperator<TAssertions> NotBeEmpty()
    {
        if (Subject is null || Subject.Length > 0)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetMessageTemplate("Expected {context} to be empty, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject is of the expected length.</summary>
    /// <param name="expected">The expected string length.</param>
    public AndOperator<TAssertions> HaveLength(int expected)
    {
        if (Subject?.Length == expected)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("subject", Subject)
            .SetContextData("expected", expected)
            .SetContextData("actual", Subject?.Length ?? 0)
            .SetMessageTemplate("Expected {context} to contain {expected} characters, but found {actual} characters.")
            .Build();
    }

    /// <summary>Asserts that the subject is null or empty.</summary>
    public AndOperator<TAssertions> BeNullOrEmpty()
    {
        if (string.IsNullOrEmpty(Subject))
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetMessageTemplate("Expected {context} to be null or empty, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject neither null nor empty.</summary>
    public AndOperator<TAssertions> NotBeNullOrEmpty()
    {
        if (!string.IsNullOrEmpty(Subject))
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetMessageTemplate("Expected {context} not to be null or empty, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject is null, empty, or consists only of whitespace characters.</summary>
    public AndOperator<TAssertions> BeNullOrWhiteSpace()
    {
        if (string.IsNullOrWhiteSpace(Subject))
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetMessageTemplate("Expected {context} to be null, empty, or whitespace, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject not null, not empty, and does not consist solely of whitespace characters.</summary>
    public AndOperator<TAssertions> NotBeNullOrWhiteSpace()
    {
        if (!string.IsNullOrWhiteSpace(Subject))
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetMessageTemplate("Expected {context} not to be null, empty, or whitespace, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject starts with <paramref name="compareTo"/>.</summary>
    /// <param name="compareTo">The value against which the subject is compared.</param>
    public AndOperator<TAssertions> StartWith(string compareTo)
        => StartWith(compareTo, StringComparison.Ordinal);

    /// <summary>Asserts that the subject starts with <paramref name="compareTo"/>.</summary>
    /// <param name="compareTo">The value against which the subject is compared.</param>
    /// <param name="comparison">The comparison ruleset to apply.</param>
    public AndOperator<TAssertions> StartWith(string compareTo, StringComparison comparison)
    {
        if (Subject is not null && Subject.StartsWith(compareTo, comparison))
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", ReadFromStart(Subject, compareTo.Length))
            .SetContextData("expected", compareTo)
            .SetContextData("subject", Subject)
            .SetMessageTemplate("Expected {context} to start with {expected}, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject does not start with <paramref name="compareTo"/>.</summary>
    /// <param name="compareTo">The value against which the subject is compared.</param>
    public AndOperator<TAssertions> NotStartWith(string compareTo)
        => NotStartWith(compareTo, StringComparison.Ordinal);

    /// <summary>Asserts that the subject does not start with <paramref name="compareTo"/>.</summary>
    /// <param name="compareTo">The value against which the subject is compared.</param>
    /// <param name="comparison">The comparison ruleset to apply.</param>
    public AndOperator<TAssertions> NotStartWith(string compareTo, StringComparison comparison)
    {
        if (Subject is null || !Subject.StartsWith(compareTo, comparison))
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", ReadFromStart(Subject, compareTo.Length))
            .SetContextData("expected", compareTo)
            .SetContextData("subject", Subject)
            .SetMessageTemplate("Expected {context} not to start with {expected}, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject ends with <paramref name="compareTo"/>.</summary>
    /// <param name="compareTo">The value against which the subject is compared.</param>
    public AndOperator<TAssertions> EndWith(string compareTo)
        => EndWith(compareTo, StringComparison.Ordinal);

    /// <summary>Asserts that the subject ends with <paramref name="compareTo"/>.</summary>
    /// <param name="compareTo">The value against which the subject is compared.</param>
    /// <param name="comparison">The comparison ruleset to apply.</param>
    public AndOperator<TAssertions> EndWith(string compareTo, StringComparison comparison)
    {
        if (Subject is not null && Subject.EndsWith(compareTo, comparison))
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", ReadFromEnd(Subject, compareTo.Length))
            .SetContextData("expected", compareTo)
            .SetContextData("subject", Subject)
            .SetMessageTemplate("Expected {context} to end with {expected}, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject does not end with <paramref name="compareTo"/>.</summary>
    /// <param name="compareTo">The value against which the subject is compared.</param>
    public AndOperator<TAssertions> NotEndWith(string compareTo)
        => NotEndWith(compareTo, StringComparison.Ordinal);

    /// <summary>Asserts that the subject does not end with <paramref name="compareTo"/>.</summary>
    /// <param name="compareTo">The value against which the subject is compared.</param>
    /// <param name="comparison">The comparison ruleset to apply.</param>
    public AndOperator<TAssertions> NotEndWith(string compareTo, StringComparison comparison)
    {
        if (Subject is null || !Subject.EndsWith(compareTo, comparison))
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", ReadFromEnd(Subject, compareTo.Length))
            .SetContextData("expected", compareTo)
            .SetContextData("subject", Subject)
            .SetMessageTemplate("Expected {context} not to end with {expected}, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject contains <paramref name="compareTo"/>.</summary>
    /// <param name="compareTo">The value against which the subject is compared.</param>
    public AndOperator<TAssertions> Contain(string compareTo)
        => Contain(compareTo, StringComparison.Ordinal);

    /// <summary>Asserts that the subject contains <paramref name="compareTo"/>.</summary>
    /// <param name="compareTo">The value against which the subject is compared.</param>
    /// <param name="comparison">The comparison ruleset to apply.</param>
    public AndOperator<TAssertions> Contain(string compareTo, StringComparison comparison)
    {
        if (Subject is not null && Subject.Contains(compareTo, comparison))
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetContextData("expected", compareTo)
            .SetMessageTemplate("Expected {context} to contain {expected}, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject does not contain <paramref name="compareTo"/>.</summary>
    /// <param name="compareTo">The value against which the subject is compared.</param>
    public AndOperator<TAssertions> NotContain(string compareTo)
        => NotContain(compareTo, StringComparison.Ordinal);

    /// <summary>Asserts that the subject does not contain <paramref name="compareTo"/>.</summary>
    /// <param name="compareTo">The value against which the subject is compared.</param>
    /// <param name="comparison">The comparison ruleset to apply.</param>
    public AndOperator<TAssertions> NotContain(string compareTo, StringComparison comparison)
    {
        if (Subject is null || !Subject.Contains(compareTo, comparison))
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetContextData("expected", compareTo)
            .SetMessageTemplate("Expected {context} not to contain {expected}, but found {actual}.")
            .Build();
    }

    /*
     * Private methods
     */

    private static string? ReadFromStart(string? input, int characters)
        => input is not null && input.Length > characters ? input[..characters] : input;

    private static string? ReadFromEnd(string? input, int characters)
        => input is not null && input.Length > characters ? input[^characters..] : input;
}