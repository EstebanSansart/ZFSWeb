using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Entities;
using API.Dtos;
using API.Dtos.Custom;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using Microsoft.VisualBasic;
using System.Security.Cryptography;

namespace API.Services
{
    public class AuthService : IAuthService
    {
        private readonly APIContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(APIContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        private string GenerateToken(string UserCc){
            var key = _configuration.GetValue<string>("JWTSettings:Key");
            var keyBytes = Encoding.ASCII.GetBytes(key);

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, UserCc));

            var credentialToken = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256Signature
            );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = credentialToken
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            string tokenCreated = tokenHandler.WriteToken(tokenConfig);

            return tokenCreated;
        }

        public async Task<AuthResponse> ReturnToken(AuthRequest auth)
        {
            var userFound = _context.Users.FirstOrDefault(x =>
                x.UserCc == auth.Username &&
                x.Password == auth.Password
            );

            if(userFound == null){
                return await Task.FromResult<AuthResponse>(null);
            }

            string tokenCreated = GenerateToken(userFound.UserCc.ToString());

            return new AuthResponse(){Token = tokenCreated, Result = true, Msg = "OK"};
        }
    }
}