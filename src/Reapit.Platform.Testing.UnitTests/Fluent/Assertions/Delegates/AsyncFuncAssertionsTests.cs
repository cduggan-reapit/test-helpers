namespace Reapit.Platform.Testing.UnitTests.Fluent.Assertions.Delegates;

public static class AsyncFuncAssertionsTests
{
    /*
     * While we use .Throw() and .NotThrow() to test other assertions, we can't allow them to mark their own homework
     * here. We'll use local Passes and Fails methods to check whether the action throws XunitException 
     */
    public class Throw
    {
        [Fact]
        public async Task Should_Fail_WhenNoExceptionThrown()
        {
            var subject = () => TestFunctionAsync(null);
            var action = () => subject.Must().ThrowAsync();
            await action.FailsAsync();
        }

        [Fact]
        public async Task Should_NotFail_WhenExceptionThrown()
        {
            var subject = () => TestFunctionAsync(new ArgumentException());
            var action = () => subject.Must().ThrowAsync();
            await action.PassesAsync();
        }
    }

    public class ThrowTException
    {
        [Fact]
        public async Task Should_Fail_WhenNoExceptionThrown()
        {
            var subject = () => TestFunctionAsync(null);
            var action = () => subject.Must().ThrowAsync<ArgumentException>();
            await action.FailsAsync();
        }

        [Fact]
        public async Task Should_Fail_WhenDifferentExceptionThrown()
        {
            var subject = () => TestFunctionAsync(new InvalidOperationException());
            var action = () => subject.Must().ThrowAsync<ArgumentException>();
            await action.FailsAsync();
        }

        [Fact]
        public async Task Should_NotFail_WhenExactExceptionThrown()
        {
            var subject = () => TestFunctionAsync(new ArgumentNullException());
            var action = () => subject.Must().ThrowAsync<ArgumentNullException>();
            await action.PassesAsync();
        }

        [Fact]
        public async Task Should_NotFail_WhenDerivedExceptionThrown()
        {
            var subject = () => TestFunctionAsync(new ArgumentNullException());
            var action = () => subject.Must().ThrowAsync<ArgumentException>();
            await action.PassesAsync();
        }
    }

    public class ThrowExactlyTException
    {
        [Fact]
        public async Task Should_Fail_WhenNoExceptionThrown()
        {
            var subject = () => TestFunctionAsync(null);
            var action = () => subject.Must().ThrowExactlyAsync<ArgumentException>();
            await action.FailsAsync();
        }

        [Fact]
        public async Task Should_NotFail_WhenExactExceptionThrown()
        {
            var subject = () => TestFunctionAsync(new ArgumentNullException());
            var action = () => subject.Must().ThrowExactlyAsync<ArgumentNullException>();
            await action.PassesAsync();
        }

        [Fact]
        public async Task Should_Fail_WhenDerivedExceptionThrown()
        {
            var subject = () => TestFunctionAsync(new ArgumentNullException());
            var action = () => subject.Must().ThrowExactlyAsync<ArgumentException>();
            await action.FailsAsync();
        }
    }

    public class NotThrow
    {
        [Fact]
        public async Task Should_NotFail_WhenNoExceptionThrown()
        {
            var subject = () => TestFunctionAsync(null);
            var action = () => subject.Must().NotThrowAsync();
            await action.PassesAsync();
        }

        [Fact]
        public async Task Should_Fail_WhenExceptionThrown()
        {
            var subject = () => TestFunctionAsync(new ArgumentException());
            var action = () => subject.Must().NotThrowAsync();
            await action.FailsAsync();
        }
    }

    public class NotThrowTException
    {
        [Fact]
        public async Task Should_NotFail_WhenNoExceptionThrown()
        {
            var subject = () => TestFunctionAsync(null);
            var action = () => subject.Must().NotThrowAsync<ArgumentException>();
            await action.PassesAsync();
        }

        [Fact]
        public async Task Should_Fail_WhenExactExceptionThrown()
        {
            var subject = () => TestFunctionAsync(new ArgumentNullException());
            var action = () => subject.Must().NotThrowAsync<ArgumentNullException>();
            await action.FailsAsync();
        }

        [Fact]
        public async Task Should_Fail_WhenDerivedExceptionThrown()
        {
            var subject = () => TestFunctionAsync(new ArgumentNullException());
            var action = () => subject.Must().NotThrowAsync<ArgumentException>();
            await action.FailsAsync();
        }
    }

    public class NotThrowExactlyTException
    {
        [Fact]
        public async Task Should_NotFail_WhenNoExceptionThrown()
        {
            var subject = () => TestFunctionAsync(null);
            var action = () => subject.Must().NotThrowExactlyAsync<ArgumentException>();
            await action.PassesAsync();
        }

        [Fact]
        public async Task Should_Fail_WhenExactExceptionThrown()
        {
            var subject = () => TestFunctionAsync(new ArgumentNullException());
            var action = () => subject.Must().NotThrowExactlyAsync<ArgumentNullException>();
            await action.FailsAsync();
        }

        [Fact]
        public async Task Should_NotFail_WhenDerivedExceptionThrown()
        {
            var subject = () => TestFunctionAsync(new ArgumentNullException());
            var action = () => subject.Must().NotThrowExactlyAsync<ArgumentException>();
            await action.PassesAsync();
        }
    }

    /*
     * Private Methods
     */

    private static Task<int> TestFunctionAsync(Exception? exception)
    {
        if (exception is null)
            return Task.FromResult(1);

        throw exception;
    }

    private static Task PassesAsync<TResult>(this Func<Task<TResult>>? action)
    {
        Assert.NotNull(action);
        return action();
    }

    private static Task FailsAsync<TResult>(this Func<Task<TResult>>? action)
        => FailsAsync<TResult, XunitException>(action);

    private static Task FailsAsync<TResult, TException>(this Func<Task<TResult>>? action)
        where TException : Exception
    {
        Assert.NotNull(action);
        return Assert.ThrowsAsync<TException>(action);
    }
}