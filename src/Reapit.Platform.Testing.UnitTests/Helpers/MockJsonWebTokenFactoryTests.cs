using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Reapit.Platform.Testing.Helpers;
using Reapit.Platform.Testing.Helpers.Models;

namespace Reapit.Platform.Testing.UnitTests.Helpers;

public static class JsonWebTokenFactoryTests
{
    private const string Subject = "subject";
    private const string Username = "username";
    private const string Organisation = "organisation";
    private const string Gty = "authorization_code";
    private const string Scope = "scope.one scope.two scope.three";

    public class Create
    {
        [Fact]
        public async Task Should_CreateJwt_WithoutSigningCredentials()
        {
            var claims = new Dictionary<string, object>
            {
                { "sub", Subject },
                { "username", Username },
                { "organisation", Organisation },
                { "gty", Gty },
                { "scope", Scope }
            };

            // Use the factory to create the token:
            var jwtConfig = new MockJwtConfiguration(MockJwtConfiguration.DefaultAudience, MockJwtConfiguration.DefaultIssuer, null, claims);
            var token = MockJsonWebTokenFactory.Create(jwtConfig);

            // Read the token (manually)
            var handler = new JsonWebTokenHandler();
            var actual = handler.ReadJsonWebToken(token);
            var actualClaims = actual.Claims.ToDictionary(claim => claim.Type, claim => claim.Value);

            // Everything in `claims` should be in the token claim collection.
            Assert.All(claims, claim => Assert.Equal(claim.Value, actualClaims[claim.Key]));

            // Last: check the token's invalid (as the signature will be bad):
            var validationConfiguration = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuers = [MockJwtConfiguration.DefaultIssuer],
                ValidateAudience = true,
                ValidAudiences = [MockJwtConfiguration.DefaultAudience],
                ValidateLifetime = true,
                RequireExpirationTime = true
            };
            var result = await handler.ValidateTokenAsync(token, validationConfiguration);
            Assert.IsType<SecurityTokenInvalidSignatureException>(result.Exception);
        }

        [Fact]
        public async Task Should_CreateJwt_WithSigningCredentials()
        {
            var claims = new Dictionary<string, object>
            {
                { "sub", Subject },
                { "username", Username },
                { "organisation", Organisation },
                { "gty", Gty },
                { "scope", Scope }
            };

            var jsonWebKey = MockJsonWebKeyFactory.Create("key-id", out var rsa);
            var signingCredentials = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256)
            {
                CryptoProviderFactory = new CryptoProviderFactory { CacheSignatureProviders = false }
            };

            // Use the factory to create the token:
            var jwtConfig = new MockJwtConfiguration(MockJwtConfiguration.DefaultAudience, MockJwtConfiguration.DefaultIssuer, signingCredentials, claims);
            var token = MockJsonWebTokenFactory.Create(jwtConfig);

            // Read the token (manually)
            var handler = new JsonWebTokenHandler();
            var actual = handler.ReadJsonWebToken(token);

            var actualClaims = actual.Claims.ToDictionary(claim => claim.Type, claim => claim.Value);

            // Everything in `claims` should be in the token claim collection.
            Assert.All(claims, claim => Assert.Equal(claim.Value, actualClaims[claim.Key]));

            // Last: check the token's valid:
            var validationConfiguration = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuers = [MockJwtConfiguration.DefaultIssuer],
                ValidateAudience = true,
                ValidAudiences = [MockJwtConfiguration.DefaultAudience],
                ValidateLifetime = true,
                RequireExpirationTime = true,
                IssuerSigningKeyResolver = (_, _, _, _) => [jsonWebKey]
            };
            var result = await handler.ValidateTokenAsync(token, validationConfiguration);
            Assert.True(result.IsValid);
        }
    }

    public class Read
    {
        [Fact]
        public void Should_ReturnToken()
        {
            var claims = new Dictionary<string, object>
            {
                { "sub", Subject },
                { "username", Username },
                { "organisation", Organisation },
                { "gty", Gty },
                { "scope", Scope }
            };

            _ = MockJsonWebKeyFactory.Create("key-id", out var rsa);
            var signingCredentials = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256)
            {
                CryptoProviderFactory = new CryptoProviderFactory { CacheSignatureProviders = false }
            };

            // Use the factory to create the token:
            var jwtConfig = new MockJwtConfiguration(MockJwtConfiguration.DefaultAudience, MockJwtConfiguration.DefaultIssuer, signingCredentials, claims);
            var token = MockJsonWebTokenFactory.Create(jwtConfig);

            // Read the token manually
            var handler = new JsonWebTokenHandler();
            var expected = handler.ReadJsonWebToken(token);

            // Read the token with the helper method
            var actual = MockJsonWebTokenFactory.Read(token);
            Assert.Equivalent(expected, actual);
        }
    }
}