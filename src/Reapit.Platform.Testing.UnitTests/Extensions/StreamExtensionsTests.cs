using Microsoft.AspNetCore.Mvc;
using Reapit.Platform.Testing.Extensions;

namespace Reapit.Platform.Testing.UnitTests.Extensions;

public static class StreamExtensionsTests
{
    public class RewindAndReadAsJsonAsync
    {
        [Fact]
        public async Task Should_ReturnNull_WhenStreamEmpty()
        {
            var stream = await "null".ToStringContent().ReadAsStreamAsync();
            var actual = await stream.RewindAndReadAsJsonAsync<object>();
            Assert.Null(actual);
        }

        [Fact]
        public async Task Should_ReturnDictionary_WhenStreamPopulated()
        {
            var content = new ProblemDetails { Status = 200 }.ToStringContent();
            var stream = await content.ReadAsStreamAsync();
            var actual = await stream.RewindAndReadAsJsonAsync<ProblemDetails>();
            actual.Must().NotBeNull().And.Match(pd => pd.Status == 200);
        }
    }

    public class RewindAndReadAsFormAsync
    {
        [Fact]
        public async Task Should_ReturnNull_WhenStreamEmpty()
        {
            var stream = new MemoryStream();
            var actual = await stream.RewindAndReadAsFormAsync();
            Assert.Null(actual);
        }

        [Fact]
        public async Task Should_ReturnDictionary_WhenStreamPopulated()
        {
            var content = new FormUrlEncodedContent([
                new KeyValuePair<string, string>("1", "one"),
                new KeyValuePair<string, string>("2", "two"),
                new KeyValuePair<string, string>("3", "three")
            ]);
            var stream = await content.ReadAsStreamAsync();
            var actual = await stream.RewindAndReadAsFormAsync();
            actual.Must().Contain("1", "one").And.Contain("2", "two").And.Contain("3", "three");
        }
    }
}