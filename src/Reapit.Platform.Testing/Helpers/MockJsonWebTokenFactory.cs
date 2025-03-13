using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Reapit.Platform.Common.Providers.Temporal;
using Reapit.Platform.Testing.Helpers.Models;

namespace Reapit.Platform.Testing.Helpers;

/// <summary>Factory used to create mock JWTs.</summary>
public static class MockJsonWebTokenFactory
{
    /// <summary>Get a serialized JWT representing the given configuration.</summary>
    /// <param name="configuration">The JWT configuration.</param>
    public static string Create(MockJwtConfiguration configuration)
    {
        var token = new SecurityTokenDescriptor
        {
            Expires = DateTimeOffsetProvider.Now.UtcDateTime.AddMinutes(20),
            Audience = configuration.Audience,
            Issuer = configuration.Issuer,
            Claims = configuration.Claims
        };

        if (configuration.SigningCredentials != null)
            token.SigningCredentials = configuration.SigningCredentials;

        var handler = new JsonWebTokenHandler();
        return handler.CreateToken(token);
    }

    /// <summary>Read a serialized JWT as a JsonWebToken.</summary>
    /// <param name="token">The JWT.</param>
    public static JsonWebToken Read(string token)
        => new JsonWebTokenHandler().ReadJsonWebToken(token);
}