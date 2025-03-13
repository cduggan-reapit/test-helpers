namespace Reapit.Platform.Testing.UnitTests.Fluent.Assertions.Primitives;

public static class NullableDateTimeAssertionsTests
{
    private static DateTime? NullDate => null;

    /*
     * NullableDateTimeAssertions is an extension of DateTimeAssertions.  For inherited methods, this class will only
     * test the null case as this cannot be exercised in the DateTimeAssertions class.
     */

    public class HaveValue
    {
        [Fact]
        public void Should_Fail_WhenSubjectIsNull()
        {
            var action = () => NullDate.Must().HaveValue();
            action.Must().Throw();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectHasValue()
        {
            DateTime? subject = DateTime.MaxValue;
            var action = () => subject.Must().HaveValue();
            action.Must().NotThrow();
        }
    }

    public class NotHaveValue
    {
        [Fact]
        public void Should_NotFail_WhenSubjectIsNull()
        {
            var action = () => NullDate.Must().NotHaveValue();
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenSubjectHasValue()
        {
            DateTime? subject = DateTime.MinValue;
            var action = () => subject.Must().NotHaveValue();
            action.Must().Throw();
        }
    }

    public class BeNull
    {
        [Fact]
        public void Should_NotFail_WhenSubjectIsNull()
        {
            var action = () => NullDate.Must().BeNull();
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenSubjectHasValue()
        {
            DateTime? subject = DateTime.MinValue;
            var action = () => subject.Must().BeNull();
            action.Must().Throw();
        }
    }

    public class NotBeNull
    {
        [Fact]
        public void Should_Fail_WhenSubjectIsNull()
        {
            var action = () => NullDate.Must().NotBeNull();
            action.Must().Throw();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectHasValue()
        {
            DateTime? subject = DateTime.UnixEpoch;
            var action = () => subject.Must().NotBeNull();
            action.Must().NotThrow();
        }
    }

    /*
     * Inherited methods
     */

    public class Be
    {
        [Fact]
        public void Should_Fail_WhenSubjectNull_AndComparisonNotNull()
        {
            var action = () => NullDate.Must().Be((DateTime?)DateTime.Now);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectNull_AndComparisonNull()
            => NullDate.Must().Be(null);

        [Fact]
        public void Should_NotFail_WhenSubjectEqual_ToNullableComparison()
        {
            DateTime? subject = DateTime.UnixEpoch;
            subject.Must().Be((DateTime?)subject.Value);
        }

        [Fact]
        public void Should_Fail_WhenSubjectNotEqual_ToNullableComparison()
        {
            DateTime? subject = DateTime.UnixEpoch;
            var action = () => subject.Must().Be((DateTime?)DateTime.UtcNow);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenSubjectNotEqual_ToNullComparison()
        {
            var action = () => NullDate.Must().Be((DateTime?)DateTime.Now);
            action.Must().Throw<XunitException>();
        }
    }

    public class NotBe
    {
        [Fact]
        public void Should_NotFail_WhenSubjectNull_AndComparisonNotNull()
            => NullDate.Must().NotBe((DateTime?)DateTime.Now);

        [Fact]
        public void Should_Fail_WhenSubjectNull_AndComparisonNull()
        {
            var action = () => NullDate.Must().NotBe(null);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenSubjectEqual_ToNullableComparison()
        {
            DateTime? subject = DateTime.UnixEpoch;
            var action = () => subject.Must().NotBe((DateTime?)subject.Value);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectNotEqual_ToNullableComparison()
        {
            DateTime? subject = DateTime.UnixEpoch;
            subject.Must().NotBe((DateTime?)DateTime.UtcNow);
        }

        [Fact]
        public void Should_NotFail_WhenSubjectNotEqual_ToNullComparison()
            => NullDate.Must().NotBe((DateTime?)DateTime.Now);
    }

    public class BeCloseTo
    {
        [Fact]
        public void Should_Fail_WhenSubjectNull()
        {
            var action = () => NullDate.Must().BeCloseTo(DateTime.UnixEpoch, TimeSpan.FromDays(1_000));
            action.Must().Throw<XunitException>();
        }
    }

    public class NotBeCloseTo
    {
        [Fact]
        public void Should_NotFail_WhenSubjectNull()
        {
            // If it's null, by definition it is not close to the value.
            NullDate.Must().NotBeCloseTo(DateTime.UnixEpoch, TimeSpan.FromDays(1_000));
        }
    }

    public class BeAfter
    {
        [Fact]
        public void Should_Fail_WhenSubjectNull()
        {
            var action = () => NullDate.Must().BeAfter(DateTime.MinValue);
            action.Must().Throw<XunitException>();
        }
    }

    public class BeOnOrAfter
    {
        [Fact]
        public void Should_Fail_WhenSubjectNull()
        {
            var action = () => NullDate.Must().BeOnOrAfter(DateTime.MinValue);
            action.Must().Throw<XunitException>();
        }
    }

    public class BeBefore
    {
        [Fact]
        public void Should_Fail_WhenSubjectNull()
        {
            var action = () => NullDate.Must().BeBefore(DateTime.MaxValue);
            action.Must().Throw<XunitException>();
        }
    }

    public class BeOnOrBefore
    {
        [Fact]
        public void Should_Fail_WhenSubjectNull()
        {
            var action = () => NullDate.Must().BeOnOrBefore(DateTime.MaxValue);
            action.Must().Throw<XunitException>();
        }
    }

    public class Match
    {

        [Fact]
        public void Should_NotFail_WhenPredicateSatisfied()
        {
            DateTime? subject = DateTime.UtcNow;
            subject.Must().Match(dt => dt.Year > 1970);
        }

        [Fact]
        public void Should_Fail_WhenPredicateNotSatisfied()
        {
            DateTime? subject = DateTime.UtcNow;
            var action = () => subject.Must().Match(dt => dt.Year > DateTime.UtcNow.Year + 1);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenNullablePredicateSatisfied()
        {
            DateTime? subject = DateTime.UtcNow;
            subject.Must().Match(dt => dt.HasValue && dt.Value.Year > 1970);
        }

        [Fact]
        public void Should_Fail_WhenNullablePredicateNotSatisfied()
        {
            DateTime? subject = DateTime.UtcNow;
            var action = () => subject.Must().Match(dt => dt.HasValue && dt.Value.Year > DateTime.UtcNow.Year + 1);
            action.Must().Throw<XunitException>();
        }
    }
}