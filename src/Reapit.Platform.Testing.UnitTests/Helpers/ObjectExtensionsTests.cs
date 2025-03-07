using Reapit.Platform.Common.Extensions;
using Reapit.Platform.Testing.Extensions;

namespace Reapit.Platform.Testing.UnitTests.Helpers;

public static class ObjectExtensionsTests
{
    public class ToStringContent
    {
        [Fact]
        public async Task Should_ReturnStringContent_WithoutSerialization_WhenInputIsAString()
        {
            const string input = "this is the input";
            var actual = input.ToStringContent();
            var content = await actual.ReadAsStringAsync();
            Assert.Equal(input, content);

            // This is just a sanity check.  If we called .Serialize on a string, it would wrap it in quotation marks
            // (e.g. `input string` becoming `"input string"`), which we don't want.
            Assert.NotEqual(input.Serialize(), content);
        }

        [Fact]
        public async Task Should_ReturnStringContent_WithSerialization_WhenInputIsAnObject()
        {
            var input = new { Content = "this is the input" };
            var expected = input.Serialize();

            var actual = input.ToStringContent();
            var content = await actual.ReadAsStringAsync();
            Assert.Equal(expected, content);
        }
    }
}