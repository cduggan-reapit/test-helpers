namespace Reapit.Platform.Testing.UnitTests.Fluent.Assertions.Primitives;

public class NumericAssertionsTests
{
    [Fact]
    public void Should_InitializeForByte() => ((byte)0x01).Must();

    [Fact]
    public void Should_InitializeForSignedByte() => ((sbyte)0x01).Must();

    [Fact]
    public void Should_InitializeForShort() => ((short)1).Must();

    [Fact]
    public void Should_InitializeForUnsignedShort() => ((ushort)1).Must();

    [Fact]
    public void Should_InitializeForInt32() => 1.Must();

    [Fact]
    public void Should_InitializeForUnsignedInt32() => ((uint)1).Must();

    [Fact]
    public void Should_InitializeForLong() => ((long)1).Must();

    [Fact]
    public void Should_InitializeForUnsignedLong() => ((ulong)1).Must();

    [Fact]
    public void Should_InitializeForFloat() => ((float)1).Must();

    [Fact]
    public void Should_InitializeForDouble() => ((double)1).Must();

    [Fact]
    public void Should_InitializeForDecimal() => ((decimal)1).Must();

    public class Be
    {
        [Fact]
        public void Should_Fail_WhenNotEqualToExpected()
        {
            const int subject = 77, expected = 99;
            var action = () => subject.Must().Be(expected);
            action.Must().Throw();
        }

        [Fact]
        public void Should_NotFail_WhenEqualToExpected()
        {
            const int subject = 99, expected = 99;
            var action = () => subject.Must().Be(expected);
            action.Must().NotThrow();
        }
    }

    public class NotBe
    {
        [Fact]
        public void Should_NotFail_WhenNotEqualToExpected()
        {
            const int subject = 77, expected = 99;
            var action = () => subject.Must().NotBe(expected);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenEqualToExpected()
        {
            const int subject = 99, expected = 99;
            var action = () => subject.Must().NotBe(expected);
            action.Must().Throw();
        }
    }

    public class BePositive
    {
        [Fact]
        public void Should_Fail_WhenZero()
        {
            const int subject = 0;
            var action = () => subject.Must().BePositive();
            action.Must().Throw();
        }

        [Fact]
        public void Should_Fail_WhenLessThanZero()
        {
            const int subject = -1;
            var action = () => subject.Must().BePositive();
            action.Must().Throw();
        }

        [Fact]
        public void Should_NotFail_WhenGreaterThanZero()
        {
            const int subject = 9;
            var action = () => subject.Must().BePositive();
            action.Must().NotThrow();
        }
    }

    public class BeNegative
    {
        [Fact]
        public void Should_Fail_WhenZero()
        {
            const int subject = 0;
            var action = () => subject.Must().BeNegative();
            action.Must().Throw();
        }

        [Fact]
        public void Should_NotFail_WhenLessThanZero()
        {
            const int subject = -1;
            var action = () => subject.Must().BeNegative();
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenGreaterThanZero()
        {
            const int subject = 9;
            var action = () => subject.Must().BeNegative();
            action.Must().Throw();
        }
    }

    public class BeLessThan
    {
        [Fact]
        public void Should_Fail_WhenSameAsComparison()
        {
            const int subject = 9, comparison = 9;
            var action = () => subject.Must().BeLessThan(comparison);
            action.Must().Throw();
        }

        [Fact]
        public void Should_NotFail_WhenLessThanComparison()
        {
            const int subject = 8, comparison = 9;
            var action = () => subject.Must().BeLessThan(comparison);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenGreaterThanComparison()
        {
            const int subject = 10, comparison = 9;
            var action = () => subject.Must().BeLessThan(comparison);
            action.Must().Throw();
        }
    }

    public class BeLessThanOrEqualTo
    {
        [Fact]
        public void Should_NotFail_WhenSameAsComparison()
        {
            const int subject = 9, comparison = 9;
            var action = () => subject.Must().BeLessThanOrEqualTo(comparison);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_NotFail_WhenLessThanComparison()
        {
            const int subject = 8, comparison = 9;
            var action = () => subject.Must().BeLessThanOrEqualTo(comparison);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenGreaterThanComparison()
        {
            const int subject = 10, comparison = 9;
            var action = () => subject.Must().BeLessThanOrEqualTo(comparison);
            action.Must().Throw();
        }
    }

    public class BeGreaterThan
    {
        [Fact]
        public void Should_Fail_WhenSameAsComparison()
        {
            const int subject = 9, comparison = 9;
            var action = () => subject.Must().BeGreaterThan(comparison);
            action.Must().Throw();
        }

        [Fact]
        public void Should_Fail_WhenLessThanComparison()
        {
            const int subject = 8, comparison = 9;
            var action = () => subject.Must().BeGreaterThan(comparison);
            action.Must().Throw();
        }

        [Fact]
        public void Should_NotFail_WhenGreaterThanComparison()
        {
            const int subject = 10, comparison = 9;
            var action = () => subject.Must().BeGreaterThan(comparison);
            action.Must().NotThrow();
        }
    }

    public class BeGreaterThanOrEqualTo
    {
        [Fact]
        public void Should_NotFail_WhenSameAsComparison()
        {
            const int subject = 9, comparison = 9;
            var action = () => subject.Must().BeGreaterThanOrEqualTo(comparison);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenLessThanComparison()
        {
            const int subject = 8, comparison = 9;
            var action = () => subject.Must().BeGreaterThanOrEqualTo(comparison);
            action.Must().Throw();
        }

        [Fact]
        public void Should_NotFail_WhenGreaterThanComparison()
        {
            const int subject = 10, comparison = 9;
            var action = () => subject.Must().BeGreaterThanOrEqualTo(comparison);
            action.Must().NotThrow();
        }
    }

    public class BeInRange
    {
        private const int Lower = 10, Upper = 20;

        [Fact]
        public void Should_NotFail_WhenInRange()
        {
            const int subject = 15;
            var action = () => subject.Must().BeInRange(Lower, Upper);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_NotFail_WhenEqualToLowerBound()
        {
            var action = () => Lower.Must().BeInRange(Lower, Upper);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenLessThanLowerBound()
        {
            const int subject = 5;
            var action = () => subject.Must().BeInRange(Lower, Upper);
            action.Must().Throw();
        }

        [Fact]
        public void Should_NotFail_WhenEqualToUpperBound()
        {
            var action = () => Upper.Must().BeInRange(Lower, Upper);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenGreaterThanUpperBound()
        {
            const int subject = 25;
            var action = () => subject.Must().BeInRange(Lower, Upper);
            action.Must().Throw();
        }
    }

    public class NotBeInRange
    {
        private const int Lower = 10, Upper = 20;

        [Fact]
        public void Should_Fail_WhenInRange()
        {
            const int subject = 15;
            var action = () => subject.Must().NotBeInRange(Lower, Upper);
            action.Must().Throw();
        }

        [Fact]
        public void Should_Fail_WhenEqualToLowerBound()
        {
            var action = () => Lower.Must().NotBeInRange(Lower, Upper);
            action.Must().Throw();
        }

        [Fact]
        public void Should_NotFail_WhenLessThanLowerBound()
        {
            const int subject = 5;
            var action = () => subject.Must().NotBeInRange(Lower, Upper);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenEqualToUpperBound()
        {
            var action = () => Upper.Must().NotBeInRange(Lower, Upper);
            action.Must().Throw();
        }

        [Fact]
        public void Should_Fail_WhenGreaterThanUpperBound()
        {
            const int subject = 25;
            var action = () => subject.Must().NotBeInRange(Lower, Upper);
            action.Must().NotThrow();
        }
    }

    public class Match
    {
        [Fact]
        public void Should_NotFail_WhenSubjectSatisfiesPredicate()
        {
            const int subject = 72;
            var action = () => subject.Must().Match(i => i == 72);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenSubjectDoesNotSatisfyPredicate()
        {
            const int subject = 72;
            var action = () => subject.Must().Match(i => i == 71);
            action.Must().Throw();
        }
    }
}