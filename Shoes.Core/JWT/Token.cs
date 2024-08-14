using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shoes.Core.Entities.Concrete;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Shoes.Core.JWT
{
    public class Token
    {
        private readonly IConfiguration _config;

        public Token(IConfiguration config)
        {
            _config = config;
        }

        public string TokenGenerator(AppUser user, string role)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Token:SecurityKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                 {
                     new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                     new Claim(JwtRegisteredClaimNames.Email, user.Email),
                     new Claim (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                     new Claim (ClaimTypes.Role, role),
                 }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature),
                Issuer = _config["Token:Issuer"],
                Audience = _config["Token:Audience"]
            };

            var token = jwtHandler.CreateToken(tokenDescriptor);
            return jwtHandler.WriteToken(token);
        }
    }
}
