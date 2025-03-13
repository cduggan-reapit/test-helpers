using Reapit.Platform.Testing.Fluent.Core;
using Reapit.Platform.Testing.Fluent.Failures;

namespace Reapit.Platform.Testing.Fluent.Assertions.Primitives;

/// <summary>Contains assertions for Boolean types.</summary>
/// <param name="subject">The assertion subject.</param>
[DebuggerNonUserCode]
public class BooleanAssertions(bool subject) : NullableBooleanAssertions<BooleanAssertions>(subject);

/// <summary>Contains assertions for nullable Boolean types.</summary>
/// <param name="subject">The assertion subject.</param>
[DebuggerNonUserCode]
public class NullableBooleanAssertions(bool? subject) : NullableBooleanAssertions<NullableBooleanAssertions>(subject);

/// <summary>Contains assertions for nullable Boolean types.</summary>
/// <typeparam name="TAssertions">The type of assertions to return in continuation objects.</typeparam>
/// <param name="subject">The assertion subject.</param>
[DebuggerNonUserCode]
public class NullableBooleanAssertions<TAssertions>(bool? subject)
    where TAssertions : NullableBooleanAssertions<TAssertions>
{
    /// <summary>The assertion context name.</summary>
    private string Context { get; } = "boolean";

    /// <summary>The subject of assertions.</summary>
    private bool? Subject { get; } = subject;

    /// <summary>Asserts that the subject is not <see langword="null"/>.</summary>
    public AndOperator<TAssertions> HaveValue()
    {
        if (Subject.HasValue)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetMessageTemplate("Expected {context} to have a value, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject is <see langword="null"/>.</summary>
    public AndOperator<TAssertions> NotHaveValue()
    {
        if (!Subject.HasValue)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetMessageTemplate("Expected {context} not to have a value, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject is not <see langword="null"/>.</summary>
    public AndOperator<TAssertions> BeNull()
        => NotHaveValue();

    /// <summary>Asserts that the subject is <see langword="null"/>.</summary>
    public AndOperator<TAssertions> NotBeNull()
        => HaveValue();

    /// <summary>Asserts that the subject has the same value as <paramref name="compareTo"/>.</summary>
    /// <param name="compareTo">The value against which the subject is compared.</param>
    public AndOperator<TAssertions> Be(bool compareTo)
    {
        if (Subject == compareTo)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("expected", compareTo)
            .SetContextData("actual", Subject)
            .SetMessageTemplate("Expected {context} to be {expected}, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject has the same value as <paramref name="compareTo"/>.</summary>
    /// <param name="compareTo">The value against which the subject is compared.</param>
    public AndOperator<TAssertions> Be(bool? compareTo)
    {
        if (Subject is null && compareTo is null)
            return new AndOperator<TAssertions>((TAssertions)this);

        if (Subject == compareTo)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("expected", compareTo)
            .SetContextData("actual", Subject)
            .SetMessageTemplate("Expected {context} to be {expected}, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject has the same value as <paramref name="compareTo"/>.</summary>
    /// <param name="compareTo">The value against which the subject is compared.</param>
    public AndOperator<TAssertions> NotBe(bool compareTo)
    {
        if (Subject != compareTo)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("expected", compareTo)
            .SetContextData("actual", Subject)
            .SetMessageTemplate("Expected {context} not to be {expected}, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject has the same value as <paramref name="compareTo"/>.</summary>
    /// <param name="compareTo">The value against which the subject is compared.</param>
    public AndOperator<TAssertions> NotBe(bool? compareTo)
    {
        if (compareTo is { } value ? Subject?.CompareTo(value) != 0 : Subject is not null)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("expected", compareTo)
            .SetContextData("actual", Subject)
            .SetMessageTemplate("Expected {context} not to be {expected}, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject is true.</summary>
    public AndOperator<TAssertions> BeTrue()
        => Be(true);

    /// <summary>Asserts that the subject is false.</summary>
    public AndOperator<TAssertions> BeFalse()
        => Be(false);
}