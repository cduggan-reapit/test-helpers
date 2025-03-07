using Reapit.Platform.Testing.Fluent.Core;
using Reapit.Platform.Testing.Fluent.Failures;
using System.Linq.Expressions;

namespace Reapit.Platform.Testing.Fluent.Assertions.Primitives;

/// <summary>Contains assertions for nullable DateTimeOffset types.</summary>
/// <param name="subject">The assertion subject.</param>
[DebuggerNonUserCode]
public class NullableDateTimeOffsetAssertions(DateTimeOffset? subject)
    : NullableDateTimeOffsetAssertions<NullableDateTimeOffsetAssertions>(subject);

/// <summary>Contains assertions for nullable DateTimeOffset types.</summary>
/// <typeparam name="TAssertions">The type of assertions to return in continuation objects.</typeparam>
/// <param name="subject">The assertion subject.</param>
[DebuggerNonUserCode]
public class NullableDateTimeOffsetAssertions<TAssertions>(DateTimeOffset? subject)
    : DateTimeOffsetAssertions<TAssertions>(subject)
    where TAssertions : NullableDateTimeOffsetAssertions<TAssertions>
{
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
    public AndOperator<TAssertions> Match(Expression<Func<DateTimeOffset?, bool>> predicate)
    {
        if (predicate.Compile()(Subject))
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetMessageTemplate("Expected {context} to satisfy the predicate, but found {actual}.")
            .Build();
    }
}