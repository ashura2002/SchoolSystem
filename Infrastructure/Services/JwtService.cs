using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Setting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        private readonly JwtSetting _jwtSetting;


        public JwtService(IOptions<JwtSetting> jwtSetting)
        {
            _jwtSetting = jwtSetting.Value;
        }

        public string GenerateToken(User user)
        {

            List<Claim> claims = new List<Claim>()
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.Username.Value),
                new(ClaimTypes.Role, user.Role.ToString())
            };

            // create security key - get key from the app setting
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Key)) ??
                throw new Exception("JWT Key is missing. Check appsettings configuration.");

            // sign credentials
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            // actual Jwt is created
            var accessToken = new JwtSecurityToken(issuer: _jwtSetting.Issuer,
                            audience: _jwtSetting.Audience,
                            claims: claims,
                            expires: DateTime.UtcNow.AddHours(_jwtSetting.ExpiryInHours),
                            signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(accessToken);

        }
    }
}
