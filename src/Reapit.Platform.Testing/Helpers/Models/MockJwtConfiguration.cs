using Microsoft.IdentityModel.Tokens;

namespace Reapit.Platform.Testing.Helpers.Models;

/// <summary>Configuration object for mock JWTs.</summary>
/// <param name="Audience">The JWT audience.</param>
/// <param name="Issuer">The JWT issuer.</param>
/// <param name="SigningCredentials">The optional signing credentials used to sign the JWT.</param>
/// <param name="Claims">An optional dictionary of claims.</param>
public record MockJwtConfiguration(
    string Audience,
    string Issuer,
    SigningCredentials? SigningCredentials = null,
    Dictionary<string, object>? Claims = null)
{
    internal const string DefaultAudience = "http://test.example.net/audience";
    internal const string DefaultIssuer = "http://test.example.net/issuer";

    /// <summary>Get the default configuration when all you need is a token in the correct format.</summary>
    public static MockJwtConfiguration Default => new(DefaultAudience, DefaultIssuer);
}