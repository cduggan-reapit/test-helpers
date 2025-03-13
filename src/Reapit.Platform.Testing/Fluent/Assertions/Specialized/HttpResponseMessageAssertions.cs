using Microsoft.AspNetCore.Mvc;
using Reapit.Platform.Testing.Fluent.Assertions.Abstract;
using Reapit.Platform.Testing.Fluent.Core;
using Reapit.Platform.Testing.Fluent.Failures;
using System.Net;
using System.Net.Http.Json;

namespace Reapit.Platform.Testing.Fluent.Assertions.Specialized;

/// <summary>Contains assertions for <see cref="HttpResponseMessage"/> objects.</summary>
/// <param name="subject">The assertion subject.</param>
[DebuggerNonUserCode]
public class HttpResponseMessageAssertions(HttpResponseMessage? subject)
    : HttpResponseMessageAssertions<HttpResponseMessageAssertions>(subject);

/// <summary>Contains assertions for <see cref="HttpResponseMessage"/> objects.</summary>
/// <param name="subject">The assertion subject.</param>
/// <typeparam name="TAssertions">The type of assertions to return in continuation objects.</typeparam>
[DebuggerNonUserCode]
public class HttpResponseMessageAssertions<TAssertions>(HttpResponseMessage? subject)
    : NullableReferenceTypeAssertions<HttpResponseMessage, TAssertions>(subject)
    where TAssertions : HttpResponseMessageAssertions<TAssertions>
{
    /// <inheritdoc />
    protected override string Context => "response";

    /// <summary>Asserts that the response has the expected status code.</summary>
    /// <param name="expected">The expected status code.</param>
    public AndOperator<TAssertions> HaveStatusCode(HttpStatusCode expected)
    {
        if (Subject?.StatusCode == expected)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("expected", expected)
            .SetContextData("actual", Subject?.StatusCode)
            .SetMessageTemplate("Expected {context} to have {expected} status code, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the response has the expected payload.</summary>
    /// <param name="expected">The expected payload.</param>
    public AndOperator<TAssertions> HaveJsonContent<TContent>(TContent expected)
        => HaveJsonContent(expected, false, out _);

    /// <summary>Asserts that the response has the expected payload.</summary>
    /// <param name="expected">The expected payload.</param>
    /// <param name="actual">The deserialized payload.</param>
    public AndOperator<TAssertions> HaveJsonContent<TContent>(TContent expected, out TContent? actual)
        => HaveJsonContent(expected, false, out actual);

    /// <summary>Asserts that the response has the expected payload.</summary>
    /// <param name="expected">The expected payload.</param>
    /// <param name="strict">Flag indicating whether strict equivalence is required.</param>
    public AndOperator<TAssertions> HaveJsonContent<TContent>(TContent expected, bool strict)
        => HaveJsonContent(expected, strict, out _);

    /// <summary>Asserts that the response has the expected payload.</summary>
    /// <param name="expected">The expected payload.</param>
    /// <param name="strict">Flag indicating whether strict equivalence is required.</param>
    /// <param name="actual">The deserialized payload.</param>
    public AndOperator<TAssertions> HaveJsonContent<TContent>(TContent expected, bool strict, out TContent? actual)
    {
        var builder = TestFailureBuilder.CreateForContext(Context).SetContextData("expected", expected);
        if (Subject is null)
            throw builder.SetMessageTemplate("Expected {context} to be {expected}, but found {null}.").Build();

        actual = Subject.Content.ReadFromJsonAsync<TContent>().Result;

        var equivalent = EquivalenceAnalyzer.VerifyEquivalence(expected, actual, strict);
        if (equivalent is null)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw builder.SetContextData("actual", actual)
            .SetMessageTemplate("Expected {context} to be {expected}, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the response has a ProblemDetails payload.</summary>
    public AndOperator<TAssertions> BeProblemDetails()
        => BeProblemDetails(out _);

    /// <summary>Asserts that the response has a ProblemDetails payload.</summary>
    /// <param name="actual">The deserialized problem details payload.</param>
    public AndOperator<TAssertions> BeProblemDetails(out ProblemDetails? actual)
    {
        var builder = TestFailureBuilder.CreateForContext(Context).SetContextData("expected", "");
        if (Subject is null)
            throw builder.SetMessageTemplate("Expected {context} to be {expected}, but found {null}.").Build();

        // Normally we'd make this method async for this, but doing so would make chained assertions require nested 
        // parentheses.  Instead, we just grab the result of the async method to force it into linearity.
        actual = Subject.Content.ReadFromJsonAsync<ProblemDetails>().Result;

        // ReadFromJson will return an object will null values.  In reality, status should always be set, so we'll use
        // that as a discriminator.
        if (actual?.Status != null)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw builder.SetContextData("actual", actual)
            .SetMessageTemplate("Expected {context} to be ProblemDetails, but found {actual}.")
            .Build();
    }
}