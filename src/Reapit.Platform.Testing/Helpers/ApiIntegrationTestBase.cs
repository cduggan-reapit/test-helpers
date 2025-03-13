using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Reapit.Platform.Testing.Helpers;

/// <summary>Base class extended by controller integration test classes to share common HTTP request actions.</summary>
/// <param name="apiFactory">The factory for bootstrapping an application in memory for functional end-to-end tests.</param>
/// <typeparam name="TApiFactory">The factory type.</typeparam>
/// <typeparam name="TEntryPoint">A type in the entry point assembly of the application. Typically, the Program class.</typeparam>
public abstract class ApiIntegrationTestBase<TApiFactory, TEntryPoint>(TApiFactory apiFactory) : IClassFixture<TApiFactory>
    where TApiFactory : WebApplicationFactory<TEntryPoint>
    where TEntryPoint : class
{
    /// <summary>The API factory.</summary>
    protected readonly TApiFactory ApiFactory = apiFactory;

    /// <summary>Create a new instance of HttpRequest message.</summary>
    /// <param name="method">The type of request (e.g. GET, POST).</param>
    /// <param name="url">The URL to which the request should be sent.</param>
    public static HttpRequestMessage CreateRequest(HttpMethod method, string url) =>
        HttpRequestMessageHelper.CreateRequest(method, url);
}