# Testing Exception Handlers

Starting from version 3.0.0, Reapit.Platform.ErrorHandling uses `IExceptionHandler` to introduce custom error handling
into the request pipeline.  Testing these handlers can be a little tricky because it often involves reading a stream.

## Context

For this example, take the exception handler example given in the ErrorHandling package:

```csharp
public class MyFirstExceptionHandler : IExceptionHandler
{
    internal const int StatusCode = 422;
    internal const string Title = "Example Error";
    internal const string Type = "https://www.reapit.com/errors/example-error";
    
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not MyFirstException resolved)
            return false;
  
        var content = new ProblemDetails 
        {
            Title = Title,
            Type = Type,
            Status = StatusCode,
            Detail = resolved.Message
        };
        
        httpContext.Response.StatusCode = StatusCode;
        await httpContext.Response.WriteAsJsonAsync(content, cancellationToken);
        
        return true;
    }
}
```

## Example

```csharp
public class MyFirstExceptionHandlerTests()
{
    /*
     * TryHandleAsync
     */
    
    [Fact]
    public async Task TryHandleAsync_ReturnsFalse_WhenDifferentException()
    {
        // Here, we confirm that the handler returns false (and thus the exception handling middleware moves onto the
        // next handler in the chain) when the exception is not MyFirstException.
        var exception = new InvalidOperationException("example exception");
        var sut = CreateSut();
        var actual = await sut.TryHandleAsync(new DefaultHttpContext(), exception, CancellationToken.None));
        actual.Must().BeFalse();
    }
    
    [Fact]
    public async Task TryHandleAsync_ReturnsTrue_WhenMatchingException()
    {
        // Here we confirm that the handler returns true (and thus the exception handling middleware terminates with the
        // current handler) when the exception is MyFirstException, and that the payload is prepared as expected.
        
        // Setup the context so that we can read what gets written to it
        using var responseStream = new MemoryStream();
        var context = new DefaultHttpContext { Response = { Body = responseStream } };
        
        // Setup the exception
        const string exceptionMessage = "this is the expected message";
        var exception = new MyFirstException(exceptionMessage);
        
        // Prepare the expectation
        var expected = new ProblemDetails 
        {
            Title = MyFirstException.Title,
            Type = MyFirstException.Type,
            Status = MyFirstException.StatusCode,
            Detail = exceptionMessage
        };

        // Check that the call returns true
        var sut = CreateSut();
        var actual = await sut.TryHandleAsync(new DefaultHttpContext(), exception, CancellationToken.None));
        actual.Must().BeTrue();
        
        // Check the response statuscode is set properly
        context.Response.StatusCode.Must().Be(MyFirstException.StatusCode);
        
        // Check that the response content is what we expected
        var content = await context.Response.RewindAndReadAsJsonAsync<ProblemDetails>();
        context.Must().BeEquivalentTo(expected);
    }
    
    /*
     * Private methods
     */
    
    private static MyFirstExceptionHandler CreateSut() => new();
}
```