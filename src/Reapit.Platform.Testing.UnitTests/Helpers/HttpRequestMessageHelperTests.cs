using Reapit.Platform.Testing.Extensions;
using Reapit.Platform.Testing.Helpers;

namespace Reapit.Platform.Testing.UnitTests.Helpers;

public static class HttpRequestMessageHelperTests
{
    public class CreateRequest
    {
        [Fact]
        public void Should_CreateObject_WithMethodAndUrl()
        {
            var method = HttpMethod.Options;
            const string url = "https://example.net/expected";

            var actual = HttpRequestMessageHelper.CreateRequest(method, url);
            Assert.IsType<HttpRequestMessage>(actual);
            Assert.NotNull(actual);
            Assert.Equivalent(new Uri(url), actual.RequestUri);
            Assert.Equal(method, actual.Method);
        }
    }

    public class SetHeader
    {
        [Fact]
        public void Should_SetHeader_WhenStringProvided()
        {
            const string key = "x-header-name", value = "x-header-value";
            var message = HttpRequestMessageHelper.CreateRequest(HttpMethod.Connect, "https://test")
                .SetHeader(key, value);

            Assert.Equal([value], message.Headers.GetValues(key));
        }

        [Fact]
        public void Should_ReplaceHeader_WhenStringProvided()
        {
            const string key = "x-header-name", value = "x-header-value";
            var message = HttpRequestMessageHelper.CreateRequest(HttpMethod.Connect, "https://test")
                .SetHeader(key, "initial");
            Assert.Equal(["initial"], message.Headers.GetValues(key));

            message.SetHeader(key, value);
            Assert.Equal([value], message.Headers.GetValues(key));
        }

        [Fact]
        public void Should_RemoveHeader_WhenNullStringProvided()
        {
            const string key = "x-header-name";
            var message = HttpRequestMessageHelper.CreateRequest(HttpMethod.Connect, "https://test")
                .SetHeader(key, "initial");
            Assert.Equal(["initial"], message.Headers.GetValues(key));

            message.SetHeader(key, null as string);
            Assert.False(message.Headers.Contains(key));
        }

        [Fact]
        public void Should_SetHeader_WhenCollectionProvided()
        {
            const string key = "x-header-name";
            var value = new[] { "x-1", "x-2", "x-3" };
            var message = HttpRequestMessageHelper.CreateRequest(HttpMethod.Connect, "https://test")
                .SetHeader(key, value);

            Assert.Equal(value, message.Headers.GetValues(key));
        }

        [Fact]
        public void Should_ReplaceHeader_WhenCollectionProvided()
        {
            const string key = "x-header-name";
            var value = new[] { "x-1", "x-2", "x-3" };
            var message = HttpRequestMessageHelper.CreateRequest(HttpMethod.Connect, "https://test")
                .SetHeader(key, "initial");
            Assert.Equal(["initial"], message.Headers.GetValues(key));

            message.SetHeader(key, value);
            Assert.Equal(value, message.Headers.GetValues(key));
        }

        [Fact]
        public void Should_RemoveHeader_WhenNullCollectionProvided()
        {
            const string key = "x-header-name";
            var message = HttpRequestMessageHelper.CreateRequest(HttpMethod.Connect, "https://test")
                .SetHeader(key, "initial");
            Assert.Equal(["initial"], message.Headers.GetValues(key));

            message.SetHeader(key, null as string[]);
            Assert.False(message.Headers.Contains(key));
        }
    }

    public class SetStringContent
    {
        private sealed record ExampleModel(string Property);

        [Fact]
        public async Task Should_SetContent_WhenDataProvided()
        {
            var content = new ExampleModel("value");

            var message = HttpRequestMessageHelper.CreateRequest(HttpMethod.Post, "https://test")
                .SetStringContent(content);

            var stream = await message.Content!.ReadAsStreamAsync();
            var actual = await stream.RewindAndReadAsJsonAsync<ExampleModel>();
            Assert.Equivalent(content, actual);
            Assert.Equal(content.Property, actual?.Property);
        }

        [Fact]
        public async Task Should_ReplaceContent_WhenDataProvided()
        {
            var firstContent = new ExampleModel("value");
            var secondContent = new ExampleModel("second");

            // Set it up and make sure it's set
            var message = HttpRequestMessageHelper.CreateRequest(HttpMethod.Post, "https://test").SetStringContent(firstContent);
            var firstActual = await (await message.Content!.ReadAsStreamAsync()).RewindAndReadAsJsonAsync<ExampleModel>();
            Assert.Equivalent(firstContent, firstActual);

            // SetStringContent again and make sure it's changed
            message.SetStringContent(secondContent);
            var secondActual = await (await message.Content!.ReadAsStreamAsync()).RewindAndReadAsJsonAsync<ExampleModel>();
            Assert.Equivalent(secondContent, secondActual);
        }

        [Fact]
        public async Task Should_RemoveContent_WhenNullProvided()
        {
            var firstContent = new ExampleModel("value");

            // Set it up and make sure it's set
            var message = HttpRequestMessageHelper.CreateRequest(HttpMethod.Post, "https://test").SetStringContent(firstContent);
            var firstActual = await (await message.Content!.ReadAsStreamAsync()).RewindAndReadAsJsonAsync<ExampleModel>();
            Assert.Equivalent(firstContent, firstActual);

            message.SetStringContent(null);
            Assert.Null(message.Content);
        }
    }

    public class SetFormContent
    {
        [Fact]
        public async Task Should_SetContent_WhenDataProvided()
        {
            var content = new Dictionary<string, string> { { "Property", "value" } };
            var message = HttpRequestMessageHelper.CreateRequest(HttpMethod.Post, "https://test")
                .SetFormContent(content);

            var stream = await message.Content!.ReadAsStreamAsync();
            var actual = (await stream.RewindAndReadAsFormAsync())!.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString());
            Assert.Equivalent(content, actual);
        }

        [Fact]
        public async Task Should_ReplaceContent_WhenDataProvided()
        {
            var firstContent = new Dictionary<string, string> { { "Property", "first" } };
            var secondContent = new Dictionary<string, string> { { "Property", "second" } };

            // Set it up and make sure it's set
            var message = HttpRequestMessageHelper.CreateRequest(HttpMethod.Post, "https://test").SetFormContent(firstContent);
            var actual = await (await message.Content!.ReadAsStreamAsync()).RewindAndReadAsFormAsync();
            var actualDict = actual!.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString());
            Assert.Equivalent(firstContent, actualDict);

            message.SetFormContent(secondContent);
            actual = await (await message.Content!.ReadAsStreamAsync()).RewindAndReadAsFormAsync();
            actualDict = actual!.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString());
            Assert.Equivalent(secondContent, actualDict);
        }

        [Fact]
        public async Task Should_RemoveContent_WhenNullProvided()
        {
            var firstContent = new Dictionary<string, string> { { "Property", "first" } };

            // Set it up and make sure it's set
            var message = HttpRequestMessageHelper.CreateRequest(HttpMethod.Post, "https://test").SetFormContent(firstContent);
            var firstActual = await (await message.Content!.ReadAsStreamAsync()).RewindAndReadAsFormAsync();
            Assert.Equivalent(firstContent, firstActual!.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString()));

            message.SetFormContent(null);
            Assert.Null(message.Content);
        }
    }
}