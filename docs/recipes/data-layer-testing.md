# Data Layer Testing

Testing the data layer of a project is always a little fuzzy - are they unit tests or integration tests?  Does it matter
whether you use the _real_ database or a substitute?

Our best effort so far uses the combination of a test context factory and a base class for repository tests:

## Test Context Factory

```csharp
public class TestDbContextFactory : IDisposable, IAsyncDisposable
{
    private readonly SqliteConnection _connection = new("Filename=:memory:");
    
    /// <summary>Create a new database context.</summary>
    /// <param name="ensureCreated">Flag indicating whether to ensure that the database is created before returning.</param>
    public MyDbContext CreateContext(bool ensureCreated = true)
        => CreateContextAsync(ensureCreated, CancellationToken.None).Result;
    
    /// <summary>Create a new database context.</summary>
    /// <param name="ensureCreated">Flag indicating whether to ensure that the database is created before returning.</param>
    /// <param name="cancellationToken">The cancellation token</param>
    public async Task<MyDbContext> CreateContextAsync(
        bool ensureCreated = true, 
        CancellationToken cancellationToken = default)
    {
        await _connection.OpenAsync(cancellationToken);
        var context = new MyDbContext(new DbContextOptionsBuilder<MyDbContext>().UseSqlite(_connection).Options);

        if (!ensureCreated) 
            return context;

        await context.Database.EnsureDeletedAsync(cancellationToken);
        await context.Database.EnsureCreatedAsync(cancellationToken);
        return context;
    }
    
    /// <inheritdoc />
    public void Dispose() => _connection.Dispose();
    
    /// <inheritdoc />
    public async ValueTask DisposeAsync() => await _connection.DisposeAsync();
}
```

## Repository Test Class Base

- This implements IAsyncLifetime so we can define asynchronous setup logic.  
- By default, it does nothing, but it can be overridden in implementations.  

```csharp
public abstract class RepositoryTestBase : IAsyncLifetime, IDisposable
{
    private readonly TestDbContextFactory _contextFactory = new();
    private AccessDbContext? _context;

    /// <summary>Get the database provider for the current test context.</summary>
    /// <param name="ensureCreated">Flag indicating whether to ensure that the database is created before returning.</param>
    public AccessDbContext GetContext(bool ensureCreated = true)
        => GetContextAsync(ensureCreated).Result;
    
    /// <summary>Get the database provider for the current test context.</summary>
    /// <param name="ensureCreated">Flag indicating whether to ensure that the database is created before returning.</param>
    /// <param name="cancellationToken">The cancellation token</param>
    public async Task<AccessDbContext> GetContextAsync(bool ensureCreated = true, CancellationToken cancellationToken = default)
        => _context ??= await _contextFactory.CreateContextAsync(ensureCreated, cancellationToken);
    
    /*
     * Lifetime events
     */
    
    /// <summary>Called immediately after the class has been created, before it is used.</summary>
    public virtual Task InitializeAsync() => Task.CompletedTask;
    
    /// <summary>Called when an object is no longer needed. Called just before Dispose() if the class also implements that.</summary>
    public virtual Task DisposeAsync() => Task.CompletedTask;
    
    /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
    public void Dispose()
    {
        _contextFactory.Dispose();
        _context?.Dispose();
    }
}
```

## Example Test

```csharp
public class MyRepositoryTests : RepositoryTestBase
{
    [Fact]
    public async Task GetByIdAsync_ReturnsEntity_WhenRecordFound()
    {
        // Arrange
        const string entityId = "Entity-027";
        var context = await GetContextAsync();
        var sut = CreateSut(context);
        
        // Act
        var actual = await sut.GetById(entityId, CancellationToken.None);
        
        // Assert
        actual.Must().NotBeNull().And.Match(entity => entity.Id == entityId);
    }
    
    /*
     * Lifetime events
     */
    
    /// <inheritdoc />
    public override async Task InitializeAsync()
    {
        // Call custom setup logic before each test. 
        // Thanks to the context factory, we'll always have a clean connection.
        var entities = [...];
        var context = await GetContextAsync(true);
        await context.Entities.AddRangeAsync(entities);
        await context.SaveChangesAsync();
    }
    
    /*
     * Private methods
     */
    
    private static MyRepository CreateSut(MyDbContext context)=> new(context);
}
```