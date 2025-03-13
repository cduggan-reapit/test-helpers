using Reapit.Platform.Testing.Fluent.Assertions.Abstract;

namespace Reapit.Platform.Testing.Fluent.Assertions.Primitives;

/// <summary>Contains assertions for Object values.</summary>
/// <param name="subject">The assertion subject.</param>
[DebuggerNonUserCode]
public class NullableObjectAssertions(object? subject) : NullableObjectAssertions<NullableObjectAssertions>(subject);

/// <summary>Contains assertions for Object values.</summary>
/// <typeparam name="TAssertions">The type of assertions to return in continuation objects.</typeparam>
[DebuggerNonUserCode]
public class NullableObjectAssertions<TAssertions>(object? subject)
    : NullableReferenceTypeAssertions<object, TAssertions>(subject)
    where TAssertions : NullableObjectAssertions<TAssertions>
{
    /// <summary>The assertion context name.</summary>
    protected override string Context { get; } = "object";
}