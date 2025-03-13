namespace Reapit.Platform.Testing.UnitTests.Fluent.Assertions.Primitives;

public static class BooleanAssertionsTests
{
    private const bool TrueValue = true;
    private const bool FalseValue = false;
    private static bool? NullValue => null;
    private static readonly bool? NullableTrueValue = true;
    private static readonly bool? NullableFalseValue = false;

    public class Be
    {
        [Fact]
        public void Should_Fail_WhenSubjectNotNull_AndExpectationNull()
        {
            var action = () => TrueValue.Must().Be(NullValue);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenSubjectTrue_AndExpectationFalse()
        {
            var action = () => TrueValue.Must().Be(FalseValue);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenSubjectTrue_AndExpectationNullableFalse()
        {
            var action = () => TrueValue.Must().Be(NullableFalseValue);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectNotNull_AndExpectationTrue()
        {
            var action = () => TrueValue.Must().Be(true);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectNotNull_AndExpectationNullableTrue()
        {
            var action = () => TrueValue.Must().Be(NullableTrueValue);
            action.Must().NotThrow();
        }
    }

    public class NotBe
    {
        [Fact]
        public void Should_NotFail_WhenSubjectNotNull_AndExpectationNull()
        {
            var action = () => TrueValue.Must().NotBe(NullValue);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectTrue_AndExpectationFalse()
        {
            var action = () => TrueValue.Must().NotBe(FalseValue);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectTrue_AndExpectationNullableFalse()
        {
            var action = () => TrueValue.Must().NotBe(NullableFalseValue);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenSubjectNotNull_AndExpectationTrue()
        {
            var action = () => TrueValue.Must().NotBe(true);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenSubjectNotNull_AndExpectationNullableTrue()
        {
            var action = () => TrueValue.Must().NotBe(NullableTrueValue);
            action.Must().Throw<XunitException>();
        }
    }

    public class BeTrue
    {
        [Fact]
        public void Should_Fail_WhenSubjectFalse()
        {
            var action = () => FalseValue.Must().BeTrue();
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectTrue()
        {
            var action = () => TrueValue.Must().BeTrue();
            action.Must().NotThrow();
        }
    }

    public class BeFalse
    {
        [Fact]
        public void Should_NotFail_WhenSubjectFalse()
        {
            var action = () => FalseValue.Must().BeFalse();
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenSubjectTrue()
        {
            var action = () => TrueValue.Must().BeFalse();
            action.Must().Throw<XunitException>();
        }
    }
}