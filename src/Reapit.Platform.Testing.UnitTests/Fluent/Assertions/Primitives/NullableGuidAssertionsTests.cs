namespace Reapit.Platform.Testing.UnitTests.Fluent.Assertions.Primitives;

public static class NullableGuidAssertionsTests
{
    public class HaveValue
    {
        [Fact]
        public void Should_Fail_WhenSubjectIsNull()
        {
            var action = () => GetNullGuid().Must().HaveValue();
            action.Must().Throw();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectHasValue()
        {
            var action = () => GetNullableGuid().Must().HaveValue();
            action.Must().NotThrow();
        }
    }

    public class NotHaveValue
    {
        [Fact]
        public void Should_NotFail_WhenSubjectIsNull()
        {
            var action = () => GetNullGuid().Must().NotHaveValue();
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenSubjectHasValue()
        {
            var action = () => GetNullableGuid().Must().NotHaveValue();
            action.Must().Throw();
        }
    }

    public class BeNull
    {
        [Fact]
        public void Should_NotFail_WhenSubjectIsNull()
        {
            var action = () => GetNullGuid().Must().BeNull();
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenSubjectHasValue()
        {
            var action = () => GetNullableGuid().Must().BeNull();
            action.Must().Throw();
        }
    }

    public class NotBeNull
    {
        [Fact]
        public void Should_Fail_WhenSubjectIsNull()
        {
            var action = () => GetNullGuid().Must().NotBeNull();
            action.Must().Throw();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectHasValue()
        {
            var action = () => GetNullableGuid().Must().NotBeNull();
            action.Must().NotThrow();
        }
    }

    /*
     * Inherited methods
     */

    public class Be
    {
        [Fact]
        public void Should_Fail_WhenSubjectNull()
        {
            var action = () => GetNullGuid().Must().Be(Guid.NewGuid());
            action.Must().Throw<XunitException>();
        }
    }

    public class NotBe
    {
        [Fact]
        public void Should_NotFail_WhenSubjectNull()
        {
            var action = () => GetNullGuid().Must().NotBe(Guid.NewGuid());
            action.Must().NotThrow();
        }
    }

    public class BeEmpty
    {
        [Fact]
        public void Should_Fail_WhenSubjectNull()
        {
            var action = () => GetNullGuid().Must().BeEmpty();
            action.Must().Throw<XunitException>();
        }
    }

    public class NotBeEmpty
    {
        [Fact]
        public void Should_Fail_WhenSubjectNull()
        {
            var action = () => GetNullGuid().Must().NotBeEmpty();
            action.Must().Throw<XunitException>();
        }
    }

    /*
     * Private methods
     */

    private static Guid? GetNullGuid() => null;

    private static Guid? GetNullableGuid() => Guid.NewGuid();
}