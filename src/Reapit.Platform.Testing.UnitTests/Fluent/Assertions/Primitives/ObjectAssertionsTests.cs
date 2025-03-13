using Microsoft.AspNetCore.Http;
using Reapit.Platform.Testing.Extensions;
using Reapit.Platform.Testing.Fluent.Core;

namespace Reapit.Platform.Testing.UnitTests.Fluent.Assertions.Primitives;

public static class ObjectAssertionsTests
{
    /*
     * Inherited methods
     */

    public class BeNull
    {
        [Fact]
        public void Should_NotFail_WhenObjectNull()
        {
            var action = () => (null as object).Must().BeNull();
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_NotFail_WhenObjectNotNull()
        {
            object input = new { Property = "value" };
            var action = () => input.Must().BeNull();
            action.Must().Throw<XunitException>();
        }
    }

    public class NotBeNull
    {
        [Fact]
        public void Should_Fail_WhenObjectNull()
        {
            var action = () => (null as object).Must().NotBeNull();
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenObjectNotNull()
        {
            object input = new { Property = "value" };
            var action = () => input.Must().NotBeNull();
            action.Must().NotThrow();
        }
    }

    public class BeOfType
    {
        [Fact]
        public void Should_Fail_WhenObjectNull()
        {
            var action = () => (null as object).Must().BeOfType<HttpContext>();
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenObjectNotOfType()
        {
            var input = new { Property = "value" };
            var action = () => input.Must().BeOfType<HttpContext>();
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenObjectOfType()
        {
            object input = new DefaultHttpContext();
            var action = () => input.Must().BeOfType<DefaultHttpContext>();
            action.Must().NotThrow();
        }
    }

    public class NotBeOfType
    {
        [Fact]
        public void Should_NotFail_WhenObjectNull()
        {
            var action = () => (null as object).Must().NotBeOfType<HttpContext>();
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_NotFail_WhenObjectNotOfType()
        {
            var input = new { Property = "value" };
            var action = () => input.Must().NotBeOfType<HttpContext>();
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenObjectOfType()
        {
            object input = new DefaultHttpContext();
            var action = () => input.Must().NotBeOfType<DefaultHttpContext>();
            action.Must().Throw<XunitException>();
        }
    }

    public class BeAssignableTo
    {
        [Fact]
        public void Should_Fail_WhenObjectNull()
        {
            var action = () => (null as object).Must().BeAssignableTo<HttpContext>();
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenObjectNotOfType()
        {
            var input = new { Property = "value" };
            var action = () => input.Must().BeAssignableTo<HttpContext>();
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenObjectOfDerivedType()
        {
            object input = new DefaultHttpContext();
            var action = () => input.Must().BeAssignableTo<HttpContext>();
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_NotFail_WhenObjectOfType()
        {
            object input = new DefaultHttpContext();
            var action = () => input.Must().BeAssignableTo<DefaultHttpContext>();
            action.Must().NotThrow();
        }
    }

    public class NotBeAssignableTo
    {
        [Fact]
        public void Should_NotFail_WhenObjectNull()
        {
            var action = () => (null as object).Must().NotBeAssignableTo<HttpContext>();
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_NotFail_WhenObjectNotOfType()
        {
            var input = new { Property = "value" };
            var action = () => input.Must().NotBeAssignableTo<HttpContext>();
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenObjectOfDerivedType()
        {
            object input = new DefaultHttpContext();
            var action = () => input.Must().NotBeAssignableTo<HttpContext>();
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenObjectOfType()
        {
            object input = new DefaultHttpContext();
            var action = () => input.Must().NotBeAssignableTo<DefaultHttpContext>();
            action.Must().Throw<XunitException>();
        }
    }

    public class BeEquivalentTo
    {
        [Fact]
        public void Should_NotFail_WhenSubjectNull_AndObjectNull()
        {
            var action = () => (null as object).Must().BeEquivalentTo(null);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenSubjectNull_AndObjectExpected()
        {
            var action = () => (null as object).Must().BeEquivalentTo(new { Property = "value" });
            action.Must().Throw<XunitException>().WithInnerException<EquivalenceException>();
        }

        [Fact]
        public void Should_Fail_WhenSubjectObject_AndNullExpected()
        {
            var action = () => new { Property = "value" }.Must().BeEquivalentTo(null);
            action.Must().Throw<XunitException>().WithInnerException<EquivalenceException>();
        }

        [Fact]
        public void Should_Fail_WhenObjectDepthExceedsMaximum()
        {
            var input = new[] { new { One = new { Two = new { Three = new { Four = new { Five = new { Six = new { Seven = new { Eight = new { Nine = new { Ten = new { Property = "value" } } } } } } } } } } } };
            var expected = new[] { new { One = new { Two = new { Three = new { Four = new { Five = new { Six = new { Seven = new { Eight = new { Nine = new { Ten = new { Property = "value" } } } } } } } } } } } }.ToList();

            var action = () => input.Must().BeEquivalentTo(expected);
            action.Must().Throw<XunitException>().Subject.Message.Must().StartWith("Exceeded the maximum depth 10");
        }

        [Fact]
        public void Should_Fail_WhenPrimitiveValuesNotEqual()
        {
            var input = new { Property = "one" };
            var expected = new { Property = 1 };
            var action = () => input.Must().BeEquivalentTo(expected);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenPrimitiveValuesEqual()
        {
            var input = new { Property = "one" };
            var expected = new { Property = "one" };
            var action = () => input.Must().BeEquivalentTo(expected);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_NotFail_WhenPrimitiveValuesEquivalent()
        {
            var input = new { Property = "1" };
            var expected = new { Property = 1 };
            var action = () => input.Must().BeEquivalentTo(expected);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenDateTimeValuesNotEqual()
        {
            var input = new { Property = new DateTime(2025, 3, 11, 11, 58, 37, DateTimeKind.Utc) };
            var expected = new { Property = DateTime.UnixEpoch };
            var action = () => input.Must().BeEquivalentTo(expected);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenDateTimeValuesEqual()
        {
            var input = new { Property = new DateTime(2025, 3, 11, 11, 58, 37, DateTimeKind.Utc) };
            var expected = new { Property = new DateTime(2025, 3, 11, 11, 58, 37, DateTimeKind.Utc) };
            var action = () => input.Must().BeEquivalentTo(expected);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenUriValuesNotEqual()
        {
            var input = new { Property = new Uri("https://example.net/one", UriKind.Absolute) };
            var expected = new { Property = new Uri("/one", UriKind.Relative) };
            var action = () => input.Must().BeEquivalentTo(expected);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenUriValuesEqual()
        {
            var input = new { Property = new Uri("https://example.net/one", UriKind.Absolute) };
            var expected = new { Property = new Uri("https://example.net/one", UriKind.Absolute) };
            var action = () => input.Must().BeEquivalentTo(expected);
            action.Must().NotThrow();
        }
    }

    public class NotBeEquivalentTo
    {
        [Fact]
        public void Should_Fail_WhenSubjectNull_AndObjectNull()
        {
            var action = () => (null as object).Must().NotBeEquivalentTo(null);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectNull_AndObjectExpected()
        {
            var action = () => (null as object).Must().NotBeEquivalentTo(new { Property = "value" });
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_NotFail_WhenSubjectObject_AndNullExpected()
        {
            var action = () => new { Property = "value" }.Must().NotBeEquivalentTo(null);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenObjectDepthExceedsMaximum()
        {
            // Whether testing positive or negative, this should fail if the objects are too deeply nested.
            var input = new[] { new { One = new { Two = new { Three = new { Four = new { Five = new { Six = new { Seven = new { Eight = new { Nine = new { Ten = new { Property = "value" } } } } } } } } } } } };
            var expected = new[] { new { One = new { Two = new { Three = new { Four = new { Five = new { Six = new { Seven = new { Eight = new { Nine = new { Ten = new { Property = "value" } } } } } } } } } } } }.ToList();

            var action = () => input.Must().NotBeEquivalentTo(expected);
            action.Must().Throw<XunitException>().Subject.Message.Must().StartWith("Exceeded the maximum depth 10");
        }

        [Fact]
        public void Should_NotFail_WhenPrimitiveValuesNotEqual()
        {
            var input = new { Property = "one" };
            var expected = new { Property = 1 };
            var action = () => input.Must().NotBeEquivalentTo(expected);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenPrimitiveValuesEqual()
        {
            var input = new { Property = "one" };
            var expected = new { Property = "one" };
            var action = () => input.Must().NotBeEquivalentTo(expected);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenPrimitiveValuesEquivalent()
        {
            var input = new { Property = "1" };
            var expected = new { Property = 1 };
            var action = () => input.Must().NotBeEquivalentTo(expected);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenDateTimeValuesNotEqual()
        {
            var input = new { Property = new DateTime(2025, 3, 11, 11, 58, 37, DateTimeKind.Utc) };
            var expected = new { Property = DateTime.UnixEpoch };
            var action = () => input.Must().NotBeEquivalentTo(expected);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenDateTimeValuesEqual()
        {
            var input = new { Property = new DateTime(2025, 3, 11, 11, 58, 37, DateTimeKind.Utc) };
            var expected = new { Property = new DateTime(2025, 3, 11, 11, 58, 37, DateTimeKind.Utc) };
            var action = () => input.Must().NotBeEquivalentTo(expected);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenUriValuesNotEqual()
        {
            var input = new { Property = new Uri("https://example.net/one", UriKind.Absolute) };
            var expected = new { Property = new Uri("/one", UriKind.Relative) };
            var action = () => input.Must().NotBeEquivalentTo(expected);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenUriValuesEqual()
        {
            var input = new { Property = new Uri("https://example.net/one", UriKind.Absolute) };
            var expected = new { Property = new Uri("https://example.net/one", UriKind.Absolute) };
            var action = () => input.Must().NotBeEquivalentTo(expected);
            action.Must().Throw<XunitException>();
        }
    }

    public class Match
    {
        [Fact]
        public void Should_Pass_WhenPredicateSatisfied()
        {
            var input = new { Property = "one" };
            var action = () => input.Must().Match(i => i.GetPropertyValue("Property") as string == "one");
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenPredicateNotSatisfied()
        {
            var input = new { Property = "one" };
            var action = () => input.Must().Match(i => i.GetPropertyValue("Property") as string == "two");
            action.Must().Throw<XunitException>();
        }
    }
}