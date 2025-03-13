using Reapit.Platform.Common.Extensions;
using System.Text;

namespace Reapit.Platform.Testing.Extensions;

/// <summary>Extension methods to help in testing <see cref="object"/>s.</summary>
public static class ObjectExtensions
{
    /// <summary>Convert an object to a <see cref="StringContent"/> for use in HTTP requests.</summary>
    /// <param name="obj">The object to convert.</param>
    public static StringContent ToStringContent(this object obj)
        => obj is string str
            ? new StringContent(str, Encoding.UTF8, "text/plain")
            : new StringContent(obj.Serialize(), Encoding.UTF8, "application/json");

    /// <summary>Get a property from an anonymous object by name.</summary>
    /// <param name="obj">The anonymous object.</param>
    /// <param name="propertyName">The property to return.</param>
    public static object? GetPropertyValue(this object? obj, string propertyName)
        => obj?.GetType().GetProperty(propertyName)?.GetValue(obj, null);
}