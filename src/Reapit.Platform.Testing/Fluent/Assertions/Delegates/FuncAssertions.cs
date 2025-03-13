using Reapit.Platform.Testing.Fluent.Assertions.Abstract;

namespace Reapit.Platform.Testing.Fluent.Assertions.Delegates;

/// <summary>Contains assertions for method delegates with a return value.</summary>
/// <param name="subject">The assertion subject.</param>
[DebuggerNonUserCode]
public class FuncAssertions<T>(Func<T> subject)
    : DelegateAssertions<Func<T>, FuncAssertions<T>>(subject)
{
    /// <inheritdoc/>
    protected override string Context => "function";

    /// <inheritdoc/>
    protected override void InvokeDelegate() => Subject();
}