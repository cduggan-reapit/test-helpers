using Reapit.Platform.Testing.Fluent.Core;
using Reapit.Platform.Testing.Fluent.Failures;

namespace Reapit.Platform.Testing.Fluent.Assertions.Primitives;

/// <summary>Contains assertions for enum values.</summary>
/// <param name="subject">The assertion subject.</param>
/// <typeparam name="TEnum">The type of enum.</typeparam>
[DebuggerNonUserCode]
public class EnumAssertions<TEnum>(TEnum subject) : NullableEnumAssertions<TEnum, EnumAssertions<TEnum>>(subject)
    where TEnum : struct, Enum;

/// <summary>Contains assertions for enum values.</summary>
/// <param name="subject">The assertion subject.</param>
/// <typeparam name="TEnum">The type of enum.</typeparam>
[DebuggerNonUserCode]
public class NullableEnumAssertions<TEnum>(TEnum? subject) : NullableEnumAssertions<TEnum, NullableEnumAssertions<TEnum>>(subject)
    where TEnum : struct, Enum;

/// <summary>Contains assertions for nullable enum types.</summary>
/// <typeparam name="TAssertions">The type of assertions to return in continuation objects.</typeparam>
/// <typeparam name="TEnum">The type of enum.</typeparam>
/// <param name="subject">The assertion subject.</param>
[DebuggerNonUserCode]
public class NullableEnumAssertions<TEnum, TAssertions>(TEnum? subject)
    where TEnum : struct, Enum
    where TAssertions : NullableEnumAssertions<TEnum, TAssertions>
{
    /// <summary>The assertion context name.</summary>
    protected string Context { get; } = "enum";

    /// <summary>The subject of assertions.</summary>
    protected TEnum? Subject { get; } = subject;

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

    /// <summary>Asserts that the subject is equal to the <paramref name="compareTo"/> value.</summary>
    /// <param name="compareTo">The expected value.</param>
    public AndOperator<TAssertions> Be(TEnum compareTo)
    {
        if (Subject?.Equals(compareTo) == true)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("expected", compareTo)
            .SetContextData("actual", Subject)
            .SetMessageTemplate("Expected {context} to be {expected}, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject is equal to the <paramref name="compareTo"/> value.</summary>
    /// <param name="compareTo">The expected value.</param>
    public AndOperator<TAssertions> Be(TEnum? compareTo)
    {
        if (Nullable.Equals(Subject, compareTo))
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("expected", compareTo)
            .SetContextData("actual", Subject)
            .SetMessageTemplate("Expected {context} to be {expected}, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject is not equal to as the <paramref name="compareTo"/> value.</summary>
    /// <param name="compareTo">The expected value.</param>
    public AndOperator<TAssertions> NotBe(TEnum compareTo)
    {
        if (Subject?.Equals(compareTo) != true)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("expected", compareTo)
            .SetContextData("actual", Subject)
            .SetMessageTemplate("Expected {context} not to be {expected}, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject is not equal to as the <paramref name="compareTo"/> value.</summary>
    /// <param name="compareTo">The expected value.</param>
    public AndOperator<TAssertions> NotBe(TEnum? compareTo)
    {
        if (!Nullable.Equals(Subject, compareTo))
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("expected", compareTo)
            .SetContextData("actual", Subject)
            .SetMessageTemplate("Expected {context} not to be {expected}, but found {actual}.")
            .Build();
    }
}