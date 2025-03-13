using Reapit.Platform.Testing.Fluent.Core;
using Reapit.Platform.Testing.Fluent.Failures;

namespace Reapit.Platform.Testing.Fluent.Assertions.Primitives;

/// <summary>Contains assertions for strings.</summary>
/// <param name="subject">The assertion subject.</param>
[DebuggerNonUserCode]
public class GuidAssertions(Guid? subject) : NullableGuidAssertions<GuidAssertions>(subject);

/// <summary>Contains assertions for guid values.</summary>
/// <param name="subject">The assertion subject.</param>
[DebuggerNonUserCode]
public class NullableGuidAssertions(Guid? subject) : NullableGuidAssertions<NullableGuidAssertions>(subject);

/// <summary>Contains assertions for nullable Guid types.</summary>
/// <typeparam name="TAssertions">The type of assertions to return in continuation objects.</typeparam>
/// <param name="subject">The assertion subject.</param>
[DebuggerNonUserCode]
public class NullableGuidAssertions<TAssertions>(Guid? subject)
    where TAssertions : NullableGuidAssertions<TAssertions>
{
    /// <summary>The assertion context name.</summary>
    protected string Context => "guid";

    /// <summary>The subject of assertions.</summary>
    public Guid? Subject { get; } = subject;

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

    /// <summary>Asserts that the subject is <see langword="null"/>.</summary>
    public AndOperator<TAssertions> BeNull()
        => NotHaveValue();

    /// <summary>Asserts that the subject is not <see langword="null"/>.</summary>
    public AndOperator<TAssertions> NotBeNull()
        => HaveValue();

    /// <summary>Asserts that the subject is equal to the <paramref name="compareTo"/> value.</summary>
    /// <param name="compareTo">The expected value.</param>
    public AndOperator<TAssertions> Be(string compareTo)
    {
        if (!Guid.TryParse(compareTo, out var compareToGuid))
            throw new ArgumentException($"\"{compareTo}\" is not a valid guid.", nameof(compareTo));

        return Be(compareToGuid);
    }

    /// <summary>Asserts that the subject is equal to the <paramref name="compareTo"/> value.</summary>
    /// <param name="compareTo">The expected value.</param>
    public AndOperator<TAssertions> Be(Guid compareTo)
    {
        if (Subject == compareTo)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("expected", compareTo)
            .SetContextData("actual", Subject)
            .SetMessageTemplate("Expected {context} to be {expected}, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject is not equal to as the <paramref name="compareTo"/> value.</summary>
    /// <param name="compareTo">The value against which the subject is compared.</param>
    public AndOperator<TAssertions> NotBe(string compareTo)
    {
        if (!Guid.TryParse(compareTo, out var compareToGuid))
            throw new ArgumentException($"\"{compareTo}\" is not a valid guid.", nameof(compareTo));

        return NotBe(compareToGuid);
    }

    /// <summary>Asserts that the subject is not equal to as the <paramref name="compareTo"/> value.</summary>
    /// <param name="compareTo">The value against which the subject is compared.</param>
    public AndOperator<TAssertions> NotBe(Guid compareTo)
    {
        if (Subject != compareTo)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("expected", compareTo)
            .SetContextData("actual", Subject)
            .SetMessageTemplate("Expected {context} to be {expected}, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject is equal to <see cref="Guid.Empty"/>.</summary>
    public AndOperator<TAssertions> BeEmpty()
    {
        if (Subject == Guid.Empty)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetMessageTemplate("Expected {context} to be empty, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject is neither null nor equal to <see cref="Guid.Empty"/>.</summary>
    public AndOperator<TAssertions> NotBeEmpty()
    {
        if (Subject is { } value && value != Guid.Empty)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetMessageTemplate("Expected {context} not to be empty, but found {actual}.")
            .Build();
    }
}