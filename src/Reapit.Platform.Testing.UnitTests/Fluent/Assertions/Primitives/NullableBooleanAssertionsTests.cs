namespace Reapit.Platform.Testing.UnitTests.Fluent.Assertions.Primitives;

public static class NullableBooleanAssertionsTests
{
    private static readonly bool? TrueValue = true;
    private static bool? NullValue => null;

    public class HaveValue
    {
        [Fact]
        public void Should_Fail_WhenSubjectIsNull()
        {
            var action = () => NullValue.Must().HaveValue();
            action.Must().Throw();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectHasValue()
        {
            var action = () => TrueValue.Must().HaveValue();
            action.Must().NotThrow();
        }
    }

    public class NotHaveValue
    {
        [Fact]
        public void Should_NotFail_WhenSubjectIsNull()
        {
            var action = () => NullValue.Must().NotHaveValue();
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenSubjectHasValue()
        {
            var action = () => TrueValue.Must().NotHaveValue();
            action.Must().Throw();
        }
    }

    public class BeNull
    {
        [Fact]
        public void Should_NotFail_WhenSubjectIsNull()
        {
            var action = () => NullValue.Must().BeNull();
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenSubjectHasValue()
        {
            var action = () => TrueValue.Must().BeNull();
            action.Must().Throw();
        }
    }

    public class NotBeNull
    {
        [Fact]
        public void Should_Fail_WhenSubjectIsNull()
        {
            var action = () => NullValue.Must().NotBeNull();
            action.Must().Throw();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectHasValue()
        {
            var action = () => TrueValue.Must().NotBeNull();
            action.Must().NotThrow();
        }
    }

    /*
     * Inherited Assertions
     */

    public class Be
    {
        [Fact]
        public void Should_NotFail_WhenSubjectNull_AndExpectationNull()
        {
            var action = () => NullValue.Must().Be(NullValue);
            action.Must().NotThrow();
        }
    }

    public class NotBe
    {
        [Fact]
        public void Should_Fail_WhenSubjectNull_AndExpectationNull()
        {
            var action = () => NullValue.Must().NotBe(NullValue);
            action.Must().Throw<XunitException>();
        }
    }

    public class BeTrue
    {
        [Fact]
        public void Should_Fail_WhenSubjectNull()
        {
            var action = () => NullValue.Must().BeTrue();
            action.Must().Throw<XunitException>();
        }
    }

    public class BeFalse
    {
        [Fact]
        public void Should_Fail_WhenSubjectNull()
        {
            var action = () => NullValue.Must().BeFalse();
            action.Must().Throw<XunitException>();
        }
    }
}