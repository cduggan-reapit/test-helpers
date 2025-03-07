using Reapit.Platform.Testing.Fluent.Core;
using Reapit.Platform.Testing.Fluent.Failures;

namespace Reapit.Platform.Testing.Fluent.Assertions.Primitives;

/// <summary>Contains assertions for enum values.</summary>
/// <param name="subject">The assertion subject.</param>
/// <typeparam name="TEnum">The type of enum.</typeparam>
public class EnumAssertions<TEnum>(TEnum subject)
    : EnumAssertions<TEnum, EnumAssertions<TEnum>>(subject)
    where TEnum : struct, Enum;

/// <summary>Contains assertions for enum values.</summary>
/// <typeparam name="TEnum">The type of enum.</typeparam>
/// <typeparam name="TAssertions">The type of assertions to return in continuation objects.</typeparam>
public class EnumAssertions<TEnum, TAssertions>
    where TEnum : struct, Enum
    where TAssertions : EnumAssertions<TEnum, TAssertions>
{
    /// <summary>The assertion context name.</summary>
    protected string Context { get; } = "enum";

    /// <summary>The subject of assertions.</summary>
    protected TEnum? Subject { get; }

    /// <summary>Initializes a new instance of the <see cref="EnumAssertions{TEnum,TAssertions}"/> class.</summary>
    /// <param name="subject">The nullable assertion subject.</param>
    protected EnumAssertions(TEnum? subject) => Subject = subject;

    /// <summary>Initializes a new instance of the <see cref="EnumAssertions{TEnum,TAssertions}"/> class.</summary>
    /// <param name="subject">The assertion subject.</param>
    protected EnumAssertions(TEnum subject) : this((TEnum?)subject)
    {
    }

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