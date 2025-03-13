namespace Reapit.Platform.Testing.UnitTests.Fluent.Assertions.Primitives;

public static class DateTimeOffsetAssertionsTests
{
    public class Be
    {
        [Fact]
        public void Should_NotFail_WhenSubjectEqual()
        {
            var subject = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
            subject.Must().Be(DateTimeOffset.UnixEpoch);
        }

        [Fact]
        public void Should_Fail_WhenSubjectNotEqual()
        {
            var subject = DateTimeOffset.UtcNow;
            var action = () => subject.Must().Be(DateTimeOffset.UnixEpoch);
            action.Must().Throw<XunitException>();
        }
    }

    public class NotBe
    {
        [Fact]
        public void Should_Fail_WhenSubjectEqual()
        {
            var subject = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
            var action = () => subject.Must().NotBe(DateTimeOffset.UnixEpoch);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectNotEqual()
        {
            var subject = DateTimeOffset.UtcNow;
            subject.Must().NotBe(DateTimeOffset.UnixEpoch);
        }
    }

    public class BeCloseTo
    {
        [Fact]
        public void Should_Throw_WhenPrecisionNegative()
        {
            var action = () => DateTimeOffset.UnixEpoch.Must().BeCloseTo(DateTimeOffset.UnixEpoch, TimeSpan.FromDays(-1));
            action.Must().ThrowExactly<ArgumentException>();
        }

        [Fact]
        public void Should_Fail_WhenSubjectBeforePrecision()
        {
            var subject = new DateTimeOffset(1970, 1, 1, 1, 1, 1, TimeSpan.Zero);
            var compareTo = subject.AddDays(11);
            var action = () => subject.Must().BeCloseTo(compareTo, TimeSpan.FromDays(10));
            action.Must().ThrowExactly<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectExactlyPrecisionBefore()
        {
            var subject = new DateTimeOffset(1970, 1, 1, 1, 1, 1, TimeSpan.Zero);
            var compareTo = subject.AddDays(-10);
            subject.Must().BeCloseTo(compareTo, TimeSpan.FromDays(10));
        }

        [Fact]
        public void Should_NotFail_WhenSubjectInRange()
        {
            var subject = new DateTimeOffset(1970, 1, 1, 1, 1, 1, TimeSpan.Zero);
            subject.Must().BeCloseTo(subject, TimeSpan.FromMilliseconds(1));
        }

        [Fact]
        public void Should_NotFail_WhenSubjectExactlyPrecisionAfter()
        {
            var subject = new DateTimeOffset(1970, 1, 1, 1, 1, 1, TimeSpan.Zero);
            var compareTo = subject.AddDays(10);
            subject.Must().BeCloseTo(compareTo, TimeSpan.FromDays(10));
        }

        [Fact]
        public void Should_Fail_WhenSubjectAfterPrecision()
        {
            var subject = new DateTimeOffset(1970, 1, 1, 1, 1, 1, TimeSpan.Zero);
            var compareTo = subject.AddDays(-11);
            var action = () => subject.Must().BeCloseTo(compareTo, TimeSpan.FromDays(10));
            action.Must().ThrowExactly<XunitException>();
        }
    }

    public class NotBeCloseTo
    {
        [Fact]
        public void Should_Throw_WhenPrecisionNegative()
        {
            var action = () => DateTimeOffset.UnixEpoch.Must().NotBeCloseTo(DateTimeOffset.UnixEpoch, TimeSpan.FromDays(-1));
            action.Must().ThrowExactly<ArgumentException>();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectBeforePrecision()
        {
            var subject = new DateTimeOffset(1970, 1, 1, 1, 1, 1, TimeSpan.Zero);
            var compareTo = subject.AddDays(11);
            subject.Must().NotBeCloseTo(compareTo, TimeSpan.FromDays(10));
        }

        [Fact]
        public void Should_Fail_WhenSubjectExactlyPrecisionBefore()
        {
            var subject = new DateTimeOffset(1970, 1, 1, 1, 1, 1, TimeSpan.Zero);
            var compareTo = subject.AddDays(-10);
            var action = () => subject.Must().NotBeCloseTo(compareTo, TimeSpan.FromDays(10));
            action.Must().ThrowExactly<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenSubjectInRange()
        {
            var subject = new DateTimeOffset(1970, 1, 1, 1, 1, 1, TimeSpan.Zero);
            var action = () => subject.Must().NotBeCloseTo(subject, TimeSpan.FromMilliseconds(1));
            action.Must().ThrowExactly<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenSubjectExactlyPrecisionAfter()
        {
            var subject = new DateTimeOffset(1970, 1, 1, 1, 1, 1, TimeSpan.Zero);
            var compareTo = subject.AddDays(10);
            var action = () => subject.Must().NotBeCloseTo(compareTo, TimeSpan.FromDays(10));
            action.Must().ThrowExactly<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectAfterPrecision()
        {
            var subject = new DateTimeOffset(1970, 1, 1, 1, 1, 1, TimeSpan.Zero);
            var compareTo = subject.AddDays(-11);
            subject.Must().NotBeCloseTo(compareTo, TimeSpan.FromDays(10));
        }
    }

    public class BeAfter
    {
        [Fact]
        public void Should_Fail_WhenSubjectBeforeComparison()
        {
            var comparison = DateTimeOffset.UtcNow;
            var subject = comparison.AddMinutes(-1);
            var action = () => subject.Must().BeAfter(comparison);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenSubjectEqualToComparison()
        {
            var comparison = DateTimeOffset.UtcNow;
            var subject = ((DateTimeOffset?)comparison).Value;
            var action = () => subject.Must().BeAfter(comparison);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectAfterComparison()
        {
            var comparison = DateTimeOffset.UtcNow;
            var subject = comparison.AddMinutes(1);
            subject.Must().BeAfter(comparison);
        }
    }

    public class BeOnOrAfter
    {
        [Fact]
        public void Should_Fail_WhenSubjectBeforeComparison()
        {
            var comparison = DateTimeOffset.UtcNow;
            var subject = comparison.AddMinutes(-1);
            var action = () => subject.Must().BeOnOrAfter(comparison);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectEqualToComparison()
        {
            var comparison = DateTimeOffset.UtcNow;
            var subject = ((DateTimeOffset?)comparison).Value;
            subject.Must().BeOnOrAfter(comparison);
        }

        [Fact]
        public void Should_NotFail_WhenSubjectAfterComparison()
        {
            var comparison = DateTimeOffset.UtcNow;
            var subject = comparison.AddMinutes(1);
            subject.Must().BeOnOrAfter(comparison);
        }
    }

    public class BeBefore
    {
        [Fact]
        public void Should_NotFail_WhenSubjectBeforeComparison()
        {
            var comparison = DateTimeOffset.UtcNow;
            var subject = comparison.AddMinutes(-1);
            subject.Must().BeBefore(comparison);
        }

        [Fact]
        public void Should_Fail_WhenSubjectEqualToComparison()
        {
            var comparison = DateTimeOffset.UtcNow;
            var subject = ((DateTimeOffset?)comparison).Value;
            var action = () => subject.Must().BeBefore(comparison);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenSubjectAfterComparison()
        {
            var comparison = DateTimeOffset.UtcNow;
            var subject = comparison.AddMinutes(1);
            var action = () => subject.Must().BeBefore(comparison);
            action.Must().Throw<XunitException>();
        }
    }

    public class BeOnOrBefore
    {
        [Fact]
        public void Should_NotFail_WhenSubjectBeforeComparison()
        {
            var comparison = DateTimeOffset.UtcNow;
            var subject = comparison.AddMinutes(-1);
            subject.Must().BeOnOrBefore(comparison);
        }

        [Fact]
        public void Should_NotFail_WhenSubjectEqualToComparison()
        {
            var comparison = DateTimeOffset.UtcNow;
            var subject = ((DateTimeOffset?)comparison).Value;
            subject.Must().BeOnOrBefore(comparison);
        }

        [Fact]
        public void Should_Fail_WhenSubjectAfterComparison()
        {
            var comparison = DateTimeOffset.UtcNow;
            var subject = comparison.AddMinutes(1);
            var action = () => subject.Must().BeOnOrBefore(comparison);
            action.Must().Throw<XunitException>();
        }
    }

    public class Match
    {
        [Fact]
        public void Should_NotFail_WhenPredicateSatisfied()
        {
            var subject = DateTimeOffset.UtcNow;
            subject.Must().Match(dt => dt.Year > 1970);
        }

        [Fact]
        public void Should_Fail_WhenPredicateNotSatisfied()
        {
            var subject = DateTimeOffset.UtcNow;
            var action = () => subject.Must().Match(dt => dt.Year > DateTimeOffset.UtcNow.Year + 1);
            action.Must().Throw<XunitException>();
        }
    }
}