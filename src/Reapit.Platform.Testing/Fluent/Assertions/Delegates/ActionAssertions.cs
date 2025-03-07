using Reapit.Platform.Testing.Fluent.Assertions.Abstract;

namespace Reapit.Platform.Testing.Fluent.Assertions.Delegates;

/// <summary>Contains assertions for method delegates without parameters or return values.</summary>
/// <param name="subject">The assertion subject.</param>
[DebuggerNonUserCode]
public class ActionAssertions(Action subject)
    : DelegateAssertions<Action, ActionAssertions>(subject)
{
    /// <inheritdoc/>
    protected override string Context => "action";

    /// <inheritdoc/>
    protected override void InvokeDelegate() => Subject();
}