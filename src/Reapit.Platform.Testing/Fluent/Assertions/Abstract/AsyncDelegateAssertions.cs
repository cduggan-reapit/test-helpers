using Reapit.Platform.Testing.Fluent.Core;
using Reapit.Platform.Testing.Fluent.Failures;

namespace Reapit.Platform.Testing.Fluent.Assertions.Abstract;

/// <summary>Contains assertions for async delegate methods.</summary>
/// <param name="subject">The assertion subject.</param>
/// <typeparam name="TTask">The type of task returned by the delegate.</typeparam>
/// <typeparam name="TAssertions">The type of assertions to return in continuation objects.</typeparam>
[DebuggerNonUserCode]
public abstract class AsyncDelegateAssertions<TTask, TAssertions>(Func<TTask> subject)
    where TTask : Task
    where TAssertions : AsyncDelegateAssertions<TTask, TAssertions>
{
    /// <summary>The assertion context name.</summary>
    private const string Context = "async method";

    /// <summary>The subject of assertions.</summary>
    private Func<TTask> Subject { get; } = subject;

    /// <summary>Asserts that the async function throws an exception.</summary>
    public async Task<AndOperator<TAssertions>> ThrowAsync()
    {
        var ex = await InvokeSubjectAsync();
        if (ex is not null)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetMessageTemplate("Expected {context} to throw an exception, but no exception occurred.")
            .Build();
    }

    /// <summary>Asserts that the async function throws <typeparamref name="TException"/> or a derived type.</summary>
    /// <typeparam name="TException">The type of exception.</typeparam>
    public async Task<AndOperator<TAssertions>> ThrowAsync<TException>()
        where TException : Exception
    {
        var ex = await InvokeSubjectAsync();
        if (ex is TException)
            return new AndOperator<TAssertions>((TAssertions)this);

        var builder = TestFailureBuilder.CreateForContext(Context).SetContextData("expected", typeof(TException).Name);
        if (ex is null)
            throw builder.SetMessageTemplate("Expected {context} to throw an exception derived from {expected}, but no exception occurred.").Build();

        throw builder
            .SetContextData("actual", ex.GetType().Name)
            .SetMessageTemplate("Expected {context} to throw an exception derived from {expected}, but {actual} was thrown.")
            .Build();
    }

    /// <summary>Asserts that the async function throws <typeparamref name="TException"/>.</summary>
    /// <typeparam name="TException">The type of exception.</typeparam>
    public async Task<AndOperator<TAssertions>> ThrowExactlyAsync<TException>()
        where TException : Exception
    {
        var ex = await InvokeSubjectAsync();
        if (ex?.GetType() == typeof(TException))
            return new AndOperator<TAssertions>((TAssertions)this);

        var builder = TestFailureBuilder.CreateForContext(Context).SetContextData("expected", typeof(TException).Name);
        if (ex is null)
            throw builder.SetMessageTemplate("Expected {context} to throw an exception derived from {expected}, but no exception occurred.").Build();

        throw builder
            .SetContextData("actual", ex.GetType().Name)
            .SetMessageTemplate("Expected {context} to throw {expected}, but {actual} was thrown.")
            .Build();
    }

    /// <summary>Asserts that the async function does not throw an exception.</summary>
    public async Task<AndOperator<TAssertions>> NotThrowAsync()
    {
        var ex = await InvokeSubjectAsync();
        if (ex is null)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("exception", ex.GetType().Name)
            .SetMessageTemplate("Expected {context} to not throw an exception, but {exception} was thrown.")
            .Build();
    }

    /// <summary>Asserts that the async function throws <typeparamref name="TException"/> or a derived type.</summary>
    /// <typeparam name="TException">The type of exception.</typeparam>
    public async Task<AndOperator<TAssertions>> NotThrowAsync<TException>()
        where TException : Exception
    {
        var ex = await InvokeSubjectAsync();
        if (ex is not TException)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("expected", typeof(TException).Name)
            .SetContextData("actual", ex.GetType().Name)
            .SetMessageTemplate("Expected {context} to not throw an exception derived from {expected}, but {actual} was thrown.")
            .Build();
    }

    /// <summary>Asserts that the async function throws <typeparamref name="TException"/>.</summary>
    /// <typeparam name="TException">The type of exception.</typeparam>
    public async Task<AndOperator<TAssertions>> NotThrowExactlyAsync<TException>()
        where TException : Exception
    {
        var ex = await InvokeSubjectAsync();
        if (ex?.GetType() != typeof(TException))
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("expected", typeof(TException).Name)
            .SetContextData("actual", ex.GetType().Name)
            .SetMessageTemplate("Expected {context} to not throw {expected}, but {actual} was thrown.")
            .Build();
    }

    /*
     * Private methods
     */

    /// <summary>Invoke the Subject method with exception interception.</summary>
    private async Task<Exception?> InvokeSubjectAsync()
    {
        try
        {
            await Subject();
            return null;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
}