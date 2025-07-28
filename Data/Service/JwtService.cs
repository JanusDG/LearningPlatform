using System;
using Microsoft.EntityFrameworkCore;
using LearningPlatform.Models;

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using LearningPlatform.Models;
using LearningPlatform.Handlers;


namespace LearningPlatform.Data.Service
{


    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly LearningPlatformDbContext _context;
        public JwtService(LearningPlatformDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        public async Task<LoginResponseModel?> AuthenticateAsync(LoginRequestModel loginRequest)
        {
            var username = loginRequest.Username;
            var password = loginRequest.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return null;
            }
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null || !PasswordHashHandler.VerifyHashedPassword(user.Password, password))
            {
                return null;
            }


            return new LoginResponseModel
            {
                AccessToken = GenerateToken(user.Username, user.Role, user.Id),
                Username = user.Username,
                Role = user.Role,
                ExpiresAt = DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("JwtConfig:ExpirationMinutes"))
            };
        }

        public string GenerateToken(string username, string role, int id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var issuer = _configuration["JwtConfig:Issuer"];
            var audience = _configuration["JwtConfig:Audience"];
            var key = _configuration["JwtConfig:Key"];
            var expirationMinutes = _configuration.GetValue<int>("JwtConfig:ExpirationMinutes");
            var expiresAt = DateTime.UtcNow.AddMinutes(expirationMinutes);
            var keyBytes = Encoding.UTF8.GetBytes(key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = expiresAt,
                Issuer = _configuration["JwtConfig:Issuer"],
                Audience = _configuration["JwtConfig:Audience"],
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(keyBytes),
                    SecurityAlgorithms.HmacSha256Signature
                )

            };


            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(securityToken);
            return accessToken;
        }
    }
}