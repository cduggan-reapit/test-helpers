using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using Reapit.Platform.Common.Extensions;
using System.IO.Pipelines;

namespace Reapit.Platform.Testing.Extensions;

/// <summary>Extension methods to help reading <see cref="Stream"/> objects in a test context.</summary>
public static class StreamExtensions
{
    /// <summary>Rewinds and reads the contents of a stream, deserializing to <typeparamref name="T"/>.</summary>
    /// <param name="stream">The stream.</param>
    /// <typeparam name="T">The type to deserialize to.</typeparam>
    public static async Task<T?> RewindAndReadAsJsonAsync<T>(this Stream stream)
    {
        // Rewind...
        stream.Position = 0;

        // Read...
        var content = await new StreamReader(stream).ReadToEndAsync();

        // AsJson!
        return content.Deserialize<T>();
    }

    /// <summary>Rewinds and reads the contents of a stream into a dictionary representing the body of a x-www-form-urlencoded request.</summary>
    /// <param name="stream">The stream.</param>
    public static async Task<Dictionary<string, StringValues>?> RewindAndReadAsFormAsync(this Stream stream)
    {
        if (stream.Length == 0)
            return null;

        var reader = new FormPipeReader(PipeReader.Create(stream));
        return await reader.ReadFormAsync();
    }
}