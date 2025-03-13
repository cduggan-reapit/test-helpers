using Microsoft.AspNetCore.Mvc;
using Reapit.Platform.Testing.Extensions;
using System.Net;

namespace Reapit.Platform.Testing.UnitTests.Fluent.Assertions.Specialized;

public static class HttpResponseMessageAssertionsTests
{
    public class HaveStatusCode
    {
        [Fact]
        public void Should_Fail_WhenSubjectNull()
        {
            HttpResponseMessage? subject = null;
            var action = () => subject.Must().HaveStatusCode(HttpStatusCode.OK);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenStatusCodeDoesNotMatch()
        {
            var subject = new HttpResponseMessage { StatusCode = HttpStatusCode.Forbidden };
            var action = () => subject.Must().HaveStatusCode(HttpStatusCode.OK);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Pass_WhenStatusCodeMatches()
        {
            var subject = new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
            var action = () => subject.Must().HaveStatusCode(HttpStatusCode.OK);
            action.Must().NotThrow();
        }
    }

    public class HaveJsonContent
    {
        [Fact]
        public void Should_Fail_WhenSubjectNull()
        {
            var expected = new { Property = "value" };
            HttpResponseMessage? subject = null;
            var action = () => subject.Must().HaveJsonContent(expected);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenContentDoesNotMatch()
        {
            var expected = new { Property = "value" };
            var subject = new HttpResponseMessage { Content = new { Property = "Different" }.ToStringContent() };
            var action = () => subject.Must().HaveJsonContent(expected);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenContentDoesNotMatch_StrictComparison()
        {
            var expected = new { Property = "value" };
            var subject = new HttpResponseMessage { Content = new { Property = "Value" }.ToStringContent() };
            var action = () => subject.Must().HaveJsonContent(expected, true);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Pass_WhenContentMatches()
        {
            var expected = new { Property = "value" };
            var subject = new HttpResponseMessage { Content = expected.ToStringContent() };
            var action = () => subject.Must().HaveJsonContent(expected, true);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_ReturnPayload_WhenContentMatches()
        {
            var expected = new { Property = "value" };
            var subject = new HttpResponseMessage { Content = expected.ToStringContent() };
            subject.Must().HaveJsonContent(expected, true, out var actual);
            actual.Must().BeEquivalentTo(expected);
        }
    }

    public class BeProblemDetails
    {
        [Fact]
        public void Should_Fail_WhenSubjectNull()
        {
            HttpResponseMessage? subject = null;
            var action = () => subject.Must().BeProblemDetails();
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenContentNull()
        {
            var subject = new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = "null".ToStringContent() };
            var action = () => subject.Must().BeProblemDetails();
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenContentIsNotProblemDetails()
        {
            var content = new { Property = "value" };
            var subject = new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = content.ToStringContent() };
            var action = () => subject.Must().BeProblemDetails();
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Pass_WhenContentIsProblemDetails()
        {
            var content = new ProblemDetails { Title = "test problem", Status = 200 };
            var subject = new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = content.ToStringContent() };
            subject.Must().BeProblemDetails(out var actual);
            actual.Must().BeEquivalentTo(content);
        }
    }
}