using System.Security.Claims;
using System.Text;
using Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Infrastructure.Helpers
{
    public class TokenHelper
    {
        public string CreateToken(User model, string secretKey)
        {
            List<Claim> claims = new List<Claim>
            {
              new Claim(ClaimTypes.NameIdentifier, model.Id.ToString()),
              new Claim(ClaimTypes.Name, model.Username),
              new Claim(ClaimTypes.Role, Enum.GetName(model.Role)) // Get role value as a string
            };        
           
            var SimmetricKey = new SymmetricSecurityKey(ASCIIEncoding.UTF8.GetBytes(secretKey));
        
            SigningCredentials credentials  = new SigningCredentials(SimmetricKey, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDecriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(8),
                SigningCredentials = credentials
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDecriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}