using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace Reapit.Platform.Testing.Helpers;

/// <summary>Factory used to create mock JWKs.</summary>
public static class MockJsonWebKeyFactory
{
    /// <summary>Get a new <see cref="JsonWebKey"/> object with the given key identifier.</summary>
    /// <param name="keyId">The key identifier.</param>
    /// <param name="rsa">The generated RSA key.</param>
    public static JsonWebKey Create(string keyId, out RSA rsa)
    {
        rsa = RSA.Create(2048);
        var pub = new RsaSecurityKey(rsa.ExportParameters(false)) { KeyId = keyId };
        return JsonWebKeyConverter.ConvertFromRSASecurityKey(pub);
    }
}