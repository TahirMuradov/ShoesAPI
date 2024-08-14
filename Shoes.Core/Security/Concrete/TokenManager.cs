using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shoes.Core.Entities.Concrete;
using Shoes.Core.Security.Abstarct;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Shoes.Core.Security.Concrete
{
    public class TokenManager : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;


        public TokenManager(IConfiguration configuration, UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }
        public async Task<Token> CreateAccessTokenAsync(AppUser User, List<string> roles)
        {
            Token token = new();
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, User.Id.ToString()),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

            token.Expiration = DateTime.UtcNow.AddMinutes(2).AddHours(4);
            JwtSecurityToken securityToken = new(
                issuer: _configuration["Token:Audience"],
                audience: _configuration["Token:Issuer"],
                expires: token.Expiration,
                notBefore: DateTime.Now,
                claims: claims,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));


            JwtSecurityTokenHandler tokenHandler = new();

            token.AccessToken = tokenHandler.WriteToken(securityToken);
            token.RefreshToken = CreateRefreshToken();


            await _userManager.AddClaimsAsync(User, claims: claims);

            return token;
        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}
