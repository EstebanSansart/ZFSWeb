namespace Api.Helpers;

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

public static  class JwtHelper
{
    public static ClaimsPrincipal ValidateAndDecodeJwt(string token, string securityKey)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false, // Puedes personalizar estas opciones según tus necesidades
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(securityKey))
        };

        SecurityToken validatedToken;
        try
        {
            return tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
        }
        catch (SecurityTokenException)
        {
            return null; // El token no es válido
        }
    }
}

