using System.Text;

namespace Reapit.Platform.Testing.Fluent.Failures;

/// <summary>Builder for text failure exceptions.</summary>
public class TestFailureBuilder
{
    /// <summary>Gets an instance of <see cref="TestFailureBuilder"/> for the defined context.</summary>
    /// <param name="context">The test context.</param>
    public static TestFailureBuilder CreateForContext(string context)
        => new TestFailureBuilder().SetContext(context);

    /// <summary>The test context.</summary>
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public string? Context { get; private set; }

    /// <summary>The error message template.</summary>
    private string? MessageTemplate { get; set; }

    /// <summary>The inner exception to include in built exceptions.</summary>
    private Exception? InnerException { get; set; }

    /// <summary>The context data collection.</summary>
    /// <remarks>
    /// Context data can be substituted in error message strings using reference in the format <c>{key}</c>.
    /// </remarks>
    private Dictionary<string, TestFailureContextData> ContextData { get; } = new()
    {
        { "null", new TestFailureContextData(null, false) }
    };

    /// <summary>Set the name of the context for the current test.</summary>
    /// <param name="context">The name of the context.</param>
    private TestFailureBuilder SetContext(string context)
    {
        Context = context;
        SetContextData("context", context, false);

        return this;
    }

    /// <summary>Sets the context data value for a given key.</summary>
    /// <param name="key">The key of the context data entry.</param>
    /// <param name="value">The value of the context data entry.</param>
    /// <param name="isReportable">Flag indicating whether the value should be included in the context data output.</param>
    /// <returns>A reference to the TestFailureBuilder after the operation has been performed.</returns>
    public TestFailureBuilder SetContextData(string key, object? value, bool isReportable = true)
    {
        ContextData.Add(key, new TestFailureContextData(value, isReportable));
        return this;
    }

    /// <summary>Sets the message template for the built exception.</summary>
    /// <param name="template">The message template.</param>
    /// <returns>A reference to the TestFailureBuilder after the operation has been performed.</returns>
    public TestFailureBuilder SetMessageTemplate(string template)
    {
        MessageTemplate = template;
        return this;
    }

    /// <summary>Sets the inner exception for the built exception.</summary>
    /// <param name="exception">The inner exception.</param>
    /// <returns>A reference to the TestFailureBuilder after the operation has been performed.</returns>
    public TestFailureBuilder SetInnerException(Exception exception)
    {
        InnerException = exception;
        return this;
    }

    /// <summary>Creates an exception representing the configured test failure.</summary>
    public XunitException Build() => new(BuildMessage(), InnerException);

    /*
     * Private methods
     */

    private string BuildMessage()
    {
        var sb = new StringBuilder();

        // First step: work through the template and replace stuff.  It's recursive, so we'll keep going until there's no {key} matches.
        var message = MessageTemplate;
        var substitutionKeys = ContextData.Select(cd => $"{{{cd.Key}}}").ToList();
        var iterations = 0;
        while (substitutionKeys.Any(k => message is not null && message.Contains(k, StringComparison.OrdinalIgnoreCase)))
        {
            message = ContextData.Aggregate(
                seed: message,
                func: (current, substitute)
                    => current?.Replace($"{{{substitute.Key}}}", Core.ArgumentFormatter.Format(substitute.Value.Value), StringComparison.OrdinalIgnoreCase));

            // Safety valve
            if (iterations++ >= 5) break;
        }

        sb.AppendLine(message);

        var reportable = ContextData.Where(cd => cd.Value.Reportable).ToDictionary();
        if (!reportable.Any())
            return sb.ToString();

        sb.AppendLine(new string('-', 50));

        var maxKeyLength = reportable.Max(item => item.Key.Length) + 2;
        foreach (var item in reportable)
            sb.Append($"{item.Key}:".PadRight(maxKeyLength, ' ')).AppendLine(Core.ArgumentFormatter.Format(item.Value.Value).Replace(Environment.NewLine, $"{Environment.NewLine}{new string(' ', 4)}"));

        sb.AppendLine(new string('-', 50));

        return sb.ToString();
    }
}