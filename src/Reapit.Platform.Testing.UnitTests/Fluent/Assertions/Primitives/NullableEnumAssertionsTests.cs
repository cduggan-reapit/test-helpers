using System.Net;

namespace Reapit.Platform.Testing.UnitTests.Fluent.Assertions.Primitives;

public static class NullableEnumAssertionsTests
{
    public class HaveValue
    {
        [Fact]
        public void Should_Fail_WhenSubjectIsNull()
        {
            var action = () => (null as StringComparison?).Must().HaveValue();
            action.Must().Throw();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectHasValue()
        {
            var action = () => ((StringComparison?)StringComparison.Ordinal).Must().HaveValue();
            action.Must().NotThrow();
        }
    }

    public class NotHaveValue
    {
        [Fact]
        public void Should_NotFail_WhenSubjectIsNull()
        {
            var action = () => (null as StringComparison?).Must().NotHaveValue();
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenSubjectHasValue()
        {
            var action = () => ((StringComparison?)StringComparison.Ordinal).Must().NotHaveValue();
            action.Must().Throw();
        }
    }

    public class BeNull
    {
        [Fact]
        public void Should_NotFail_WhenSubjectIsNull()
        {
            var action = () => (null as StringComparison?).Must().BeNull();
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenSubjectHasValue()
        {
            var action = () => ((StringComparison?)StringComparison.Ordinal).Must().BeNull();
            action.Must().Throw();
        }
    }

    public class NotBeNull
    {
        [Fact]
        public void Should_Fail_WhenSubjectIsNull()
        {
            var action = () => (null as StringComparison?).Must().NotBeNull();
            action.Must().Throw();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectHasValue()
        {
            var action = () => ((StringComparison?)StringComparison.Ordinal).Must().NotBeNull();
            action.Must().NotThrow();
        }
    }

    /*
     * Inherited methods
     */

    public class Be
    {
        [Fact]
        public void Should_NotFail_WhenNullSubjectEqual_ToNull()
        {
            var action = () => (null as HttpStatusCode?).Must().Be(null);
            action.Must().NotThrow();
        }
    }

    public class NotBe
    {
        [Fact]
        public void Should_Fail_WhenNullSubjectEqual_ToNull()
        {
            var action = () => (null as HttpStatusCode?).Must().NotBe(null);
            action.Must().Throw<XunitException>();
        }
    }
}