using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Product.Core.Entities;
using Product.Core.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure.Repository
{
    public class TokenServices : ITokenServices
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;

        public TokenServices(IConfiguration config)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["token:key"]));
        }
        public string CreateToken(AppUsers appUsers)
        {
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, appUsers.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, appUsers.DisplayName)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(10),
                Issuer = _config["token:issuer"],
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
