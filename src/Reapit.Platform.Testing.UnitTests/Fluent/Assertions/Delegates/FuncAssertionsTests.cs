using Reapit.Platform.Testing.Fluent.Assertions;
using Reapit.Platform.Testing.Fluent.Assertions.Delegates;
using Reapit.Platform.Testing.Fluent.Core;

namespace Reapit.Platform.Testing.UnitTests.Fluent.Assertions.Delegates;

public static class FuncAssertionsTests
{
    /*
     * While we use .Throw() and .NotThrow() to test other assertions, we can't allow them to mark their own homework
     * here. We'll use local Passes and Fails methods to check whether the action throws XunitException 
     */
    public class Throw
    {
        [Fact]
        public void Should_Fail_WhenNoExceptionThrown()
        {
            var subject = () => TestFunction(null);
            var action = () => subject.Must().Throw();
            action.Fails();
        }

        [Fact]
        public void Should_NotFail_WhenExceptionThrown()
        {
            var subject = () => TestFunction(new ArgumentException());
            var action = () => subject.Must().Throw();
            action.Passes();
        }
    }

    public class ThrowTException
    {
        [Fact]
        public void Should_Fail_WhenNoExceptionThrown()
        {
            var subject = () => TestFunction(null);
            var action = () => subject.Must().Throw<ArgumentException>();
            action.Fails();
        }

        [Fact]
        public void Should_Fail_WhenDifferentExceptionThrown()
        {
            var subject = () => TestFunction(new InvalidOperationException());
            var action = () => subject.Must().Throw<ArgumentException>();
            action.Fails();
        }

        [Fact]
        public void Should_NotFail_WhenExactExceptionThrown()
        {
            var subject = () => TestFunction(new ArgumentNullException());
            var action = () => subject.Must().Throw<ArgumentNullException>();
            action.Passes();
        }

        [Fact]
        public void Should_NotFail_WhenDerivedExceptionThrown()
        {
            var subject = () => TestFunction(new ArgumentNullException());
            var action = () => subject.Must().Throw<ArgumentException>();
            action.Passes();
        }
    }

    public class ThrowExactlyTException
    {
        [Fact]
        public void Should_Fail_WhenNoExceptionThrown()
        {
            var subject = () => TestFunction(null);
            var action = () => subject.Must().ThrowExactly<ArgumentException>();
            action.Fails();
        }

        [Fact]
        public void Should_NotFail_WhenExactExceptionThrown()
        {
            var subject = () => TestFunction(new ArgumentNullException());
            var action = () => subject.Must().ThrowExactly<ArgumentNullException>();
            action.Passes();
        }

        [Fact]
        public void Should_Fail_WhenDerivedExceptionThrown()
        {
            var subject = () => TestFunction(new ArgumentNullException());
            var action = () => subject.Must().ThrowExactly<ArgumentException>();
            action.Fails();
        }
    }

    public class NotThrow
    {
        [Fact]
        public void Should_NotFail_WhenNoExceptionThrown()
        {
            var subject = () => TestFunction(null);
            var action = () => subject.Must().NotThrow();
            action.Passes();
        }

        [Fact]
        public void Should_Fail_WhenExceptionThrown()
        {
            var subject = () => TestFunction(new ArgumentException());
            var action = () => subject.Must().NotThrow();
            action.Fails();
        }
    }

    public class NotThrowTException
    {
        [Fact]
        public void Should_NotFail_WhenNoExceptionThrown()
        {
            var subject = () => TestFunction(null);
            var action = () => subject.Must().NotThrow<ArgumentException>();
            action.Passes();
        }

        [Fact]
        public void Should_Fail_WhenExactExceptionThrown()
        {
            var subject = () => TestFunction(new ArgumentNullException());
            var action = () => subject.Must().NotThrow<ArgumentNullException>();
            action.Fails();
        }

        [Fact]
        public void Should_Fail_WhenDerivedExceptionThrown()
        {
            var subject = () => TestFunction(new ArgumentNullException());
            var action = () => subject.Must().NotThrow<ArgumentException>();
            action.Fails();
        }
    }

    public class NotThrowExactlyTException
    {
        [Fact]
        public void Should_NotFail_WhenNoExceptionThrown()
        {
            var subject = () => TestFunction(null);
            var action = () => subject.Must().NotThrowExactly<ArgumentException>();
            action.Passes();
        }

        [Fact]
        public void Should_Fail_WhenExactExceptionThrown()
        {
            var subject = () => TestFunction(new ArgumentNullException());
            var action = () => subject.Must().NotThrowExactly<ArgumentNullException>();
            action.Fails();
        }

        [Fact]
        public void Should_NotFail_WhenDerivedExceptionThrown()
        {
            var subject = () => TestFunction(new ArgumentNullException());
            var action = () => subject.Must().NotThrowExactly<ArgumentException>();
            action.Passes();
        }
    }

    /*
     * Private Methods
     */

    private static int TestFunction(Exception? exception)
    {
        if (exception is null)
            return 1;

        throw exception;
    }

    private static void Passes<TException>(this Func<ExceptionAssertions<TException>> action)
        where TException : Exception
        => action();

    private static void Passes<T>(this Func<AndOperator<FuncAssertions<T>>> action)
        => action();

    private static void Fails<TException>(this Func<ExceptionAssertions<TException>> action)
        where TException : Exception
        => Assert.Throws<XunitException>(action);

    private static void Fails<T>(this Func<AndOperator<FuncAssertions<T>>> action)
        => Assert.Throws<XunitException>(action);
}