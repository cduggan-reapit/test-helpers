using Reapit.Platform.Testing.Fluent.Core;
using Reapit.Platform.Testing.Fluent.Failures;

namespace Reapit.Platform.Testing.Fluent.Assertions.Primitives;

/// <summary>Contains assertions for nullable Boolean types.</summary>
/// <param name="subject">The assertion subject.</param>
[DebuggerNonUserCode]
public class NullableBooleanAssertions(bool? subject)
    : NullableBooleanAssertions<NullableBooleanAssertions>(subject);

/// <summary>Contains assertions for nullable Boolean types.</summary>
/// <typeparam name="TAssertions">The type of assertions to return in continuation objects.</typeparam>
/// <param name="subject">The assertion subject.</param>
[DebuggerNonUserCode]
public class NullableBooleanAssertions<TAssertions>(bool? subject)
    : BooleanAssertions<TAssertions>(subject)
    where TAssertions : NullableBooleanAssertions<TAssertions>
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
}