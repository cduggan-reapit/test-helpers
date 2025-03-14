# Test API Factory

Where it used to be quite tricky to spin up test APIs, it has gotten much easier with IClassFixture,
WebApplicationFactory, and minimal program files.

## Example

### MyNamespace.Program.cs

- To allow everything to gel, we add a small partial Program class to the bottom of `Program.cs`.

```csharp
var builder = WebApplication.CreateBuilder(args);

// Configure the service container

var app = builder.Build(); 

// Configure the request pipeline

app.Run();

namespace MyNamespace
{
    /// <summary>Class description allowing test service injection.</summary>
    public partial class Program { }
}
```

### MyNamespace.IntegrationTests.TestApiFactory.cs

- There will be times when we need to swap out a service for a mock concretion.  The `WebApplicationFactory` class 
  provided by `Microsoft.AspNetCore.Mvc.Testing` makes this really easy: 

```csharp
namespace MyNamespace.IntegrationTests;

public class TestApiFactory : WebApplicationFactory<MyNamespace.Program>
{
    /// <summary>Gives a fixture an opportunity to configure the application before it gets built.</summary>
    /// <param name="builder">The <see cref="IWebHostBuilder"/> for the application</param>
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // example: set the application environment
        builder.UseEnvironment("Testing");
        
        // example: swapping a real database connection for an in-memory SQLite database:
        services.RemoveServiceForType<DbContextOptions<MyDbContext>>():
        
        services.AddSingleton<DbConnection>(_ => 
        {
            var connection = new SqliteConnection("DataSource=:memory@");
            connection.Open();
            return connection;
        });
        
        services.AddDbContext<MyDbContext>((serviceProvider, options) => 
        {
            var connection = serviceProvider.GetRequiredService<DbConnection>();
            options.UseSqlite(connection);
            options.EnableSensitiveDataLogging();
        });
        
        // example: swap out a service concretion:
        services.RemoveServiceForType<IIdentityProviderService>();
        services.AddSingleton<IIdentityProviderService, MockIdentityProviderService>();
    }
}
```

### MyNamespace.IntegrationTests.Controllers.ExampleControllerTests.cs

- To keep the tests isolated, we tear down and restore an instance of the database on a per-test basis.  
- If a test is expected to return before reaching the database (e.g. version or permission errors), we can skip the call
  to `InitializeDatabaseAsync` - since we're using in-memory SQLite databases it's not a huge saving (1-3ms in the 
  organisations v2 services)
- If all tests require a database, having the test class implement `IAsyncLifetime` would be preferred.

```csharp
namespace MyNamespace.IntegrationTests.Controllers.Examples;

public class ExampleControllerTests(TestApiFactory apiFactory) : ApiIntegrationTestBase(apiFactory)
{
    private readonly IMapper _mapper = AutoMapperFactory.Create<ExampleProfile>();
    private const string BasePath = "/";
    
    /*
     * Get
     */
    
    [Fact]
    public async Task GetExamples_ReturnsOk_WhenRequestSuccessful()
    {
        await InitializeDatabaseAsync();
        var expected = _mapper.Map<IEnumerable<Example>>(SeedData.Take(3));
        
        var response = await CreateRequest(HttpMethod.Get, $"{BasePath}?pageSize=3")
            .SetHeader("x-api-version", "1.0")
            .SetHeader("x-client-scopes", "examples.read", "examples.write")
            .SendAsync(ApiFactory);
        
        response.Must().HaveStatusCode(HttpStatus.OK).And.HaveJsonContent(expected);
    }
    
    /*
     * Private methods
     */
    
    private async Task InitializeDatabaseAsync()
    {
        await using var scope = ApiFactory.Services.CreateAsyncScope();
        var serviceProvider = scope.ServiceProvider;
        var dbContext = serviceProvider.GetRequiredService<AccessDbContext>();

        _ = await dbContext.Database.EnsureDeletedAsync();
        _ = await dbContext.Database.EnsureCreatedAsync();

        await dbContext.Apps.AddRangeAsync(SeedData);
        await dbContext.SaveChangesAsync();
    }
}
```
