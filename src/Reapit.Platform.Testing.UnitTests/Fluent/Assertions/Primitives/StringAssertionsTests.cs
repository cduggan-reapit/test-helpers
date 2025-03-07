namespace Reapit.Platform.Testing.UnitTests.Fluent.Assertions.Primitives;

public static class StringAssertionsTests
{
    // Avoid having to define the same things over and over again
    private const string PotterString = "Mr and Mrs Dursley, of number four, Privet Drive";
    private const string LoremString = "Maecenas vel accumsan diam, sit amet tincidunt nunc.";
    private const string NullString = null;
    private const string EmptyString = "";
    private const string WhiteSpaceString = "     ";

    public class Be
    {
        [Fact]
        public void Should_Fail_WhenSubjectNull()
        {
            var action = () => NullString.Must().Be(LoremString);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenSubjectNotEqual()
        {
            var action = () => LoremString.ToLowerInvariant().Must().Be(LoremString);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectEqual()
        {
            var action = () => LoremString.Must().Be(LoremString);
            action.Must().NotThrow();
        }
    }

    public class NotBe
    {
        [Fact]
        public void Should_NotFail_WhenSubjectNull()
        {
            var action = () => NullString.Must().NotBe(LoremString);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectNotEqual()
        {
            var action = () => LoremString.ToLowerInvariant().Must().NotBe(LoremString);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenSubjectEqual()
        {
            var action = () => LoremString.Must().NotBe(LoremString);
            action.Must().Throw<XunitException>();
        }
    }

    public class BeEquivalentTo
    {
        [Fact]
        public void Should_Fail_WhenSubjectNull()
        {
            var action = () => NullString.Must().BeEquivalentTo(LoremString);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenSubjectNotEquivalent()
        {
            var action = () => PotterString.Must().BeEquivalentTo(LoremString);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectEquivalent()
        {
            var action = () => LoremString.ToLowerInvariant().Must().BeEquivalentTo(LoremString);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectEqual()
        {
            var action = () => LoremString.Must().BeEquivalentTo(LoremString);
            action.Must().NotThrow();
        }
    }

    public class NotBeEquivalentTo
    {
        [Fact]
        public void Should_NotFail_WhenSubjectNull()
        {
            var action = () => NullString.Must().NotBeEquivalentTo(LoremString);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectNotEquivalent()
        {
            var action = () => PotterString.Must().NotBeEquivalentTo(LoremString);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenSubjectEquivalent()
        {
            var action = () => LoremString.ToLowerInvariant().Must().NotBeEquivalentTo(LoremString);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenSubjectEqual()
        {
            var action = () => LoremString.Must().NotBeEquivalentTo(LoremString);
            action.Must().Throw<XunitException>();
        }
    }

    public class BeEmpty
    {
        [Fact]
        public void Should_Fail_WhenSubjectNull()
        {
            var action = () => NullString.Must().BeEmpty();
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenSubjectWhiteSpace()
        {
            var action = () => WhiteSpaceString.Must().BeEmpty();
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectEmpty()
        {
            var action = () => EmptyString.Must().BeEmpty();
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenSubjectNotEmpty()
        {
            var action = () => PotterString.Must().BeEmpty();
            action.Must().Throw<XunitException>();
        }
    }

    public class NotBeEmpty
    {
        [Fact]
        public void Should_NotFail_WhenSubjectNull()
        {
            var action = () => NullString.Must().NotBeEmpty();
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectWhiteSpace()
        {
            var action = () => WhiteSpaceString.Must().NotBeEmpty();
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenSubjectEmpty()
        {
            var action = () => EmptyString.Must().NotBeEmpty();
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectNotEmpty()
        {
            var action = () => PotterString.Must().NotBeEmpty();
            action.Must().NotThrow();
        }
    }

    public class HaveLength
    {
        [Fact]
        public void Should_Fail_WhenSubjectNull()
        {
            var action = () => NullString.Must().HaveLength(5);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenSubjectDoesNotMatchLength()
        {
            var action = () => PotterString.Must().HaveLength(5);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectMatchesLength()
        {
            var action = () => LoremString.Must().HaveLength(52);
            action.Must().NotThrow();
        }
    }

    public class BeNullOrEmpty
    {
        [Fact]
        public void Should_NotFail_WhenSubjectNull()
        {
            var action = () => NullString.Must().BeNullOrEmpty();
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenSubjectWhiteSpace()
        {
            var action = () => WhiteSpaceString.Must().BeNullOrEmpty();
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectEmpty()
        {
            var action = () => EmptyString.Must().BeNullOrEmpty();
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenSubjectNotEmpty()
        {
            var action = () => PotterString.Must().BeNullOrEmpty();
            action.Must().Throw<XunitException>();
        }
    }

    public class NotBeNullOrEmpty
    {
        [Fact]
        public void Should_NotFail_WhenSubjectNull()
        {
            var action = () => NullString.Must().NotBeNullOrEmpty();
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectWhiteSpace()
        {
            var action = () => WhiteSpaceString.Must().NotBeNullOrEmpty();
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenSubjectEmpty()
        {
            var action = () => EmptyString.Must().NotBeNullOrEmpty();
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectNotEmpty()
        {
            var action = () => PotterString.Must().NotBeNullOrEmpty();
            action.Must().NotThrow();
        }
    }

    public class BeNullOrWhiteSpace
    {
        [Fact]
        public void Should_NotFail_WhenSubjectNull()
        {
            var action = () => NullString.Must().BeNullOrWhiteSpace();
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenSubjectWhiteSpace()
        {
            var action = () => WhiteSpaceString.Must().BeNullOrWhiteSpace();
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectEmpty()
        {
            var action = () => EmptyString.Must().BeNullOrWhiteSpace();
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenSubjectNotEmpty()
        {
            var action = () => PotterString.Must().BeNullOrWhiteSpace();
            action.Must().Throw<XunitException>();
        }
    }

    public class NotBeNullOrWhiteSpace
    {
        [Fact]
        public void Should_NotFail_WhenSubjectNull()
        {
            var action = () => NullString.Must().NotBeNullOrWhiteSpace();
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectWhiteSpace()
        {
            var action = () => WhiteSpaceString.Must().NotBeNullOrWhiteSpace();
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenSubjectEmpty()
        {
            var action = () => EmptyString.Must().NotBeNullOrWhiteSpace();
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectNotEmpty()
        {
            var action = () => PotterString.Must().NotBeNullOrWhiteSpace();
            action.Must().NotThrow();
        }
    }

    public class StartWith
    {
        [Fact]
        public void Should_Fail_WhenSubjectNull()
        {
            var action = () => NullString.Must().StartWith(PotterString[..10]);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenSubjectDifferent_AndNoComparisonProvided()
        {
            var action = () => PotterString.Must().StartWith(LoremString[..10]);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenSubjectDifferentCase_AndNoComparisonProvided()
        {
            var action = () => PotterString.Must().StartWith(PotterString[..10].ToLowerInvariant());
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectMatches_AndNoComparisonProvided()
        {
            var action = () => PotterString.Must().StartWith(PotterString[..10]);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenSubjectDifferent_AndComparisonProvided()
        {
            var action = () => PotterString.Must().StartWith(LoremString[..10], StringComparison.OrdinalIgnoreCase);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenSubjectDifferentCase_AndCaseAwareComparisonProvided()
        {
            var action = () => PotterString.Must().StartWith(PotterString[..10].ToLowerInvariant(), StringComparison.Ordinal);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectDifferentCase_AndCaseInsensitiveComparisonProvided()
        {
            var action = () => PotterString.Must().StartWith(PotterString[..10].ToLowerInvariant(), StringComparison.OrdinalIgnoreCase);
            action.Must().NotThrow();
        }
    }

    public class NotStartWith
    {
        [Fact]
        public void Should_NotFail_WhenSubjectNull()
        {
            var action = () => NullString.Must().NotStartWith(PotterString[..10]);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectDifferent_AndNoComparisonProvided()
        {
            var action = () => PotterString.Must().NotStartWith(LoremString[..10]);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectDifferentCase_AndNoComparisonProvided()
        {
            var action = () => PotterString.Must().NotStartWith(PotterString[..10].ToLowerInvariant());
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenSubjectMatches_AndNoComparisonProvided()
        {
            var action = () => PotterString.Must().NotStartWith(PotterString[..10]);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectDifferent_AndComparisonProvided()
        {
            var action = () => PotterString.Must().NotStartWith(LoremString[..10], StringComparison.OrdinalIgnoreCase);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectDifferentCase_AndCaseAwareComparisonProvided()
        {
            var action = () => PotterString.Must().NotStartWith(PotterString[..10].ToLowerInvariant(), StringComparison.Ordinal);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenSubjectDifferentCase_AndCaseInsensitiveComparisonProvided()
        {
            var action = () => PotterString.Must().NotStartWith(PotterString[..10].ToLowerInvariant(), StringComparison.OrdinalIgnoreCase);
            action.Must().Throw<XunitException>();
        }
    }

    public class EndWith
    {
        [Fact]
        public void Should_Fail_WhenSubjectNull()
        {
            var action = () => NullString.Must().EndWith(PotterString[^10..]);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenSubjectDifferent_AndNoComparisonProvided()
        {
            var action = () => PotterString.Must().EndWith(LoremString[^10..]);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenSubjectDifferentCase_AndNoComparisonProvided()
        {
            var action = () => PotterString.Must().EndWith(PotterString[^10..].ToLowerInvariant());
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectMatches_AndNoComparisonProvided()
        {
            var action = () => PotterString.Must().EndWith(PotterString[^10..]);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenSubjectDifferent_AndComparisonProvided()
        {
            var action = () => PotterString.Must().EndWith(LoremString[^10..], StringComparison.OrdinalIgnoreCase);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenSubjectDifferentCase_AndCaseAwareComparisonProvided()
        {
            var action = () => PotterString.Must().EndWith(PotterString[^10..].ToLowerInvariant(), StringComparison.Ordinal);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectDifferentCase_AndCaseInsensitiveComparisonProvided()
        {
            var action = () => PotterString.Must().EndWith(PotterString[^10..].ToLowerInvariant(), StringComparison.OrdinalIgnoreCase);
            action.Must().NotThrow();
        }
    }

    public class NotEndWith
    {
        [Fact]
        public void Should_NotFail_WhenSubjectNull()
        {
            var action = () => NullString.Must().NotEndWith(PotterString[^10..]);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectDifferent_AndNoComparisonProvided()
        {
            var action = () => PotterString.Must().NotEndWith(LoremString[^10..]);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectDifferentCase_AndNoComparisonProvided()
        {
            var action = () => PotterString.Must().NotEndWith(PotterString[^10..].ToLowerInvariant());
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenSubjectMatches_AndNoComparisonProvided()
        {
            var action = () => PotterString.Must().NotEndWith(PotterString[^10..]);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectDifferent_AndComparisonProvided()
        {
            var action = () => PotterString.Must().NotEndWith(LoremString[^10..], StringComparison.OrdinalIgnoreCase);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectDifferentCase_AndCaseAwareComparisonProvided()
        {
            var action = () => PotterString.Must().NotEndWith(PotterString[^10..].ToLowerInvariant(), StringComparison.Ordinal);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenSubjectDifferentCase_AndCaseInsensitiveComparisonProvided()
        {
            var action = () => PotterString.Must().NotEndWith(PotterString[^10..].ToLowerInvariant(), StringComparison.OrdinalIgnoreCase);
            action.Must().Throw<XunitException>();
        }
    }

    public class Contain
    {
        [Fact]
        public void Should_Fail_WhenSubjectNull()
        {
            var action = () => NullString.Must().Contain(PotterString[5..^5]);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenSubjectDifferent_AndNoComparisonProvided()
        {
            var action = () => PotterString.Must().Contain(LoremString[5..^5]);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenSubjectDifferentCase_AndNoComparisonProvided()
        {
            var action = () => PotterString.Must().Contain(PotterString[5..^5].ToLowerInvariant());
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectMatches_AndNoComparisonProvided()
        {
            var action = () => PotterString.Must().Contain(PotterString[5..^5]);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenSubjectDifferent_AndComparisonProvided()
        {
            var action = () => PotterString.Must().Contain(LoremString[5..^5], StringComparison.OrdinalIgnoreCase);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenSubjectDifferentCase_AndCaseAwareComparisonProvided()
        {
            var action = () => PotterString.Must().Contain(PotterString[5..^5].ToLowerInvariant(), StringComparison.Ordinal);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectDifferentCase_AndCaseInsensitiveComparisonProvided()
        {
            var action = () => PotterString.Must().Contain(PotterString[5..^5].ToLowerInvariant(), StringComparison.OrdinalIgnoreCase);
            action.Must().NotThrow();
        }
    }

    public class NotContain
    {
        [Fact]
        public void Should_NotFail_WhenSubjectNull()
        {
            var action = () => NullString.Must().NotContain(PotterString[5..^5]);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectDifferent_AndNoComparisonProvided()
        {
            var action = () => PotterString.Must().NotContain(LoremString[5..^5]);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectDifferentCase_AndNoComparisonProvided()
        {
            var action = () => PotterString.Must().NotContain(PotterString[5..^5].ToLowerInvariant());
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenSubjectMatches_AndNoComparisonProvided()
        {
            var action = () => PotterString.Must().NotContain(PotterString[5..^5]);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectDifferent_AndComparisonProvided()
        {
            var action = () => PotterString.Must().NotContain(LoremString[5..^5], StringComparison.OrdinalIgnoreCase);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectDifferentCase_AndCaseAwareComparisonProvided()
        {
            var action = () => PotterString.Must().NotContain(PotterString[5..^5].ToLowerInvariant(), StringComparison.Ordinal);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenSubjectDifferentCase_AndCaseInsensitiveComparisonProvided()
        {
            var action = () => PotterString.Must().NotContain(PotterString[5..^5].ToLowerInvariant(), StringComparison.OrdinalIgnoreCase);
            action.Must().Throw<XunitException>();
        }
    }

    /*
     * Inherited Assertions (ReferenceTypeAssertions)
     * We'll only test the ones relevant to `string` here (BeOfType and BeAssignableTo are somewhat moot in this context)
     */

    public class BeNull
    {
        [Fact]
        public void Should_Fail_WhenSubjectNotNull()
        {
            var action = () => LoremString.Must().BeNull();
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectNull()
        {
            var action = () => NullString.Must().BeNull();
            action.Must().NotThrow();
        }
    }

    public class NotBeNull
    {
        [Fact]
        public void Should_NotFail_WhenSubjectNotNull()
        {
            var action = () => LoremString.Must().NotBeNull();
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenSubjectNull()
        {
            var action = () => NullString.Must().NotBeNull();
            action.Must().Throw<XunitException>();
        }
    }

    public class Match
    {
        [Fact]
        public void Should_Fail_WhenSubjectNull()
        {
            var action = () => NullString.Must().Match(s => true);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenSubjectDoesNotSatisfyPredicate()
        {
            var action = () => LoremString.Must().Match(s => s == NullString);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenSubjectSatisfiesPredicate()
        {
            var action = () => LoremString.Must().Match(s => s == LoremString);
            action.Must().NotThrow();
        }
    }
}