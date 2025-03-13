using Reapit.Platform.Testing.Fluent.Core;
using Reapit.Platform.Testing.Fluent.Failures;
using System.Linq.Expressions;

namespace Reapit.Platform.Testing.Fluent.Assertions.Abstract;

/// <summary>Contains assertions for reference types.</summary>
/// <param name="subject">The assertion subject.</param>
/// <typeparam name="TSubject">The reference type.</typeparam>
/// <typeparam name="TAssertions">The type of assertions to return in continuation objects.</typeparam>
[DebuggerNonUserCode]
public abstract class NullableReferenceTypeAssertions<TSubject, TAssertions>(TSubject? subject)
    where TAssertions : NullableReferenceTypeAssertions<TSubject, TAssertions>
{
    /// <summary>The assertion context name.</summary>
    protected abstract string Context { get; }

    /// <summary>The subject of assertions.</summary>
    protected TSubject? Subject { get; } = subject;

    /// <summary>Asserts that the subject is <see langword="null"/>.</summary>
    public AndOperator<TAssertions> BeNull()
    {
        if (Subject is null)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetMessageTemplate("Expected {context} to be null, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject is not <see langword="null"/>.</summary>
    public AndOperator<TAssertions> NotBeNull()
    {
        if (Subject is not null)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetMessageTemplate("Expected {context} not to be null.")
            .Build();
    }

    /// <summary>Asserts that the subject is an instance of <typeparamref name="T"/>.</summary>
    /// <typeparam name="T">The expected type.</typeparam>
    public AndOperator<TAssertions> BeOfType<T>()
    {
        if (Subject is not null && Subject.GetType() == typeof(T))
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("expected", typeof(T).Name)
            .SetContextData("actual", Subject?.GetType().Name)
            .SetMessageTemplate("Expected {context} to be {expected}, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject is not an instance of <typeparamref name="T"/>.</summary>
    /// <typeparam name="T">The type against which the subject is compared.</typeparam>
    /// <remarks>Will not cause tests to fail if subject is null.</remarks>
    public AndOperator<TAssertions> NotBeOfType<T>()
    {
        if (Subject is null || Subject.GetType() != typeof(T))
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("expected", typeof(T).Name)
            .SetMessageTemplate("Expected {context} not to be {expected}.")
            .Build();
    }

    /// <summary>Asserts that the subject is an instance of <typeparamref name="T"/> or a derived type.</summary>
    /// <typeparam name="T">The expected type.</typeparam>
    public AndOperator<TAssertions> BeAssignableTo<T>()
    {
        if (Subject is T)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("expected", typeof(T).Name)
            .SetContextData("actual", Subject?.GetType().Name)
            .SetMessageTemplate("Expected {context} to be assignable to {expected}, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject is not an instance of <typeparamref name="T"/> or a derived type.</summary>
    /// <typeparam name="T">The type against which the subject is compared.</typeparam>
    /// <remarks>Will not cause tests to fail if subject is null.</remarks>
    public AndOperator<TAssertions> NotBeAssignableTo<T>()
    {
        if (Subject is not T)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("expected", typeof(T).Name)
            .SetContextData("actual", Subject?.GetType().Name)
            .SetMessageTemplate("Expected {context} not to be assignable to {expected}, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject is equivalent to <paramref name="expected"/>.</summary>
    /// <param name="expected">The object against which the subject is compared.</param>
    public AndOperator<TAssertions> BeEquivalentTo(object? expected) => BeEquivalentTo(expected, false);

    /// <summary>Asserts that the subject is equivalent to <paramref name="expected"/>.</summary>
    /// <param name="expected">The object against which the subject is compared.</param>
    /// <param name="strict">Flag indicating whether strict comparison should be applied.</param>
    public AndOperator<TAssertions> BeEquivalentTo(object? expected, bool strict)
    {
        var result = EquivalenceAnalyzer.VerifyEquivalence(expected, Subject, strict);
        if (result == null)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("expected", expected)
            .SetContextData("actual", Subject)
            .SetContextData("strict", strict)
            .SetContextData("details", result.Message)
            .SetInnerException(result)
            .SetMessageTemplate("Expected {context} to be equivalent to {expected}, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject is not equivalent to <paramref name="expected"/>.</summary>
    /// <param name="expected">The object against which the subject is compared.</param>
    public AndOperator<TAssertions> NotBeEquivalentTo(object? expected)
        => NotBeEquivalentTo(expected, false);

    /// <summary>Asserts that the subject is not equivalent to <paramref name="expected"/>.</summary>
    /// <param name="expected">The object against which the subject is compared.</param>
    /// <param name="strict">Flag indicating whether strict comparison should be applied.</param>
    public AndOperator<TAssertions> NotBeEquivalentTo(object? expected, bool strict)
    {
        var result = EquivalenceAnalyzer.VerifyEquivalence(expected, Subject, strict);
        if (result is not null)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("expected", expected)
            .SetContextData("actual", Subject)
            .SetContextData("strict", strict)
            .SetMessageTemplate("Expected {context} not to be equivalent to {expected}.")
            .Build();
    }

    /// <summary>Asserts that the subject matches the given predicate.</summary>
    /// <param name="predicate">The predicate which must be satisfied.</param>
    public AndOperator<TAssertions> Match(Expression<Func<TSubject, bool>> predicate)
        => Match<TSubject>(predicate);

    /// <summary>Asserts that the subject matches the given predicate.</summary>
    /// <param name="predicate">The predicate which must be satisfied.</param>
    public AndOperator<TAssertions> Match<T>(Expression<Func<T, bool>> predicate)
        where T : TSubject
    {
        if (Subject is not null && predicate.Compile()((T)Subject))
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetMessageTemplate("Expected {context} to satisfy the predicate, but found {actual}.")
            .Build();
    }
}