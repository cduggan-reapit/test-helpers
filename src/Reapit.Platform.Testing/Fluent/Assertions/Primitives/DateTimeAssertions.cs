using Reapit.Platform.Testing.Fluent.Core;
using Reapit.Platform.Testing.Fluent.Failures;
using System.Linq.Expressions;

namespace Reapit.Platform.Testing.Fluent.Assertions.Primitives;

/// <summary>Contains assertions for DateTime types.</summary>
/// <param name="subject">The assertion subject.</param>
[DebuggerNonUserCode]
public class DateTimeAssertions(DateTime subject) : NullableDateTimeAssertions<DateTimeAssertions>(subject);

/// <summary>Contains assertions for nullable DateTime types.</summary>
/// <param name="subject">The assertion subject.</param>
[DebuggerNonUserCode]
public class NullableDateTimeAssertions(DateTime? subject) : NullableDateTimeAssertions<NullableDateTimeAssertions>(subject);

/// <summary>Contains assertions for nullable DateTime types.</summary>
/// <typeparam name="TAssertions">The type of assertions to return in continuation objects.</typeparam>
/// <param name="subject">The assertion subject.</param>
[DebuggerNonUserCode]
public class NullableDateTimeAssertions<TAssertions>(DateTime? subject)
    where TAssertions : NullableDateTimeAssertions<TAssertions>
{
    /// <summary>The assertion context name.</summary>
    protected const string Context = "datetime";

    /// <summary>The subject of assertions.</summary>
    protected DateTime? Subject { get; } = subject;

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
    public AndOperator<TAssertions> Match(Expression<Func<DateTime?, bool>> predicate)
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
    public AndOperator<TAssertions> Be(DateTime compareTo)
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
    public AndOperator<TAssertions> Be(DateTime? compareTo)
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
    public AndOperator<TAssertions> NotBe(DateTime compareTo)
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
    public AndOperator<TAssertions> NotBe(DateTime? compareTo)
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

    /// <summary>Asserts that the subject is within a defined TimeSpan of the given <paramref name="compareTo"/> value.</summary>
    /// <param name="compareTo">The value against which the subject is compared.</param>
    /// <param name="precision">The maximum amount of time by which the subject may differ.</param>
    /// <exception cref="ArgumentException">when <paramref name="precision"/> is negative.</exception>
    public AndOperator<TAssertions> BeCloseTo(DateTime compareTo, TimeSpan precision)
    {
        if (precision < TimeSpan.Zero)
            throw new ArgumentException("Precision cannot be a negative value.", nameof(precision));

        var minTicks = (compareTo - DateTime.MinValue).Ticks;
        var maxTicks = (DateTime.MaxValue - compareTo).Ticks;

        var minValue = compareTo.AddTicks(-Math.Min(precision.Ticks, minTicks));
        var maxValue = compareTo.AddTicks(Math.Min(precision.Ticks, maxTicks));

        if (Subject >= minValue && Subject <= maxValue)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetContextData("expected", compareTo)
            .SetContextData("precision", precision)
            .SetMessageTemplate("Expected {context} to be within {precision} of {expected}, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject is not within a defined TimeSpan of the given <paramref name="compareTo"/> value.</summary>
    /// <param name="compareTo">The value against which the subject is compared.</param>
    /// <param name="precision">The minimum amount of time by which the subject must differ.</param>
    /// <exception cref="ArgumentException">when <paramref name="precision"/> is negative.</exception>
    public AndOperator<TAssertions> NotBeCloseTo(DateTime compareTo, TimeSpan precision)
    {
        if (precision < TimeSpan.Zero)
            throw new ArgumentException("Precision cannot be a negative value.", nameof(precision));

        var minTicks = (compareTo - DateTime.MinValue).Ticks;
        var maxTicks = (DateTime.MaxValue - compareTo).Ticks;

        var minValue = compareTo.AddTicks(-Math.Min(precision.Ticks, minTicks));
        var maxValue = compareTo.AddTicks(Math.Min(precision.Ticks, maxTicks));

        if (Subject is null || Subject < minValue || Subject > maxValue)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetContextData("expected", compareTo)
            .SetContextData("precision", precision)
            .SetMessageTemplate("Expected {context} not to be within {precision} of {expected}, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject is greater than the given <paramref name="compareTo"/> value.</summary>
    /// <param name="compareTo">The value which the subject must be greater than.</param>
    public AndOperator<TAssertions> BeAfter(DateTime compareTo)
    {
        if (Subject?.CompareTo(compareTo) > 0)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetContextData("compareTo", compareTo)
            .SetMessageTemplate("Expected {context} to be after {compareTo}, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject is equal to or greater than the given <paramref name="compareTo"/> value.</summary>
    /// <param name="compareTo">The value which the subject must be equal to or greater than.</param>
    public AndOperator<TAssertions> BeOnOrAfter(DateTime compareTo)
    {
        if (Subject?.CompareTo(compareTo) >= 0)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetContextData("compareTo", compareTo)
            .SetMessageTemplate("Expected {context} to be on or after {compareTo}, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject is less than the given <paramref name="compareTo"/> value.</summary>
    /// <param name="compareTo">The value which the subject must be less than.</param>
    public AndOperator<TAssertions> BeBefore(DateTime compareTo)
    {
        if (Subject?.CompareTo(compareTo) < 0)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetContextData("compareTo", compareTo)
            .SetMessageTemplate("Expected {context} to be before {compareTo}, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject is equal to or less than the given <paramref name="compareTo"/> value.</summary>
    /// <param name="compareTo">The value which the subject must be equal to or less than.</param>
    public AndOperator<TAssertions> BeOnOrBefore(DateTime compareTo)
    {
        if (Subject?.CompareTo(compareTo) <= 0)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetContextData("compareTo", compareTo)
            .SetMessageTemplate("Expected {context} to be on or before {compareTo}, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject matches the given predicate.</summary>
    /// <param name="predicate">The predicate which must be satisfied.</param>
    public AndOperator<TAssertions> Match(Expression<Func<DateTime, bool>> predicate)
    {
        if (Subject is { } subject && predicate.Compile()(subject))
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetMessageTemplate("Expected {context} to satisfy the predicate, but found {actual}.")
            .Build();
    }
}