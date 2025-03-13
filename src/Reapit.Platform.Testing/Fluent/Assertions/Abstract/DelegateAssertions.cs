using Reapit.Platform.Testing.Fluent.Core;
using Reapit.Platform.Testing.Fluent.Failures;

namespace Reapit.Platform.Testing.Fluent.Assertions.Abstract;

/// <summary>Contains assertions for delegate methods.</summary>
/// <param name="subject">The assertion subject.</param>
/// <typeparam name="TDelegate">The type of delegate (e.g. Action, Func{T}).</typeparam>
/// <typeparam name="TAssertions">The type of assertions to return in continuation objects.</typeparam>
[DebuggerNonUserCode]
public abstract class DelegateAssertions<TDelegate, TAssertions>(TDelegate subject)
    where TDelegate : Delegate
    where TAssertions : DelegateAssertions<TDelegate, TAssertions>
{
    /// <summary>The assertion context name.</summary>
    protected abstract string Context { get; }

    /// <summary>The subject of assertions.</summary>
    protected TDelegate Subject { get; } = subject;

    /// <summary>Method used to test delegate execution.</summary>
    protected abstract void InvokeDelegate();

    /// <summary>Asserts that execution of the subject delegate raises an exception.</summary>
    public ExceptionAssertions<Exception> Throw()
    {
        var exception = InvokeActualWithInterception();
        if (exception != null)
            return new ExceptionAssertions<Exception>(exception);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetMessageTemplate("Expected {context} to throw an exception.")
            .Build();
    }

    /// <summary>Asserts that execution of the subject delegate raises an exception of type <typeparamref name="TException"/>.</summary>
    /// <typeparam name="TException">The expected exception type.</typeparam>
    /// <remarks>
    /// This test will match exceptions of type <typeparamref name="TException"/> and all derived types. For type
    /// specificity, please use <see cref="ThrowExactly{TException}"/>.
    /// </remarks>
    public ExceptionAssertions<TException> Throw<TException>()
        where TException : Exception
    {
        var exception = InvokeActualWithInterception();

        if (exception is TException typedException)
            return new ExceptionAssertions<TException>(typedException);

        var failure = TestFailureBuilder.CreateForContext(Context)
            .SetMessageTemplate("Expected {context} to throw {expected},{reason}")
            .SetContextData("expected", typeof(TException).Name);

        if (exception is null)
            throw failure.SetContextData("reason", " but no exception was thrown.").Build();

        throw failure.SetContextData("reason", " but found {actual}.", false)
            .SetContextData("actual", exception.GetType().Name)
            .Build();
    }

    /// <summary>Asserts that execution of the subject delegate raises an exception of type <typeparamref name="TException"/>.</summary>
    /// <typeparam name="TException">The expected exception type.</typeparam>
    /// <remarks>
    /// This test will match exceptions of type <typeparamref name="TException"/>. To accept derived types, please use
    ///  <see cref="Throw{TException}"/>.
    /// </remarks>
    public ExceptionAssertions<TException> ThrowExactly<TException>()
        where TException : Exception
    {
        var exception = InvokeActualWithInterception();

        if (exception is TException typedException && exception.GetType() == typeof(TException))
            return new ExceptionAssertions<TException>(typedException);

        var failure = TestFailureBuilder.CreateForContext(Context)
            .SetMessageTemplate("Expected {context} to throw {expected},{reason}")
            .SetContextData("expected", typeof(TException).Name);

        if (exception is null)
            throw failure.SetContextData("reason", "but no exception was thrown.").Build();

        throw failure.SetContextData("reason", "but found {actual}.")
            .SetContextData("actual", exception.GetType().Name)
            .Build();
    }

    /// <summary>Asserts that execution of the subject delegate does not raise an exception.</summary>
    public AndOperator<TAssertions> NotThrow()
    {
        var exception = InvokeActualWithInterception();
        if (exception is null)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetMessageTemplate("Expected {context} to execute without exception, but {expected} was thrown.")
            .SetContextData("expected", exception.GetType().Name)
            .SetContextData("message", exception.Message)
            .Build();
    }

    /// <summary>Asserts that execution of the subject delegate does not raise an exception of type <typeparamref name="TException"/>.</summary>
    /// <typeparam name="TException">The exception type that must not be thrown.</typeparam>
    /// <remarks>
    /// This test will match exceptions of type <typeparamref name="TException"/> and all derived types. For type
    /// specificity, please use <see cref="NotThrowExactly{TException}"/>.
    /// </remarks>
    public AndOperator<TAssertions> NotThrow<TException>()
        where TException : Exception
    {
        var exception = InvokeActualWithInterception();

        if (exception is not TException)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetMessageTemplate("Expected {context} not to throw {expected}.")
            .SetContextData("expected", typeof(TException).Name)
            .SetContextData("actual", exception?.GetType().Name)
            .SetContextData("message", exception?.Message)
            .Build();
    }

    /// <summary>Asserts that execution of the subject delegate raises an exception of type <typeparamref name="TException"/>.</summary>
    /// <typeparam name="TException">The expected exception type.</typeparam>
    /// <remarks>
    /// This test will match exceptions of type <typeparamref name="TException"/>. To accept derived types, please use
    ///  <see cref="Throw{TException}"/>.
    /// </remarks>
    public AndOperator<TAssertions> NotThrowExactly<TException>()
        where TException : Exception
    {
        var exception = InvokeActualWithInterception();

        if (exception is null)
            return new AndOperator<TAssertions>((TAssertions)this);

        if (exception.GetType() != typeof(TException))
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetMessageTemplate("Expected {context} not to throw {expected}.")
            .SetContextData("expected", typeof(TException).Name)
            .SetContextData("actual", exception?.GetType().Name)
            .SetContextData("message", exception?.Message)
            .Build();
    }

    /// <summary>Invokes the subject action.</summary>
    private Exception? InvokeActualWithInterception()
    {
        try
        {
            InvokeDelegate();
            return null;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
}