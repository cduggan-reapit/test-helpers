using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Reapit.Platform.Testing.UnitTests.Fluent.Assertions.Collections;

public static class CollectionAssertionsTests
{
    public class BeEmpty
    {
        [Fact]
        public void Should_Fail_WhenCollectionNull()
        {
            var action = () => (null as int[]).Must().BeEmpty();
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenCollectionEmpty()
        {
            int[] collection = [];
            var action = () => collection.Must().BeEmpty();
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenCollectionPopulated()
        {
            int[] collection = [1, 2, 3];
            var action = () => collection.Must().BeEmpty();
            action.Must().Throw<XunitException>();
        }
    }

    public class NotBeEmpty
    {
        [Fact]
        public void Should_Fail_WhenCollectionNull()
        {
            var action = () => (null as int[]).Must().NotBeEmpty();
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenCollectionEmpty()
        {
            int[] collection = [];
            var action = () => collection.Must().NotBeEmpty();
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenCollectionPopulated()
        {
            int[] collection = [1, 2, 3];
            var action = () => collection.Must().NotBeEmpty();
            action.Must().NotThrow();
        }
    }

    public class Contain
    {
        [Fact]
        public void Should_Fail_WhenCollectionNull_AndNoFunctionProvided()
        {
            var action = () => (null as int[]).Must().Contain(2);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenCollectionNull_AndFunctionProvided()
        {
            var action = () => (null as int[]).Must().Contain(item => item % 2 == 0);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenCollectionEmpty_AndNoFunctionProvided()
        {
            int[] collection = [];
            var action = () => collection.Must().Contain(2);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenCollectionEmpty_AndFunctionProvided()
        {
            int[] collection = [];
            var action = () => collection.Must().Contain(item => item % 2 == 0);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenNotMatched_AndNoFunctionProvided()
        {
            int[] collection = [1, 3, 5];
            var action = () => collection.Must().Contain(2);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenNotMatched_AndFunctionProvided()
        {
            int[] collection = [1, 3, 5];
            var action = () => collection.Must().Contain(item => item % 2 == 0);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenMatched_AndNoFunctionProvided()
        {
            int[] collection = [1, 2, 3, 4, 5];
            var action = () => collection.Must().Contain(2);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_NotFail_WhenMatched_AndFunctionProvided()
        {
            int[] collection = [1, 2, 3, 4, 5];
            var action = () => collection.Must().Contain(item => item % 2 == 0);
            action.Must().NotThrow();
        }
    }

    public class NotContain
    {
        [Fact]
        public void Should_NotFail_WhenCollectionNull_AndNoFunctionProvided()
        {
            var action = () => (null as int[]).Must().NotContain(2);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_NotFail_WhenCollectionNull_AndFunctionProvided()
        {
            var action = () => (null as int[]).Must().NotContain(item => item % 2 == 0);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_NotFail_WhenCollectionEmpty_AndNoFunctionProvided()
        {
            int[] collection = [];
            var action = () => collection.Must().NotContain(2);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_NotFail_WhenCollectionEmpty_AndFunctionProvided()
        {
            int[] collection = [];
            var action = () => collection.Must().NotContain(item => item % 2 == 0);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_NotFail_WhenNotMatched_AndNoFunctionProvided()
        {
            int[] collection = [1, 3, 5];
            var action = () => collection.Must().NotContain(2);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_NotFail_WhenNotMatched_AndFunctionProvided()
        {
            int[] collection = [1, 3, 5];
            var action = () => collection.Must().NotContain(item => item % 2 == 0);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenMatched_AndNoFunctionProvided()
        {
            int[] collection = [1, 2, 3, 4, 5];
            var action = () => collection.Must().NotContain(2);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenMatched_AndFunctionProvided()
        {
            int[] collection = [1, 2, 3, 4, 5];
            var action = () => collection.Must().NotContain(item => item % 2 == 0);
            action.Must().Throw<XunitException>();
        }
    }

    public class AllSatisfy
    {
        [Fact]
        public void Should_Fail_WhenCollectionEmpty()
        {
            string[] collection = [];
            var action = () => collection.Must().AllSatisfy(item => item.Must().HaveLength(3));
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenNotAllElementsSatisfyPredicate()
        {
            var collection = new[] { "one", "two", "three" };
            var action = () => collection.Must().AllSatisfy(item => item.Must().HaveLength(3));
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenNotAllElementsSatisfyPredicate()
        {
            var collection = new[] { "one", "two", "thr" };
            var action = () => collection.Must().AllSatisfy(item => item.Must().HaveLength(3));
            action.Must().NotThrow();
        }
    }

    /*
     * Inherited methods
     */

    public class BeNull
    {
        [Fact]
        public void Should_NotFail_WhenCollectionNull()
        {
            var action = () => (null as IEnumerable<int>).Must().BeNull();
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_NotFail_WhenCollectionNotNull()
        {
            int[] collection = [1, 2, 3];
            var action = () => collection.Must().BeNull();
            action.Must().Throw<XunitException>();
        }
    }

    public class NotBeNull
    {
        [Fact]
        public void Should_Fail_WhenCollectionNull()
        {
            var action = () => (null as IEnumerable<int>).Must().NotBeNull();
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenCollectionNotNull()
        {
            int[] collection = [1, 2, 3];
            var action = () => collection.Must().NotBeNull();
            action.Must().NotThrow();
        }
    }

    public class BeOfType
    {
        [Fact]
        public void Should_Fail_WhenCollectionNull()
        {
            // the types would match were the subject not null
            var action = () => (null as IEnumerable<int>).Must().BeOfType<int[]>();
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenCollectionNotOfType()
        {
            // IEnumerable<int> != int[], so this should fail
            int[] collection = [1, 2, 3];
            var action = () => collection.Must().BeOfType<IEnumerable<int>>();
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenCollectionOfType()
        {
            int[] collection = [1, 2, 3];
            var action = () => collection.Must().BeOfType<int[]>();
            action.Must().NotThrow();
        }
    }

    public class NotBeOfType
    {
        [Fact]
        public void Should_NotFail_WhenCollectionNull()
        {
            // the types would match were the subject not null
            var action = () => (null as IEnumerable<int>).Must().NotBeOfType<int[]>();
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_NotFail_WhenCollectionNotOfType()
        {
            // IEnumerable<int> != int[], so this should fail
            int[] collection = [1, 2, 3];
            var action = () => collection.Must().NotBeOfType<IEnumerable<int>>();
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenCollectionOfType()
        {
            int[] collection = [1, 2, 3];
            var action = () => collection.Must().NotBeOfType<int[]>();
            action.Must().Throw<XunitException>();
        }
    }

    public class BeAssignableTo
    {
        [Fact]
        public void Should_Fail_WhenCollectionNull()
        {
            // the types would match were the subject not null
            var action = () => (null as IEnumerable<int>).Must().BeAssignableTo<int[]>();
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenCollectionNotOfDerivedType()
        {
            int[] collection = [1, 2, 3];
            var action = () => collection.Must().BeAssignableTo<IEnumerable<string>>();
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenCollectionOfDerivedType()
        {
            // int[] implements IEnumerable<int>
            int[] collection = [1, 2, 3];
            var action = () => collection.Must().BeAssignableTo<IEnumerable<int>>();
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_NotFail_WhenCollectionOfType()
        {
            int[] collection = [1, 2, 3];
            var action = () => collection.Must().BeAssignableTo<int[]>();
            action.Must().NotThrow();
        }
    }

    public class NotBeAssignableTo
    {
        [Fact]
        public void Should_NotFail_WhenCollectionNull()
        {
            var action = () => (null as IEnumerable<int>).Must().NotBeAssignableTo<int[]>();
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_NotFail_WhenCollectionNotOfDerivedType()
        {
            int[] collection = [1, 2, 3];
            var action = () => collection.Must().NotBeAssignableTo<IEnumerable<string>>();
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenCollectionOfDerivedType()
        {
            // int[] implements IEnumerable<int>
            int[] collection = [1, 2, 3];
            var action = () => collection.Must().NotBeAssignableTo<IEnumerable<int>>();
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenCollectionOfType()
        {
            int[] collection = [1, 2, 3];
            var action = () => collection.Must().NotBeAssignableTo<int[]>();
            action.Must().Throw<XunitException>();
        }
    }

    public class BeEquivalentTo
    {
        [Fact]
        public void Should_NotFail_WhenPrimitiveTypesEqual()
        {
            string[] input = ["1", "2", "3"];
            string[] expectation = ["2", "3", "1"];
            var action = () => input.Must().BeEquivalentTo(expectation);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenPrimitiveTypesNotEqual()
        {
            string[] input = ["1", "2", "3"];
            string[] expectation = ["2", "3", "4"];
            var action = () => input.Must().BeEquivalentTo(expectation);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenObjectsEquivalent()
        {
            var input = new[] { new DummyObject(1), new DummyObject(2), new DummyObject(3) };
            var expectation = new[] { new DummyObject(3), new DummyObject(2), new DummyObject(1) };
            var action = () => input.Must().BeEquivalentTo(expectation);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenObjectsNotEquivalent()
        {
            var input = new[] { new DummyObject(1), new DummyObject(2), new DummyObject(3) };
            var expectation = new[] { new DummyObject(4), new DummyObject(3), new DummyObject(2) };
            var action = () => input.Must().BeEquivalentTo(expectation);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_Fail_WhenActualContainsExtraElements_InStrictMode()
        {
            string[] input = ["1", "2", "3", "4"];
            string[] expectation = ["1", "2", "3"];
            var action = () => input.Must().BeEquivalentTo(expectation, true);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenActualContainsExtraElements_NotInStrictMode()
        {
            string[] input = ["1", "2", "3", "4"];
            string[] expectation = ["1", "2", "3"];
            var action = () => input.Must().BeEquivalentTo(expectation, false);
            action.Must().NotThrow();
        }
    }

    public class NotBeEquivalentTo
    {
        [Fact]
        public void Should_Fail_WhenPrimitiveTypesEqual()
        {
            string[] input = ["1", "2", "3"];
            string[] expectation = ["2", "3", "1"];
            var action = () => input.Must().NotBeEquivalentTo(expectation);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenPrimitiveTypesNotEqual()
        {
            string[] input = ["1", "2", "3"];
            string[] expectation = ["2", "3", "4"];
            var action = () => input.Must().NotBeEquivalentTo(expectation);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenObjectsEquivalent()
        {
            var input = new[] { new DummyObject(1), new DummyObject(2), new DummyObject(3) };
            var expectation = new[] { new DummyObject(3), new DummyObject(2), new DummyObject(1) };
            var action = () => input.Must().NotBeEquivalentTo(expectation);
            action.Must().Throw<XunitException>();
        }

        [Fact]
        public void Should_NotFail_WhenObjectsNotEquivalent()
        {
            var input = new[] { new DummyObject(1), new DummyObject(2), new DummyObject(3) };
            var expectation = new[] { new DummyObject(4), new DummyObject(3), new DummyObject(2) };
            var action = () => input.Must().NotBeEquivalentTo(expectation);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_NotFail_WhenActualContainsExtraElements_InStrictMode()
        {
            string[] input = ["1", "2", "3", "4"];
            string[] expectation = ["1", "2", "3"];
            var action = () => input.Must().NotBeEquivalentTo(expectation, true);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenActualContainsExtraElements_NotInStrictMode()
        {
            string[] input = ["1", "2", "3", "4"];
            string[] expectation = ["1", "2", "3"];
            var action = () => input.Must().NotBeEquivalentTo(expectation, false);
            action.Must().Throw<XunitException>();
        }
    }

    public class Match
    {
        [Fact]
        public void Should_Pass_WhenPredicateSatisfied()
        {
            var input = new[] { new DummyObject(1), new DummyObject(2), new DummyObject(3) };
            var action = () => input.Must().Match(e => e.Count() == 3);
            action.Must().NotThrow();
        }

        [Fact]
        public void Should_Fail_WhenPredicateNotSatisfied()
        {
            var input = new[] { new DummyObject(1), new DummyObject(2), new DummyObject(3) };
            var action = () => input.Must().Match(e => e.Count() == 4);
            action.Must().Throw<XunitException>();
        }
    }

    /// <summary>
    /// This object is used when comparing equivalence - it has a few properties populated from a single input value
    /// so checks that we're testing all the contents.
    /// </summary>
    /// <param name="seed">The seed value.</param>
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    private sealed class DummyObject(int seed)
    {
        public int Number { get; } = seed;

        public string String { get; } = seed.ToString("N0", CultureInfo.InvariantCulture);

        public object Content { get; } = new { Number = seed, String = seed.ToString("N0", CultureInfo.InvariantCulture) };
    }
}