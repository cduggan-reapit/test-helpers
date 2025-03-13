namespace Reapit.Platform.Testing.UnitTests.Fluent.Assertions.Primitives;

public class NullableNumericAssertionsTests
{
    private static T? NullValue<T>() where T : struct => null;

    /*
     * NullableNumericAssertions is an extension of NumericAssertions.  For inherited methods, this class will only test
     * the null case as this cannot be exercised in the NumericAssertionsTests class.
     */

    [Fact]
    public void Should_InitializeForByte() => ((byte?)0x01).Must();

    [Fact]
    public void Should_InitializeForSignedByte() => ((sbyte?)0x01).Must();

    [Fact]
    public void Should_InitializeForShort() => ((short?)1).Must();

    [Fact]
    public void Should_InitializeForUnsignedShort() => ((ushort?)1).Must();

    [Fact]
    public void Should_InitializeForInt32() => ((int?)1).Must();

    [Fact]
    public void Should_InitializeForUnsignedInt32() => ((uint?)1).Must();

    [Fact]
    public void Should_InitializeForLong() => ((long?)1).Must();

    [Fact]
    public void Should_InitializeForUnsignedLong() => ((ulong?)1).Must();

    [Fact]
    public void Should_InitializeForFloat() => ((float?)1).Must();

    [Fact]
    public void Should_InitializeForDouble() => ((double?)1).Must();

    [Fact]
    public void Should_InitializeForDecimal() => ((decimal?)1).Must();

    public class Be
    {
        [Fact]
        public void Should_Fail_WhenSubjectIsNull()
        {
            var subject = NullValue<int>();
            var action = () => subject.Must().Be(17);
            action.Must().Throw();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectIsNotNull_AndValueMatches()
        {
            int? subject = 17;
            var action = () => subject.Must().Be((int?)17);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenSubjectIsNotNull_AndValueDoesNotMatch()
        {
            int? subject = 18;
            var action = () => subject.Must().Be((int?)17);
            action.Must().Throw();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectIsNull_AndComparisonIsNull()
        {
            var subject = NullValue<int>();
            var action = () => subject.Must().Be(null);
            action.Must().NotThrow();
        }
    }

    public class NotBe
    {
        [Fact]
        public void Should_NotFail_WhenSubjectIsNull()
        {
            var subject = NullValue<int>();
            var action = () => subject.Must().NotBe(17);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenSubjectIsNotNull_AndValueMatches()
        {
            int? subject = 17;
            var action = () => subject.Must().NotBe((int?)17);
            action.Must().Throw();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectIsNotNull_AndValueDoesNotMatch()
        {
            int? subject = 18;
            var action = () => subject.Must().NotBe((int?)17);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenSubjectIsNull_AndComparisonIsNull()
        {
            var subject = NullValue<int>();
            var action = () => subject.Must().NotBe(null);
            action.Must().Throw();
        }
    }

    public class BePositive
    {
        [Fact]
        public void Should_Fail_WhenSubjectNull()
        {
            var subject = NullValue<byte>();
            var action = () => subject.Must().BePositive();
            action.Must().Throw();
        }
    }

    public class BeNegative
    {
        [Fact]
        public void Should_Fail_WhenSubjectNull()
        {
            var subject = NullValue<byte>();
            var action = () => subject.Must().BeNegative();
            action.Must().Throw();
        }
    }

    public class BeLessThan
    {
        [Fact]
        public void Should_Fail_WhenSubjectNull()
        {
            var subject = NullValue<sbyte>();
            var action = () => subject.Must().BeLessThan(50);
            action.Must().Throw();
        }
    }

    public class BeLessThanOrEqualTo
    {
        [Fact]
        public void Should_Fail_WhenSubjectNull()
        {
            var subject = NullValue<uint>();
            var action = () => subject.Must().BeLessThanOrEqualTo(49);
            action.Must().Throw();
        }
    }

    public class BeGreaterThan
    {
        [Fact]
        public void Should_Fail_WhenSubjectNull()
        {
            var subject = NullValue<uint>();
            var action = () => subject.Must().BeGreaterThan(9);
            action.Must().Throw();
        }
    }

    public class BeGreaterThanOrEqualTo
    {
        [Fact]
        public void Should_Fail_WhenSubjectNull()
        {
            var subject = NullValue<uint>();
            var action = () => subject.Must().BeGreaterThanOrEqualTo(10);
            action.Must().Throw();
        }
    }

    public class BeInRange
    {
        [Fact]
        public void Should_Fail_WhenSubjectNull()
        {
            var subject = NullValue<uint>();
            var action = () => subject.Must().BeInRange(1, 10);
            action.Must().Throw();
        }
    }

    public class NotBeInRange
    {
        [Fact]
        public void Should_Fail_WhenSubjectNull()
        {
            var subject = NullValue<uint>();
            var action = () => subject.Must().NotBeInRange(1, 10);
            action.Must().Throw();
        }
    }

    public class Match
    {
        // This is sneaky, because we tend to use it with an inline method it will generate Func<T?, bool>.
        [Fact]
        public void Should_Fail_WhenSubjectIsNull_AndDoesNotSatisfyPredicate()
        {
            var subject = NullValue<int>();
            var action = () => subject.Must().Match(i => i == 72);
            action.Must().Throw();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectSatisfiesPredicate()
        {
            int? subject = 72;
            var action = () => subject.Must().Match(i => i == 72);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenSubjectDoesNotSatisfyPredicate()
        {
            int? subject = 72;
            var action = () => subject.Must().Match(i => i == 71);
            action.Must().Throw();
        }
    }

    public class HaveValue
    {
        [Fact]
        public void Should_Fail_WhenSubjectIsNull()
        {
            var subject = NullValue<int>();
            var action = () => subject.Must().HaveValue();
            action.Must().Throw();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectHasValue()
        {
            int? subject = 7;
            var action = () => subject.Must().HaveValue();
            action.Must().NotThrow();
        }
    }

    public class NotHaveValue
    {
        [Fact]
        public void Should_NotFail_WhenSubjectIsNull()
        {
            var subject = NullValue<int>();
            var action = () => subject.Must().NotHaveValue();
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenSubjectHasValue()
        {
            int? subject = 7;
            var action = () => subject.Must().NotHaveValue();
            action.Must().Throw();
        }
    }

    public class BeNull
    {
        [Fact]
        public void Should_NotFail_WhenSubjectIsNull()
        {
            var subject = NullValue<int>();
            var action = () => subject.Must().BeNull();
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenSubjectHasValue()
        {
            int? subject = 7;
            var action = () => subject.Must().BeNull();
            action.Must().Throw();
        }
    }

    public class NotBeNull
    {
        [Fact]
        public void Should_Fail_WhenSubjectIsNull()
        {
            var subject = NullValue<int>();
            var action = () => subject.Must().NotBeNull();
            action.Must().Throw();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectHasValue()
        {
            int? subject = 7;
            var action = () => subject.Must().NotBeNull();
            action.Must().NotThrow();
        }
    }
}