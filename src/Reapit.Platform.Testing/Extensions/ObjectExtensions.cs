using System.Text;
using Reapit.Platform.Common.Extensions;

namespace Reapit.Platform.Testing.Extensions;

/// <summary>Extension methods to help in testing <see cref="object"/>s.</summary>
public static class ObjectExtensions
{
    /// <summary>Convert an object to a <see cref="StringContent"/> for use in HTTP requests.</summary>
    /// <param name="obj">The object to convert.</param>
    public static StringContent ToStringContent(this object obj)
        => new(obj as string ?? obj.Serialize(), Encoding.UTF8, "application/json");
}