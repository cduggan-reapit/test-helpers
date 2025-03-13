namespace Reapit.Platform.Testing.Fluent.Core;

/// <summary>Class used to chain together multiple assertions.</summary>
/// <param name="parent">The parent assertion object.</param>
/// <typeparam name="T">The type of assertion.</typeparam>
[DebuggerNonUserCode]
public class AndOperator<T>(T parent)
{
    /// <summary>Continue making assertions against the subject.</summary>
    public T And { get; } = parent;
}