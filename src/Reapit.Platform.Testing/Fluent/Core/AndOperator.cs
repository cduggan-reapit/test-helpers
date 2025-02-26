namespace Reapit.Platform.Testing.Fluent.Core;

[DebuggerNonUserCode]
public class AndOperator<T>(T parent)
{
    /// <summary>Continue making assertions against the subject.</summary>
    public T And { get; } = parent;
}