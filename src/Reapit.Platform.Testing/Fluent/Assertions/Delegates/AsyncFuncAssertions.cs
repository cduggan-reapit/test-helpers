using Reapit.Platform.Testing.Fluent.Assertions.Abstract;

namespace Reapit.Platform.Testing.Fluent.Assertions.Delegates;

/// <summary>Contains assertions for method delegates without parameters or return values.</summary>
/// <param name="subject">The assertion subject.</param>
[DebuggerNonUserCode]
public class AsyncFuncAssertions<TResult>(Func<Task<TResult>> subject)
    : AsyncDelegateAssertions<Task<TResult>, AsyncFuncAssertions<TResult>>(subject);