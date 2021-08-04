using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Alura.Challenge.BackEnd.Api.Configurations;
using Alura.Challenge.BackEnd.Functions.Extensions;
using Alura.Challenge.Database.Repositories.Interfaces;
using Alura.Challenge.Domain.Interfaces;
using Alura.Challenge.Domain.Models.Data;
using Alura.Challenge.Domain.Models.Responses;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Alura.Challenge.BackEnd.Api.Services
{
    public class GrantTokenService : IGrantTokenService
    {
        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(ConfigurationsWrapper.GetConfiguration().GetSection("Jwt:Secret").Value);
            user.Password = string.Empty;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, "user"),
                    new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(user.GetAs<UserCreatedResponse>()))
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var parsedToken = tokenHandler.WriteToken(token);
            return parsedToken;
        }
    }
}