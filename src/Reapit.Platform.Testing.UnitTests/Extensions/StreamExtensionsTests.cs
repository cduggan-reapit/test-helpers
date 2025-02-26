using Reapit.Platform.Testing.Extensions;

namespace Reapit.Platform.Testing.UnitTests.Extensions;

public static class StreamExtensionsTests
{
    public class RewindAndReadAsFormAsync
    {
        [Fact]
        public async Task Should_ReturnNull_WhenStreamEmpty()
        {
            var stream = new MemoryStream();
            var actual = await stream.RewindAndReadAsFormAsync();
            Assert.Null(actual);
        }
    }
}