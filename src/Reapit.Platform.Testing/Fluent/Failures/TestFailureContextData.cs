namespace Reapit.Platform.Testing.Fluent.Failures;

/// <summary>Record used to store context data.</summary>
/// <param name="Value">The value.</param>
/// <param name="Reportable">Flag indicating whether the value should be reported.</param>
public record TestFailureContextData(object? Value, bool Reportable);