using Microsoft.Extensions.DependencyInjection;
using Reapit.Platform.Testing.Extensions;

namespace Reapit.Platform.Testing.UnitTests.Extensions;

public static class ServiceCollectionExtensions
{
    public class RemoveServiceForType
    {
        private sealed class ExampleSingleton(int number)
        {
            public int Number { get; init; } = number;
        }

        [Fact]
        public void Should_RemoveServices()
        {
            var firstServiceCollection = new ServiceCollection();
            firstServiceCollection.AddSingleton<ExampleSingleton>(_ => new ExampleSingleton(1));

            // Accessing the service won't throw here
            _ = firstServiceCollection.BuildServiceProvider().GetRequiredService<ExampleSingleton>();

            var secondServiceCollection = new ServiceCollection();
            secondServiceCollection.AddSingleton<ExampleSingleton>(_ => new ExampleSingleton(2));
            secondServiceCollection.RemoveServiceForType<ExampleSingleton>();

            // But it will throw here
            var ex = Assert.Throws<InvalidOperationException>(() => secondServiceCollection.BuildServiceProvider().GetRequiredService<ExampleSingleton>());
            Assert.Matches("No service for type '.*' has been registered.", ex.Message);
        }
    }
}