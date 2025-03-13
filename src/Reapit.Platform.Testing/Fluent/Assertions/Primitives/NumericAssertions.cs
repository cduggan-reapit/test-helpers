
using Reapit.Platform.Testing.Fluent.Core;
using Reapit.Platform.Testing.Fluent.Failures;
using System.Linq.Expressions;

namespace Reapit.Platform.Testing.Fluent.Assertions.Primitives;

/// <summary>Contains assertions for numeric types.</summary>
/// <typeparam name="T">The type against which to assert.</typeparam>
/// <param name="subject">The assertion subject.</param>
[DebuggerNonUserCode]
public class NumericAssertions<T>(T subject) : NullableNumericAssertions<T, NumericAssertions<T>>(subject) where T : struct, IComparable<T>;

/// <summary>Contains assertions for nullable numeric types.</summary>
/// <typeparam name="T">The type against which to assert.</typeparam>
/// <param name="subject">The assertion subject.</param>
[DebuggerNonUserCode]
public class NullableNumericAssertions<T>(T? subject) : NullableNumericAssertions<T, NullableNumericAssertions<T>>(subject)
    where T : struct, IComparable<T>;

/// <summary>Contains assertions for nullable numeric types.</summary>
/// <typeparam name="T">The type against which to assert.</typeparam>
/// <typeparam name="TAssertions">The type of assertions to return in continuation objects.</typeparam>
/// <param name="subject">The assertion subject.</param>
[DebuggerNonUserCode]
public class NullableNumericAssertions<T, TAssertions>(T? subject)
    where T : struct, IComparable<T>
    where TAssertions : NullableNumericAssertions<T, TAssertions>
{
    /// <summary>The assertion context name.</summary>
    protected const string Context = "number";

    /// <summary>The subject of assertions.</summary>
    protected T? Subject { get; } = subject;

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

    /// <summary>Asserts that the subject matches the given predicate.</summary>
    /// <param name="predicate">The predicate which must be satisfied.</param>
    public AndOperator<TAssertions> Match(Expression<Func<T?, bool>> predicate)
    {
        if (predicate.Compile()(Subject))
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetMessageTemplate("Expected {context} to satisfy the predicate, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject is the same as the <paramref name="compareTo"/> value.</summary>
    /// <param name="compareTo">The expected value.</param>
    public AndOperator<TAssertions> Be(T compareTo)
    {
        if (Subject?.CompareTo(compareTo) == 0)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetContextData("expected", compareTo)
            .SetMessageTemplate("Expected {context} to be {expected}, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject is the same as the <paramref name="compareTo"/> value.</summary>
    /// <param name="compareTo">The expected value.</param>
    public AndOperator<TAssertions> Be(T? compareTo)
    {
        if (Subject is null && compareTo is null)
            return new AndOperator<TAssertions>((TAssertions)this);

        // If compareTo is null then Subject must be null, otherwise the values must be the same.
        if (compareTo is { } value ? Subject?.CompareTo(value) == 0 : Subject is null)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetContextData("expected", compareTo)
            .SetMessageTemplate("Expected {context} to be {expected}, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject is not the same as the <paramref name="compareTo"/> value.</summary>
    /// <param name="compareTo">The unexpected value.</param>
    public AndOperator<TAssertions> NotBe(T compareTo)
    {
        if (Subject?.CompareTo(compareTo) != 0)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetContextData("expected", compareTo)
            .SetMessageTemplate("Expected {context} to not be {expected}, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject is not the same as the <paramref name="compareTo"/> value.</summary>
    /// <param name="compareTo">The unexpected value.</param>
    public AndOperator<TAssertions> NotBe(T? compareTo)
    {
        // If compareTo is null then Subject must be null, otherwise the values must be the same.
        if (compareTo is { } value ? Subject?.CompareTo(value) != 0 : Subject is not null)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetContextData("expected", compareTo)
            .SetMessageTemplate("Expected {context} to not be {expected}, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject is greater than zero.</summary>
    public AndOperator<TAssertions> BePositive()
    {
        if (Subject?.CompareTo(default) > 0)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetMessageTemplate("Expected {context} to be positive, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject is less than zero.</summary>
    public AndOperator<TAssertions> BeNegative()
    {
        if (Subject?.CompareTo(default) < 0)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetMessageTemplate("Expected {context} to be negative, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject is less than the <paramref name="compareTo"/> value.</summary>
    /// <param name="compareTo">The value against which the subject is compared.</param>
    public AndOperator<TAssertions> BeLessThan(T compareTo)
    {
        if (Subject?.CompareTo(compareTo) < 0)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetContextData("lessThan", compareTo)
            .SetMessageTemplate("Expected {context} to be less than {lessThan}, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject is less than or equal to the <paramref name="compareTo"/> value.</summary>
    /// <param name="compareTo">The value against which the subject is compared.</param>
    public AndOperator<TAssertions> BeLessThanOrEqualTo(T compareTo)
    {
        if (Subject?.CompareTo(compareTo) <= 0)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetContextData("lessThanOrEqualTo", compareTo)
            .SetMessageTemplate("Expected {context} to be less than or equal to {lessThanOrEqualTo}, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject is greater than the <paramref name="compareTo"/> value.</summary>
    /// <param name="compareTo">The value against which the subject is compared.</param>
    public AndOperator<TAssertions> BeGreaterThan(T compareTo)
    {
        if (Subject?.CompareTo(compareTo) > 0)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetContextData("greaterThan", compareTo)
            .SetMessageTemplate("Expected {context} to be greater than {greaterThan}, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject is greater than or equal to the <paramref name="compareTo"/> value.</summary>
    /// <param name="compareTo">The value against which the subject is compared.</param>
    public AndOperator<TAssertions> BeGreaterThanOrEqualTo(T compareTo)
    {
        if (Subject?.CompareTo(compareTo) >= 0)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetContextData("greaterThanOrEqualTo", compareTo)
            .SetMessageTemplate("Expected {context} to be greater than or equal to {greaterThanOrEqualTo}, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject is between <paramref name="minimumValue"/> and <paramref name="maximumValue"/>, inclusive.</summary>
    /// <param name="minimumValue">The lower bound of the acceptable range.</param>
    /// <param name="maximumValue">The upper bound of the acceptable range.</param>
    public AndOperator<TAssertions> BeInRange(T minimumValue, T maximumValue)
    {
        if (Subject is { } subject && subject.CompareTo(minimumValue) >= 0 && subject.CompareTo(maximumValue) <= 0)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetContextData("lowerBound", minimumValue)
            .SetContextData("upperBound", maximumValue)
            .SetMessageTemplate("Expected {context} to be between {lowerBound} and {upperBound}, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject is not between <paramref name="minimumValue"/> and <paramref name="maximumValue"/>, inclusive.</summary>
    /// <param name="minimumValue">The lower bound of the acceptable range.</param>
    /// <param name="maximumValue">The upper bound of the acceptable range.</param>
    public AndOperator<TAssertions> NotBeInRange(T minimumValue, T maximumValue)
    {
        if (Subject is { } subject && !(subject.CompareTo(minimumValue) >= 0 && subject.CompareTo(maximumValue) <= 0))
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetContextData("lowerBound", minimumValue)
            .SetContextData("upperBound", maximumValue)
            .SetMessageTemplate("Expected {context} to not be between {lowerBound} and {upperBound}, but found {actual}.")
            .Build();
    }
}