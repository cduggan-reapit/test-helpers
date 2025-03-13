using Reapit.Platform.Testing.Fluent.Failures;

namespace Reapit.Platform.Testing.Fluent.Assertions;

/// <summary>Contains assertions for exception types.</summary>
/// <typeparam name="T">The type against which to assert.</typeparam>
/// <param name="subject">The assertion subject.</param>
[DebuggerNonUserCode]
public class ExceptionAssertions<T>(T subject) : ExceptionAssertions<T, ExceptionAssertions<T>>(subject)
    where T : Exception;

/// <summary>Contains assertions for exception types.</summary>
/// <typeparam name="T">The type against which to assert.</typeparam>
/// <typeparam name="TAssertions">The type of assertions to return in continuation objects.</typeparam>
/// <remarks>
/// Exception assertions will look a little different.  They're typically used with a delegate assertions:
/// <code>
/// action.Must().Throw&lt;TException&gt;()
///     .WithMessage("expected")
///     .WithInnerException&lt;TInner&gt;()
///     .WithMessage("expected inner message"):
/// </code>
/// </remarks>
[DebuggerNonUserCode]
public class ExceptionAssertions<T, TAssertions>
    where T : Exception
    where TAssertions : ExceptionAssertions<T, TAssertions>
{
    /// <summary>The assertion context name.</summary>
    private const string Context = "exception";

    /// <summary>The subject of assertions.</summary>
    protected internal T Subject { get; }

    /// <summary>Initializes a new instance of the <see cref="ExceptionAssertions{T}"/> class.</summary>
    /// <param name="subject">The nullable assertion subject.</param>
    protected ExceptionAssertions(T subject)
        => Subject = subject;

    /// <summary>Asserts that the exception message matches the expected value.</summary>
    /// <param name="expected">The expected message.</param>
    public ExceptionAssertions<T, TAssertions> WithMessage(string expected)
        => WithMessage(expected, StringComparison.OrdinalIgnoreCase);

    /// <summary>Asserts that the exception message matches the expected value.</summary>
    /// <param name="expected">The expected message.</param>
    /// <param name="comparison">The type of string comparison to apply when evaluating message equality.</param>
    public ExceptionAssertions<T, TAssertions> WithMessage(string expected, StringComparison comparison)
    {
        if (Subject is not null && Subject.Message.Equals(expected, comparison))
            return this;

        throw TestFailureBuilder.CreateForContext(Context)
            .SetMessageTemplate("Expected {context} to have message: {expected}'.")
            .SetContextData("expected", expected)
            .SetContextData("actual", Subject?.Message)
            .Build();
    }

    /// <summary>Asserts that the exception message matches the expected value.</summary>
    /// <typeparam name="TInnerException">The expected inner exception type.</typeparam>
    public ExceptionAssertions<TInnerException> WithInnerException<TInnerException>()
        where TInnerException : Exception
    {
        if (Subject?.InnerException is TInnerException inner)
            return new ExceptionAssertions<TInnerException>(inner);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetMessageTemplate("Expected {context} to have inner exception of type {expected}, but found {actual}.")
            .SetContextData("expected", typeof(TInnerException).Name)
            .SetContextData("actual", Subject?.InnerException?.GetType().Name)
            .Build();
    }
}