using Reapit.Platform.Common.Extensions;
using System.Net;

namespace Reapit.Platform.Testing.Helpers;

/// <summary>Mock HTTP message handler.</summary>
public class MockHttpMessageHandler : HttpMessageHandler
{
    private readonly HttpStatusCode _statusCode;
    private readonly string? _response;

    /// <summary>
    /// The URL of the most recent request received by the message handler.
    /// </summary>
    public string? LastRequestUrl { get; private set; }

    /// <summary>
    /// The number of requests received by the message handler.
    /// </summary>
    public int RequestCount { get; private set; }

    /// <summary>Initializes a new instance of the <see cref="MockHttpMessageHandler"/> class.</summary>
    /// <param name="statusCode">The status code that the handler will return.</param>
    /// <param name="response">The body of the response that the handler will return.</param>
    public MockHttpMessageHandler(HttpStatusCode statusCode, object? response)
    {
        _statusCode = statusCode;

        switch (response)
        {
            case null:
                break;
            case string responseString:
                _response = responseString;
                break;
            default:
                _response = response.Serialize();
                break;
        }
    }

    /// <inheritdoc />
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var message = new HttpResponseMessage(_statusCode);
        if (_response is not null)
            message.Content = new StringContent(_response);

        if (request.RequestUri != null)
            LastRequestUrl = request.RequestUri.ToString();

        RequestCount++;
        return Task.FromResult(message);
    }
}