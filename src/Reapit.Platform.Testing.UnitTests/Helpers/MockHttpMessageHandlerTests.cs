using Reapit.Platform.Common.Extensions;
using Reapit.Platform.Testing.Helpers;
using System.Net;

namespace Reapit.Platform.Testing.UnitTests.Helpers;

public static class MockHttpMessageHandlerTests
{
    public class SendAsync
    {
        [Fact]
        public async Task Should_ReturnResponseMessageWithoutContent_WhenNoResponseModelConfigured()
        {
            const string url = "http://example.net/expected";
            var sut = new MockHttpMessageHandler(HttpStatusCode.NotAcceptable, null);

            var client = new HttpClient(sut);
            var actual = await client.GetAsync(url);

            Assert.Equal(HttpStatusCode.NotAcceptable, actual.StatusCode);
            Assert.Empty(await actual.Content.ReadAsStringAsync());
            Assert.Equal(1, sut.RequestCount);
            Assert.Equal(url, sut.LastRequestUrl);
        }

        [Fact]
        public async Task Should_ReturnResponseMessageWithStringContent_WhenStringResponseConfigured()
        {
            const string url = "http://example.net/expected";
            var payload = "I'm a string of sorts";
            var sut = new MockHttpMessageHandler(HttpStatusCode.NotAcceptable, payload);

            var client = new HttpClient(sut);
            var actual = await client.GetAsync(url);

            Assert.Equal(HttpStatusCode.NotAcceptable, actual.StatusCode);
            Assert.Equivalent(payload, await actual.Content.ReadAsStringAsync());
            Assert.Equal(1, sut.RequestCount);
            Assert.Equal(url, sut.LastRequestUrl);
        }

        [Fact]
        public async Task Should_ReturnResponseMessageWithSerializedContent_WhenObjectResponseConfigured()
        {
            const string url = "http://example.net/expected";
            var payload = new { Property = "value" };
            var sut = new MockHttpMessageHandler(HttpStatusCode.NotAcceptable, payload);

            var client = new HttpClient(sut);
            var actual = await client.GetAsync(url);

            Assert.Equal(HttpStatusCode.NotAcceptable, actual.StatusCode);
            Assert.Equivalent(payload.Serialize(), await actual.Content.ReadAsStringAsync());
            Assert.Equal(1, sut.RequestCount);
            Assert.Equal(url, sut.LastRequestUrl);
        }
    }
}