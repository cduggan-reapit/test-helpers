using Microsoft.AspNetCore.Mvc;
using Reapit.Platform.Testing.Fluent.Assertions.Abstract;
using Reapit.Platform.Testing.Fluent.Core;
using Reapit.Platform.Testing.Fluent.Failures;

namespace Reapit.Platform.Testing.Fluent.Assertions.Specialized;

/// <summary>Contains assertions for <see cref="ProblemDetails"/> objects.</summary>
/// <param name="subject">The assertion subject.</param>
[DebuggerNonUserCode]
public class ProblemDetailsAssertions(ProblemDetails? subject)
    : ProblemDetailsAssertions<ProblemDetailsAssertions>(subject);

/// <summary>Contains assertions for <see cref="ProblemDetails"/> objects.</summary>
/// <param name="subject">The assertion subject.</param>
/// <typeparam name="TAssertions">The type of assertions to return in continuation objects.</typeparam>
[DebuggerNonUserCode]
public class ProblemDetailsAssertions<TAssertions>(ProblemDetails? subject)
    : NullableReferenceTypeAssertions<ProblemDetails, ProblemDetailsAssertions<TAssertions>>(subject)
    where TAssertions : ProblemDetailsAssertions<TAssertions>
{
    /// <inheritdoc/>
    protected override string Context => "problem details";

    /// <summary>Asserts that the subject has the expected type value.</summary>
    /// <param name="expected">The expected type.</param>
    public AndOperator<TAssertions> HaveType(string expected)
    {
        if (Subject?.Type?.Equals(expected) == true)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("expected", expected)
            .SetContextData("actual", Subject?.Type)
            .SetMessageTemplate("Expected {context} to have type {expected}, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject has the expected title value.</summary>
    /// <param name="expected">The expected title.</param>
    public AndOperator<TAssertions> HaveTitle(string expected)
    {
        if (Subject?.Title?.Equals(expected) == true)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("expected", expected)
            .SetContextData("actual", Subject?.Type)
            .SetMessageTemplate("Expected {context} to have title {expected}, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject has the expected status code value.</summary>
    /// <param name="status">The expected status code.</param>
    public AndOperator<TAssertions> HaveStatus(int status)
    {
        if (Subject?.Status?.Equals(status) == true)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("expected", status)
            .SetContextData("actual", Subject?.Type)
            .SetMessageTemplate("Expected {context} to have status {expected}, but found {actual}.")
            .Build();
    }
}