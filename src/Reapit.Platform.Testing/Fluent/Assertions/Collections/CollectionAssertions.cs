using Reapit.Platform.Testing.Fluent.Assertions.Abstract;
using Reapit.Platform.Testing.Fluent.Core;
using Reapit.Platform.Testing.Fluent.Failures;
using System.Linq.Expressions;

namespace Reapit.Platform.Testing.Fluent.Assertions.Collections;

/// <summary>Contains assertions for a collection of <typeparamref name="T"/> values.</summary>
/// <param name="subject">The assertion subject.</param>
/// <typeparam name="T">The type of object in the collection.</typeparam>
public class CollectionAssertions<T>(IEnumerable<T>? subject)
    : CollectionAssertions<IEnumerable<T>, T>(subject);

/// <summary>Contains assertions for a collection of <typeparamref name="T"/> values.</summary>
/// <param name="subject">The assertion subject.</param>
/// <typeparam name="TCollection">The type of the collection.</typeparam>
/// <typeparam name="T">The type of object in the collection.</typeparam>
[DebuggerNonUserCode]
public class CollectionAssertions<TCollection, T>(TCollection? subject)
    : CollectionAssertions<TCollection, T, CollectionAssertions<TCollection, T>>(subject)
    where TCollection : IEnumerable<T>;

/// <summary>Contains assertions for a collection of <typeparamref name="TElement"/> values.</summary>
/// <param name="subject">The assertion subject.</param>
/// <typeparam name="TCollection">The type of the collection.</typeparam>
/// <typeparam name="TElement">The type of object in the collection.</typeparam>
/// <typeparam name="TAssertions">The type of assertions to return in continuation objects.</typeparam>
[DebuggerNonUserCode]
public class CollectionAssertions<TCollection, TElement, TAssertions>(TCollection? subject)
    : NullableReferenceTypeAssertions<IEnumerable<TElement>, TAssertions>(subject)
    where TCollection : IEnumerable<TElement>
    where TAssertions : CollectionAssertions<TCollection, TElement, TAssertions>
{
    /// <inheritdoc />
    protected override string Context => "collection";

    /// <summary>Asserts that the subject is not null and does not contain any items.</summary>
    public AndOperator<TAssertions> BeEmpty()
    {
        if (Subject != null && !Subject.Any())
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetMessageTemplate("Expected {context} to be empty, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject contains at least one item.</summary>
    public AndOperator<TAssertions> NotBeEmpty()
    {
        if (Subject != null && Subject.Any())
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetMessageTemplate("Expected {context} not to be empty, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject contains the expected value.</summary>
    /// <param name="expected">The expected value.</param>
    public virtual AndOperator<TAssertions> Contain(TElement expected)
    {
        if (Subject is not null && Subject.Contains(expected))
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetContextData("expected", expected)
            .SetMessageTemplate("Expected {context} to contain {expected}.")
            .Build();
    }

    /// <summary>Asserts that the subject does not contain the <paramref name="unexpected"/> value.</summary>
    /// <param name="unexpected">The unexpected value.</param>
    public virtual AndOperator<TAssertions> NotContain(TElement unexpected)
    {
        if (Subject is null || !Subject.Contains(unexpected))
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetContextData("expected", unexpected)
            .SetMessageTemplate("Expected {context} not to contain {expected}.")
            .Build();
    }

    /// <summary>Asserts that the subject contains one or more elements matching the expression.</summary>
    /// <param name="expression">An equality comparer to compare values.</param>
    public AndOperator<TAssertions> Contain(Expression<Func<TElement, bool>> expression)
    {
        var predicate = expression.Compile();
        if (Subject is not null && Subject.Any(predicate))
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetMessageTemplate("Expected {context} to contain {expected}.")
            .Build();
    }

    /// <summary>Asserts that the subject does not contain any elements matching the expression.</summary>
    /// <param name="expression">The predicate which all elements must satisfy.</param>
    public AndOperator<TAssertions> NotContain(Expression<Func<TElement, bool>> expression)
    {
        var predicate = expression.Compile();
        if (Subject is null || !Subject.Any(predicate))
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("actual", Subject)
            .SetMessageTemplate("Expected {context} not to contain any elements matching the given expression.")
            .Build();
    }

    /// <summary>Asserts that all elements in the subject collection satisfy the given predicate.</summary>
    /// <param name="predicate">The predicate method.</param>
    /// <remarks>
    /// The predicate should contain an assertion which is applied to the <typeparamref name="TElement"/> elements in the collection, for example
    /// <code>
    /// var collection = new[] { "one", "two", "three" };
    /// collection.Must().AllSatisfy(item => item.Must().HaveLength(3));
    /// </code>
    /// </remarks>
    public AndOperator<TAssertions> AllSatisfy(Action<TElement> predicate)
    {
        if (Subject is null || !Subject.Any())
        {
            throw TestFailureBuilder.CreateForContext(Context)
                .SetContextData("actual", Subject)
                .SetMessageTemplate("Expected all elements to satisfy the predicate, but no items found.")
                .Build();
        }

        // The intention is that this is used as collection.Must(item => item.Must().Be(""));
        // we don't add any special handling here, we'll just let errors bubble up if an exception's thrown.
        foreach (var item in Subject)
        {
            try
            {
                predicate(item);
            }
            catch (XunitException failure)
            {
                throw TestFailureBuilder.CreateForContext(Context)
                    .SetContextData("actual", Subject)
                    .SetContextData("item", item)
                    .SetMessageTemplate("Expected all elements to satisfy the predicate, but found {item}.")
                    .SetInnerException(failure)
                    .Build();
            }
        }

        return new AndOperator<TAssertions>((TAssertions)this);
    }
}