# Helpers

## Reapit.Platform.Testing.Extensions

### Object Extensions

This package include two object extension methods that tend to be useful in testing:

#### ToStringContent

A shortcut method to convert a `string` or `object` to a `StringContent` object. If a string is provided, it is added
directly into the created object, where an object will be serialized before being added.

The created `StringContent` object will define UTF-8 encoding and either `text/plain` or `application/json` media type.

```csharp
var payload = new { Property = "value" };
var message = new HttpRequestMessage(HttpMethod.Post, "https://example.net");
message.Content = content.ToStringContent()
await message.SendAsync();
```

#### GetPropertyValue

To interrogate an object without first deserializing it, this package provides the `GetPropertyValue` method:

```csharp
var example = { Property = "value" };
var output = example.GetPropertyValue("Property");
// output = "value"
```

### Service Collection Extensions

Designed to help when configuring a test container, this package provides the `RemoveServiceForType` method:

```csharp
public class TestApiFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveServiceForType<IExampleService>();
            services.AddSingleton<IExampleService, MockExampleService>();
        });
    }
}
```

### Stream Extensions

With the change to `IExceptionHandler` for exception handling middleware, it is often necessary to read the stream from
an `HttpContext` instance.  This package provides the `RewindAndReadAsJsonAsync` and `RewindAndReadAsFormAsync` methods
to make it easier to read:

#### Reading JSON Data

```csharp
var content = new ProblemDetails { Status = 200 }.ToStringContent();
var stream = await content.ReadAsStreamAsync();
var actual = await stream.RewindAndReadAsJsonAsync<ProblemDetails>();
// actual = new ProblemDetails { Status = 200 }
```

#### Reading Form Data

```csharp
var formContent = new FormUrlEncodedContent([
    new KeyValuePair<string, string>("1", "one"), 
    new KeyValuePair<string, string>("2", "two"),
    new KeyValuePair<string, string>("3", "three")
]);

var stream = await formContent.ReadAsStreamAsync();

var actual = await stream.RewindAndReadAsFormAsync();
// actual = [["1", ["one"]], ["2", ["two"]], ["3", ["three"]]]
```

## Reapit.Platform.Testing.Helpers

### ApiIntegrationTestBase

A simple base class for controller integration/acceptance test classes. Requires an instance of 
`WebApplicationFactory<TEntryPoint>` which can then be accessed through the `ApiFactory` property.  Provides a shorthand
accessor for `HttpRequestMessageHelper.CreateRequest` under the alias `CreateRequest(HttpMethod, string)`.

### AutoMapperFactory

Do you ever get sick of creating IMapper instances in your tests?  This is meant to make life a little bit easier:
```csharp
// Original:
private readonly IMapper _mapper => new MapperConfiguration(cfg 
        => cfg.AddProfile(typeof(TProfile)))
    .CreateMapper();

// Using AutoMapperFactory:
private readonly IMapper _mapper = AutoMapperFactory.Create<TProfile>();
```

### HttpRequestMessageHelper

Intended to allow integration tests to use fluent-like syntax in configuration

```csharp
public class ApiControllerTests(MyApiFactory apiFactory) : ApiIntegrationTestBase(apiFactory)
{
    private const string BasePath = "/api/controller";
    private readonly IMapper _mapper = AutoMapperFactory.Create<EntityProfile>();
    
    // Test Example
    [Fact]
    public async Task Get_ReturnsOk_WithExpectedContent()
    {
        var expected = { ... };
        
        // HttpRequestMessageHelper methods used to create the request:
        var request = await CreateRequest(HttpMethod.Post, BasePath)
            .SetHeader("x-api-version", "1.0")
            .SetHeader("x-client-scopes", "controller.read", "controller.write")
            .SetStringContent(body)
            .SendAsync(ApiFactory);
        
        request.Must().HaveStatusCode(HttpStatusCode.OK).And.HaveJsonContent(expected);
    }
}
```

#### CreateRequest

The `HttpRequestMessageHelper.CreateRequest(HttpMethod method, string uri)` method is the starting point, creating a new
instance of HttpRequestMessage that further extensions will build upon.  If your test class inherits 
ApiIntegrationTestBase, this method is available as `SendAsync` as there's a method in the base class which calls the 
helper.

```csharp
// Relevant part of test example:
CreateRequest(HttpMethod.Post, BasePath)
```

#### SetHeader

The `SetHeader` extension method can be called to modify the header collection in an `HttpRequestMessage`.  If the 
header has already been registered, it will be replaced.  If `value` is `null`, the header is removed. Value can be 
either `string?` or `params string[]?`.

```csharp
// Relevant part of test example:
.SetHeader("x-api-version", "1.0")
.SetHeader("x-client-scopes", "controller.read", "controller.write")
```

#### SetStringContent

To add `text/plain` or `application/json` content to a request, call `.SetStringContent(object?)`.  If a string is 
provided, the message will be given the `text/plain;charset=UTF-8` media type.  If an object is provided, it will be 
serialized and given the `application/json;charset=UTF-8` media type.

If `null` content is provided, the request content is cleared.

```csharp
// Relevant part of test example:
.SetStringContent(new { ... })
```

#### SetFormContent

To add `application/x-www-form-urlencoded` content to a request, call `SetFormContent(Dictionary<string, string>?)`.  If
a `null` dictionary is provided, the request content is cleared.

#### SendAsync

If a test class inherits ApiIntegrationTestBase, `SendAsync(TApiFactory)` can be called to send the 
HttpRequestMessage to the test web application.  Otherwise, an instance of `WebApplicationFactory` can be provided.

### MockHttpMessageHandler

When testing `HttpClient`, we suggest using `MockHttpMessageHandler` to intercept requests.  Creating an instance of the
handler takes the status code and an optional response payload to return for requests:

```csharp
// NSubstitute for the factory
private readonly IHttpClientFactory _clientFactory = Substitute.For<IHttpClientFactory>();

public async Task Test()
{
    var expectedResult =  new { };
    var handler = new MockHttpMessageHandler(HttpStatusCode.Ok, expectedResult);
    
    _clientFactory.CreateClient().Returns(new HttpClient(handler));
    
    var sut = CreateSut();
    var actual = sut.DoThingThatSendsRequest();
    actual.Must().BeEquivalentTo(expectedResult);
    
    // Properties of MockHttpMessageHandler populated after the request is received:
    handler.RequestCount.Must().Be(1);
    handler.LastRequestUrl.Must().Be("https://expected/url");
}

private MyClass CreateSut()
    => new(_clientFactory);
```

### MockJsonWebKeyFactory

Generally used in authorizers when testing retrieval of well known keys:

```csharp
var jwk = JsonWebKeyFactory.Create("key-id", out var rsa);

// Use to, for example, mock the JWK retrieval service response
_jwkRetrievalService.GetJwkCollectionAsync().Returns(jwk);

// And the outparam used to create the signing credentials used to sign and read mock tokens with MockJsonWebToken:
var signingCredentials = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256)
{
    CryptoProviderFactory = new CryptoProviderFactory { CacheSignatureProviders = false }
};
```

### MockJsonWebTokenFactory

A simple JWT builder with a configuration parameter:

```csharp
var jwt = JsonWebTokenFactory.Create(MockJwtConfiguration.Default);
```

The MockJwtConfiguration allows setting four properties:
- Audience - the token audience (`aud`)
- Issuer - the token issuer (`iss`)
- SigningCredentials (optional) - the signing credentials to use when signing the JWT
- Claims (optional) - a dictionary representing additional claims to include in the token

```csharp
var jwk = JsonWebKeyFactory.Create("key-id", out var rsa);
var signingCredentials = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256)
{
    CryptoProviderFactory = new CryptoProviderFactory { CacheSignatureProviders = false }
};

var claims = new Dictionary<string, object>
{
    { "sub", userId },
    { "username", username },
    { "exp", 1000 }
};

var jwtConfig = new MockJwtConfiguration("audience", "issuer", signingCredentials, claims);
var jwt = JsonWebTokenFactory.Create(jwtConfig);
```