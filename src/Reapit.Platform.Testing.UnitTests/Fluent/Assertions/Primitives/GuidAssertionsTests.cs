namespace Reapit.Platform.Testing.UnitTests.Fluent.Assertions.Primitives;

public static class GuidAssertionsTests
{
    public class Be
    {
        [Fact]
        public void Should_ThrowArgumentException_WhenCompareToIsInvalidGuidString()
        {
            var subject = Guid.NewGuid();
            var action = () => subject.Must().Be("this isn't a guid");
            action.Must().Throw<ArgumentException>();
        }

        [Fact]
        public void Should_Fail_WhenCompareToIsDifferentGuidString()
        {
            var subject = Guid.NewGuid();
            var action = () => subject.Must().Be("00000000-0000-0000-0000-000000000001");
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenCompareToIsMatchingGuidString()
        {
            var subject = new Guid("00000001-0002-0003-0004-000000000005");
            var action = () => subject.Must().Be("00000001-0002-0003-0004-000000000005");
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenSubjectNotEqual()
        {
            var subject = new Guid("00000001-0002-0003-0004-000000000005");
            var action = () => subject.Must().Be(Guid.Empty);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectEqual()
        {
            var subject = new Guid("00000001-0002-0003-0004-000000000005");
            var action = () => subject.Must().Be(new Guid(subject.ToString()));
            action.Must().NotThrow();
        }
    }

    public class NotBe
    {
        [Fact]
        public void Should_ThrowArgumentException_WhenCompareToIsInvalidGuidString()
        {
            var subject = Guid.NewGuid();
            var action = () => subject.Must().NotBe("this isn't a guid");
            action.Must().Throw<ArgumentException>();
        }

        [Fact]
        public void Should_NotFail_WhenCompareToIsDifferentGuidString()
        {
            var subject = Guid.NewGuid();
            var action = () => subject.Must().NotBe("00000000-0000-0000-0000-000000000001");
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenCompareToIsMatchingGuidString()
        {
            var subject = new Guid("00000001-0002-0003-0004-000000000005");
            var action = () => subject.Must().NotBe("00000001-0002-0003-0004-000000000005");
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectNotEqual()
        {
            var subject = new Guid("00000001-0002-0003-0004-000000000005");
            var action = () => subject.Must().NotBe(Guid.Empty);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenSubjectEqual()
        {
            var subject = new Guid("00000001-0002-0003-0004-000000000005");
            var action = () => subject.Must().NotBe(new Guid(subject.ToString()));
            action.Must().Throw<XunitException>();
        }
    }

    public class BeEmpty
    {
        [Fact]
        public void Should_Fail_WhenGuidNotEmpty()
        {
            var action = () => Guid.NewGuid().Must().BeEmpty();
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenGuidEmpty()
        {
            var action = () => Guid.Empty.Must().BeEmpty();
            action.Must().NotThrow();
        }
    }

    public class NotBeEmpty
    {
        [Fact]
        public void Should_NotFail_WhenGuidNotEmpty()
        {
            var action = () => Guid.NewGuid().Must().NotBeEmpty();
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenGuidEmpty()
        {
            var action = () => Guid.Empty.Must().NotBeEmpty();
            action.Must().Throw<XunitException>();
        }
    }
}