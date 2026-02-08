using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FinanceApi.Models;
using Microsoft.IdentityModel.Tokens;

namespace FinanceApi.Services
{
    public class TokenService
    {
        public static string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("SuaChaveSuperSecretaComMaisDe32Caracteres");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim("id", user.Id.ToString()), // Guarda o id no token
                    new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User")// Guarda o tipo do user no token
                ]),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}