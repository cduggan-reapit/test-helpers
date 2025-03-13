using Reapit.Platform.Testing.Extensions;

namespace Reapit.Platform.Testing.Helpers;

/// <summary>Extension methods for consistent <see cref="HttpRequestMessage"/> construction using fluent syntax. </summary>
public static class HttpRequestMessageHelper
{
    /// <summary>Create a new instance of HttpRequest message.</summary>
    /// <param name="method">The type of request (e.g. GET, POST).</param>
    /// <param name="url">The URL to which the request should be sent.</param>
    public static HttpRequestMessage CreateRequest(HttpMethod method, string url)
        => new(method, url);

    /// <summary>Add a header value to the request message object.</summary>
    /// <param name="request">The request message object.</param>
    /// <param name="header">The key of the header to add.</param>
    /// <param name="value">The value of the header to add.</param>
    /// <returns>A reference to the request message object after the header has been updated.</returns>
    /// <remarks>
    /// <para>Calling this method with a key that is in use will replace any existing header value.</para>
    /// <para>Calling this method with a null value will remove any existing header value.</para>
    /// </remarks>
    public static HttpRequestMessage SetHeader(this HttpRequestMessage request, string header, string? value)
    {
        if (request.Headers.Contains(header))
            request.Headers.Remove(header);

        if (value is not null)
            request.Headers.Add(header, value);

        return request;
    }

    /// <summary>Add a header value to the request message object.</summary>
    /// <param name="request">The request message object.</param>
    /// <param name="header">The key of the header to add.</param>
    /// <param name="value">The value of the header to add.</param>
    /// <returns>A reference to the request message object after the header has been updated.</returns>
    /// <remarks>
    /// <para>Calling this method with a key that is in use will replace any existing header value.</para>
    /// <para>Calling this method with a null value will remove any existing header value.</para>
    /// </remarks>
    public static HttpRequestMessage SetHeader(this HttpRequestMessage request, string header, params string[]? value)
    {
        if (request.Headers.Contains(header))
            request.Headers.Remove(header);

        if (value is not null)
            request.Headers.Add(header, value);

        return request;
    }

    /// <summary>Adds a application/json content to the request message.</summary>
    /// <param name="request">The request message object.</param>
    /// <param name="content">The object to include in the message body.</param>
    /// <returns>A reference to the request message object after the content has been updated.</returns>
    /// <remarks>
    /// <para>Calling this method will replace any content previously added to the message.</para>
    /// <para>Calling this method with null content will remove any content previously added to the message.</para>
    /// </remarks>
    public static HttpRequestMessage SetStringContent(this HttpRequestMessage request, object? content)
    {
        request.Content = null;

        if (content is not null)
            request.Content = content.ToStringContent();

        return request;
    }

    /// <summary>Adds a application/x-www-form-urlencoded content to the request message.</summary>
    /// <param name="request">The request message object.</param>
    /// <param name="content">The dictionary of values to include in the message body.</param>
    /// <returns>A reference to the request message object after the content has been updated.</returns>
    /// <remarks>
    /// <para>Calling this method will replace any content previously added to the message.</para>
    /// <para>Calling this method with null content will remove any content previously added to the message.</para>
    /// </remarks>
    public static HttpRequestMessage SetFormContent(this HttpRequestMessage request, Dictionary<string, string>? content)
    {
        request.Content = null;

        if (content is not null)
            request.Content = new FormUrlEncodedContent(content);

        return request;
    }
}