using Microsoft.AspNetCore.Mvc;

namespace Reapit.Platform.Testing.UnitTests.Fluent.Assertions.Specialized;

public static class ProblemDetailsAssertionsTests
{
    public class HaveType
    {
        [Fact]
        public void Should_Fail_WhenTypeNull()
        {
            var subject = new ProblemDetails();
            var action = () => subject.Must().HaveType("expected");
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenTypeNotEqual()
        {
            var subject = new ProblemDetails { Type = "unexpected" };
            var action = () => subject.Must().HaveType("expected");
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Pass_WhenTypeEqual()
        {
            var subject = new ProblemDetails { Type = "expected" };
            var action = () => subject.Must().HaveType("expected");
            action.Must().NotThrow();
        }
    }

    public class HaveTitle
    {
        [Fact]
        public void Should_Fail_WhenTitleNull()
        {
            var subject = new ProblemDetails();
            var action = () => subject.Must().HaveTitle("expected");
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenTitleNotEqual()
        {
            var subject = new ProblemDetails { Title = "unexpected" };
            var action = () => subject.Must().HaveTitle("expected");
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Pass_WhenTitleEqual()
        {
            var subject = new ProblemDetails { Title = "expected" };
            var action = () => subject.Must().HaveTitle("expected");
            action.Must().NotThrow();
        }
    }

    public class HaveStatus
    {
        [Fact]
        public void Should_Fail_WhenStatusNull()
        {
            var subject = new ProblemDetails();
            var action = () => subject.Must().HaveStatus(404);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenStatusNotEqual()
        {
            var subject = new ProblemDetails { Status = 200 };
            var action = () => subject.Must().HaveStatus(404);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Pass_WhenStatusEqual()
        {
            var subject = new ProblemDetails { Status = 404 };
            var action = () => subject.Must().HaveStatus(404);
            action.Must().NotThrow();
        }
    }
}