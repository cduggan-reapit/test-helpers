using Reapit.Platform.Common.Extensions;

namespace Reapit.Platform.Testing.Fluent.Core;

/// <summary>Extension methods for converting objects to strings in messages.</summary>
public static class ArgumentFormatter
{
    /// <summary>Format the object as a string.</summary>
    /// <param name="input">The object to format.</param>
    public static string Format(this object? input)
    {
        switch (input)
        {
            case null:
                return "<null>";
            case DateTime dt:
                return dt.ToString("s");
            case DateTimeOffset dto:
                return dto.ToString("u");
            case string str:
                return str;
        }

        if (input.GetType().IsPrimitive)
            return input.ToString() ?? "<empty>";

        return input.Serialize();
    }
}