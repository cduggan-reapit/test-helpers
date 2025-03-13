namespace Reapit.Platform.Testing.UnitTests.Fluent.Assertions.Primitives;

public static class NullableDateTimeOffsetAssertionsTests
{
    private static DateTimeOffset? NullDate => null;

    /*
     * NullableDateTimeOffsetAssertions is an extension of DateTimeOffsetAssertions.  For inherited methods, this class
     * will only test the null case as this cannot be exercised in the DateTimeOffsetAssertions class.
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
            DateTimeOffset? subject = DateTimeOffset.MaxValue;
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
            DateTimeOffset? subject = DateTimeOffset.MinValue;
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
            DateTimeOffset? subject = DateTimeOffset.MinValue;
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
            DateTimeOffset? subject = DateTimeOffset.UnixEpoch;
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
            var action = () => NullDate.Must().Be((DateTimeOffset?)DateTimeOffset.Now);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectNull_AndComparisonNull()
            => NullDate.Must().Be(null);

        [Fact]
        public void Should_NotFail_WhenSubjectEqual_ToNullableComparison()
        {
            DateTimeOffset? subject = DateTimeOffset.UnixEpoch;
            subject.Must().Be((DateTimeOffset?)subject.Value);
        }

        [Fact]
        public void Should_Fail_WhenSubjectNotEqual_ToNullableComparison()
        {
            DateTimeOffset? subject = DateTimeOffset.UnixEpoch;
            var action = () => subject.Must().Be((DateTimeOffset?)DateTimeOffset.UtcNow);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenSubjectNotEqual_ToNullComparison()
        {
            var action = () => NullDate.Must().Be((DateTimeOffset?)DateTimeOffset.Now);
            action.Must().Throw<XunitException>();
        }
    }

    public class NotBe
    {
        [Fact]
        public void Should_NotFail_WhenSubjectNull_AndComparisonNotNull()
        {
            NullDate.Must().NotBe((DateTimeOffset?)DateTimeOffset.Now);
        }

        [Fact]
        public void Should_Fail_WhenSubjectNull_AndComparisonNull()
        {
            var action = () => NullDate.Must().NotBe(null);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenSubjectEqual_ToNullableComparison()
        {
            DateTimeOffset? subject = DateTimeOffset.UnixEpoch;
            var action = () => subject.Must().NotBe((DateTimeOffset?)subject.Value);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectNotEqual_ToNullableComparison()
        {
            DateTimeOffset? subject = DateTimeOffset.UnixEpoch;
            subject.Must().NotBe((DateTimeOffset?)DateTimeOffset.UtcNow);
        }

        [Fact]
        public void Should_NotFail_WhenSubjectNotEqual_ToNullComparison()
            => NullDate.Must().NotBe((DateTimeOffset?)DateTimeOffset.Now);
    }

    public class BeCloseTo
    {
        [Fact]
        public void Should_Fail_WhenSubjectNull()
        {
            var action = () => NullDate.Must().BeCloseTo(DateTimeOffset.UnixEpoch, TimeSpan.FromDays(1_000));
            action.Must().Throw<XunitException>();
        }
    }

    public class NotBeCloseTo
    {
        [Fact]
        public void Should_NotFail_WhenSubjectNull()
        {
            // If it's null, by definition it is not close to the value.
            NullDate.Must().NotBeCloseTo(DateTimeOffset.UnixEpoch, TimeSpan.FromDays(1_000));
        }
    }

    public class BeAfter
    {
        [Fact]
        public void Should_Fail_WhenSubjectNull()
        {
            var action = () => NullDate.Must().BeAfter(DateTimeOffset.MinValue);
            action.Must().Throw<XunitException>();
        }
    }

    public class BeOnOrAfter
    {
        [Fact]
        public void Should_Fail_WhenSubjectNull()
        {
            var action = () => NullDate.Must().BeOnOrAfter(DateTimeOffset.MinValue);
            action.Must().Throw<XunitException>();
        }
    }

    public class BeBefore
    {
        [Fact]
        public void Should_Fail_WhenSubjectNull()
        {
            var action = () => NullDate.Must().BeBefore(DateTimeOffset.MaxValue);
            action.Must().Throw<XunitException>();
        }
    }

    public class BeOnOrBefore
    {
        [Fact]
        public void Should_Fail_WhenSubjectNull()
        {
            var action = () => NullDate.Must().BeOnOrBefore(DateTimeOffset.MaxValue);
            action.Must().Throw<XunitException>();
        }
    }

    public class Match
    {
        /*
         * VS/ReSharper/Rider will flag the DateTimeOffset/DateTimeOffset? type specifications in the expressions as "not required"
         * but I've intentionally left them in to make it easier to know whether we're testing the inherited or declared
         * version of the method.
         */

        [Fact]
        public void Should_NotFail_WhenPredicateSatisfied()
        {
            DateTimeOffset? subject = DateTimeOffset.UtcNow;
            subject.Must().Match(dt => dt.Year > 1970);
        }

        [Fact]
        public void Should_Fail_WhenPredicateNotSatisfied()
        {
            DateTimeOffset? subject = DateTimeOffset.UtcNow;
            var action = () => subject.Must().Match(dt => dt.Year > DateTimeOffset.UtcNow.Year + 1);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenNullablePredicateSatisfied()
        {
            DateTimeOffset? subject = DateTimeOffset.UtcNow;
            subject.Must().Match(dt => dt.HasValue && dt.Value.Year > 1970);
        }

        [Fact]
        public void Should_Fail_WhenNullablePredicateNotSatisfied()
        {
            DateTimeOffset? subject = DateTimeOffset.UtcNow;
            var action = () => subject.Must().Match(dt => dt.HasValue && dt.Value.Year > DateTimeOffset.UtcNow.Year + 1);
            action.Must().Throw<XunitException>();
        }
    }
}