namespace Reapit.Platform.Testing.UnitTests.Fluent.Assertions.Collections;

public static class DictionaryAssertionsTests
{
    private static Dictionary<int, string> TestDictionary => new()
    {
        { 1, "one" }, { 2, "two" }, { 3, "three" }
    };

    private static Dictionary<int, string>? NullDictionary => null;

    public class ContainKey
    {
        [Fact]
        public void Should_Fail_WhenDictionaryNull()
        {
            var action = () => NullDictionary.Must().ContainKey(1);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenKeyNotInDictionary()
        {
            var action = () => TestDictionary.Must().ContainKey(0);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenKeyInDictionary()
        {
            var action = () => TestDictionary.Must().ContainKey(2);
            action.Must().NotThrow();
        }
    }

    public class NotContainKey
    {
        [Fact]
        public void Should_NotFail_WhenDictionaryNull()
        {
            var action = () => NullDictionary.Must().NotContainKey(1);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_NotFail_WhenKeyNotInDictionary()
        {
            var action = () => TestDictionary.Must().NotContainKey(0);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenKeyInDictionary()
        {
            var action = () => TestDictionary.Must().NotContainKey(2);
            action.Must().Throw<XunitException>();
        }
    }

    public class ContainValue
    {
        [Fact]
        public void Should_Fail_WhenDictionaryNull()
        {
            var action = () => NullDictionary.Must().ContainValue("one");
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenKeyNotInDictionary()
        {
            var action = () => TestDictionary.Must().ContainValue("four");
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenKeyInDictionary()
        {
            var action = () => TestDictionary.Must().ContainValue("two");
            action.Must().NotThrow();
        }
    }

    public class NotContainValue
    {
        [Fact]
        public void Should_NotFail_WhenDictionaryNull()
        {
            var action = () => NullDictionary.Must().NotContainValue("one");
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_NotFail_WhenKeyNotInDictionary()
        {
            var action = () => TestDictionary.Must().NotContainValue("five");
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenKeyInDictionary()
        {
            var action = () => TestDictionary.Must().NotContainValue("two");
            action.Must().Throw<XunitException>();
        }
    }

    /*
     * Inherited methods
     */

    public class BeEmpty
    {
        [Fact]
        public void Should_Fail_WhenDictionaryNull()
        {
            var action = () => NullDictionary.Must().BeEmpty();
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenDictionaryEmpty()
        {
            var action = () => new Dictionary<int, string>().Must().BeEmpty();
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenKeyInDictionary()
        {
            var action = () => TestDictionary.Must().BeEmpty();
            action.Must().Throw<XunitException>();
        }
    }

    public class NotBeEmpty
    {
        [Fact]
        public void Should_Fail_WhenDictionaryNull()
        {
            var action = () => NullDictionary.Must().NotBeEmpty();
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenDictionaryEmpty()
        {
            var action = () => new Dictionary<int, string>().Must().NotBeEmpty();
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenKeyInDictionary()
        {
            var action = () => TestDictionary.Must().NotBeEmpty();
            action.Must().NotThrow();
        }
    }

    public class Contain
    {
        [Fact]
        public void Should_Fail_WhenDictionaryNull()
        {
            var action = () => NullDictionary.Must().Contain(1, "one");
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenKeyValuePairNotInDictionary()
        {
            var action = () => TestDictionary.Must().Contain(1, "two");
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenKeyValuePairInDictionary()
        {
            var action = () => TestDictionary.Must().Contain(1, "one");
            action.Must().NotThrow();
        }
    }

    public class NotContain
    {
        [Fact]
        public void Should_NotFail_WhenDictionaryNull()
        {
            var action = () => NullDictionary.Must().NotContain(1, "one");
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_NotFail_WhenKeyValuePairNotInDictionary()
        {
            var action = () => TestDictionary.Must().NotContain(1, "two");
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenKeyValuePairInDictionary()
        {
            var action = () => TestDictionary.Must().NotContain(1, "one");
            action.Must().Throw<XunitException>();
        }
    }

    public class AllSatisfy
    {
        [Fact]
        public void Should_Fail_WhenDictionaryNull()
        {
            var action = () => NullDictionary.Must().AllSatisfy(kvp => kvp.Key.Must().BePositive());
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenUnsatisfied()
        {
            var action = () => TestDictionary.Must().AllSatisfy(kvp => kvp.Key.Must().BeNegative());
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenSatisfied()
        {
            var action = () => TestDictionary.Must().AllSatisfy(kvp => kvp.Key.Must().BePositive());
            action.Must().NotThrow();
        }
    }
}