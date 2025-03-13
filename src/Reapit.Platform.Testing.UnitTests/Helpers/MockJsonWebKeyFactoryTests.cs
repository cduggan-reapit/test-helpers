using Reapit.Platform.Testing.Helpers;

namespace Reapit.Platform.Testing.UnitTests.Helpers;

public static class MockJsonWebKeyFactoryTests
{
    public class Create
    {
        [Fact]
        public void Should_ReturnJsonWebKeys_WithRsaKey()
        {
            var jwk = MockJsonWebKeyFactory.Create("example", out _);
            Assert.Equal("example", jwk.Kid);
        }
    }
}