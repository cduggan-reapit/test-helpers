using System.Net;

namespace Reapit.Platform.Testing.UnitTests.Fluent.Assertions.Primitives;

public static class NullableEnumAssertionsTests
{
    public class HaveValue
    {
        [Fact]
        public void Should_Fail_WhenSubjectIsNull()
        {
            var action = () => ((StringComparison?)null).Must().HaveValue();
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
            var action = () => ((StringComparison?)null).Must().NotHaveValue();
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
            var action = () => ((StringComparison?)null).Must().BeNull();
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
            var action = () => ((StringComparison?)null).Must().NotBeNull();
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
            var action = () => ((HttpStatusCode?)null).Must().Be(null);
            action.Must().NotThrow();
        }
    }

    public class NotBe
    {
        [Fact]
        public void Should_Fail_WhenNullSubjectEqual_ToNull()
        {
            var action = () => ((HttpStatusCode?)null).Must().NotBe(null);
            action.Must().Throw<XunitException>();
        }
    }
}