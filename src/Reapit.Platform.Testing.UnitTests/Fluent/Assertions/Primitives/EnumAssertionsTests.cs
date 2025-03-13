using System.Net;

namespace Reapit.Platform.Testing.UnitTests.Fluent.Assertions.Primitives;

public static class EnumAssertionsTests
{
    public class Be
    {
        [Fact]
        public void Should_Fail_WhenSubjectNotEqual()
        {
            var action = () => HttpStatusCode.OK.Must().Be(HttpStatusCode.Conflict);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectEqual()
        {
            var action = () => HttpStatusCode.OK.Must().Be(HttpStatusCode.OK);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenSubjectNotEqual_ToNull()
        {
            var action = () => HttpStatusCode.OK.Must().Be(null);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenSubjectNotEqual_ToNullable()
        {
            var action = () => HttpStatusCode.OK.Must().Be((HttpStatusCode?)HttpStatusCode.Conflict);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectEqual_ToNullable()
        {
            var action = () => HttpStatusCode.OK.Must().Be((HttpStatusCode?)HttpStatusCode.OK);
            action.Must().NotThrow();
        }
    }

    public class NotBe
    {
        [Fact]
        public void Should_NotFail_WhenSubjectNotEqual()
        {
            var action = () => HttpStatusCode.OK.Must().NotBe(HttpStatusCode.Conflict);
            action.Must().NotThrow();

        }

        [Fact]
        public void Should_Fail_WhenSubjectEqual()
        {
            var action = () => HttpStatusCode.OK.Must().NotBe(HttpStatusCode.OK);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectNotEqual_ToNull()
        {
            var action = () => HttpStatusCode.OK.Must().NotBe(null);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectNotEqual_ToNullable()
        {
            var action = () => HttpStatusCode.OK.Must().NotBe((HttpStatusCode?)HttpStatusCode.Conflict);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenSubjectEqual_ToNullable()
        {
            var action = () => HttpStatusCode.OK.Must().NotBe((HttpStatusCode?)HttpStatusCode.OK);
            action.Must().Throw<XunitException>();
        }
    }
}