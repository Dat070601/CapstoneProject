using BookStore.Models.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStore.Service.TokenGenerators
{
    public class AccessTokenGenerator
    {
        private readonly IConfiguration configuration;
        private readonly TokenGenerator tokenGenerator;
        public AccessTokenGenerator(IConfiguration configuration, TokenGenerator tokenGenerator)
        {
            this.configuration = configuration;
            this.tokenGenerator = tokenGenerator;
        }

        public JwtSecurityToken Generate(Account user, Guid? userShopId, string listCredentials)
        {
            var claims = new[]
            {
                new Claim("Email", user.Email!),
                new Claim("Credentials", listCredentials),
                new Claim("ShopId", userShopId!.Value.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthSettings:AccessTokenSecret"]!));
            var issuer = configuration["AuthSettings:Issuer"];
            var audience = configuration["AuthSettings:Audience"];
            var expires = DateTime.Now.AddMinutes(30); // expires in 30 minutes later
            var token = tokenGenerator.GenerateToken(key, issuer, audience, expires, claims);
            return token;
        }
    }
}
