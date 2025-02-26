using Reapit.Platform.Testing.Helpers.Models;

namespace Reapit.Platform.Testing.UnitTests.Helpers.Models;

public static class MockJwtConfigurationTests
{
    public class Default
    {
        [Fact]
        public void Should_ReturnExpectedConfiguration()
        {
            var actual = MockJwtConfiguration.Default;
            var expected = new MockJwtConfiguration(MockJwtConfiguration.DefaultAudience, MockJwtConfiguration.DefaultIssuer);
            Assert.Equal(expected, actual);
        }
    }
}