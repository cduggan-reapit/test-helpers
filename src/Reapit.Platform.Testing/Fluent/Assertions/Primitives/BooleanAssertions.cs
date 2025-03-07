using Reapit.Platform.Testing.Fluent.Core;
using Reapit.Platform.Testing.Fluent.Failures;

namespace Reapit.Platform.Testing.Fluent.Assertions.Primitives;

/// <summary>Contains assertions for boolean values.</summary>
/// <param name="subject">The assertion subject.</param>
[DebuggerNonUserCode]
public class BooleanAssertions(bool subject) : BooleanAssertions<BooleanAssertions>(subject);

/// <summary>Contains assertions for boolean values.</summary>
/// <typeparam name="TAssertions">The type of assertions to return in continuation objects.</typeparam>
[DebuggerNonUserCode]
public class BooleanAssertions<TAssertions>
    where TAssertions : BooleanAssertions<TAssertions>
{
    /// <summary>The assertion context name.</summary>
    protected string Context { get; } = "boolean";

    /// <summary>The subject of assertions.</summary>
    protected bool? Subject { get; }
    
    /// <summary>Initializes a new instance of the <see cref="BooleanAssertions"/> class.</summary>
    /// <param name="subject">The nullable assertion subject.</param>
    protected BooleanAssertions(bool? subject) => Subject = subject;

    /// <param name="subject">The assertion subject.</param>
    protected BooleanAssertions(bool subject) : this((bool?)subject)
    {
    }

    /// <summary>Asserts that the subject has the same value as <paramref name="compareTo"/>.</summary>
    /// <param name="compareTo">The value against which the subject is compared.</param>
    public AndOperator<TAssertions> Be(bool compareTo)
    {
        if (Subject == compareTo)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("expected", compareTo)
            .SetContextData("actual", Subject)
            .SetMessageTemplate("Expected {context} to be {expected}, but found {actual}.")
            .Build();
    }
    
    /// <summary>Asserts that the subject has the same value as <paramref name="compareTo"/>.</summary>
    /// <param name="compareTo">The value against which the subject is compared.</param>
    public AndOperator<TAssertions> Be(bool? compareTo)
    {
        if(Subject is null && compareTo is null)
            return new AndOperator<TAssertions>((TAssertions)this);
        
        if (Subject == compareTo)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("expected", compareTo)
            .SetContextData("actual", Subject)
            .SetMessageTemplate("Expected {context} to be {expected}, but found {actual}.")
            .Build();
    }
    
    /// <summary>Asserts that the subject has the same value as <paramref name="compareTo"/>.</summary>
    /// <param name="compareTo">The value against which the subject is compared.</param>
    public AndOperator<TAssertions> NotBe(bool compareTo)
    {
        if (Subject != compareTo)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("expected", compareTo)
            .SetContextData("actual", Subject)
            .SetMessageTemplate("Expected {context} not to be {expected}, but found {actual}.")
            .Build();
    }
    
    /// <summary>Asserts that the subject has the same value as <paramref name="compareTo"/>.</summary>
    /// <param name="compareTo">The value against which the subject is compared.</param>
    public AndOperator<TAssertions> NotBe(bool? compareTo)
    {
        if (compareTo is { } value ? Subject?.CompareTo(value) != 0 : Subject is not null)
            return new AndOperator<TAssertions>((TAssertions)this);

        throw TestFailureBuilder.CreateForContext(Context)
            .SetContextData("expected", compareTo)
            .SetContextData("actual", Subject)
            .SetMessageTemplate("Expected {context} not to be {expected}, but found {actual}.")
            .Build();
    }

    /// <summary>Asserts that the subject is true.</summary>
    public AndOperator<TAssertions> BeTrue()
        => Be(true);

    /// <summary>Asserts that the subject is false.</summary>
    public AndOperator<TAssertions> BeFalse()
        => Be(false);
}