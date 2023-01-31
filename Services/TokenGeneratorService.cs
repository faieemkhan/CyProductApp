using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MVCCoreApplication.Services
{
    public class TokenGeneratorService : ITokenGeneratorService
    {
        public string GenerateToken(int id, string name)
        {
            var userClaims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Jti,new Guid().ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName,name)

            };
            var userSecretKey = Encoding.UTF8.GetBytes("adjskhtjfhjggjhftyuh");
            var userSymmetricSecuritKey = new SymmetricSecurityKey(userSecretKey);
            var userSigningCredentials = new SigningCredentials(userSymmetricSecuritKey, SecurityAlgorithms.HmacSha256);
            var userJwtSecurityToken = new JwtSecurityToken(
                issuer: "MVCCoreApplication",
                audience: "MVCCore",
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: userSigningCredentials
                );

            var userSecurityTokenHandler = new JwtSecurityTokenHandler().WriteToken(userJwtSecurityToken);
            return userSecurityTokenHandler;

        }

        public bool IsTokenValid(string userSecretKey, string userIssuer, string userAudience, string userToken)
        {
           var userSecretKeyInBytes = Encoding.UTF8.GetBytes(userSecretKey);
            var userSymmetricSecuritKey = new SymmetricSecurityKey(userSecretKeyInBytes);
            var tokenValidationPrameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer =userIssuer,

                ValidateAudience = true,
                ValidAudience= userAudience,
                ValidateIssuerSigningKey= true,
                IssuerSigningKey = userSymmetricSecuritKey,
                ValidateLifetime= true

            };
            JwtSecurityTokenHandler tokenValidationHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenValidationHandler.ValidateToken(userToken, tokenValidationPrameters,out SecurityToken securityToken);
                return true;
            }
            catch
            {
                return false;
            }

            
        }
    }
}
