using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookTaxi.Common2.Models.Jwt;
using BookTaxiEntyties.Entyties;
using System.Security.Cryptography;

namespace BookTaxi.Services.JwtService;

public class JwtTokenService
{
    private readonly IConfiguration _configuration;
    private readonly JwtSettings _jwtSettings;
    public JwtTokenService(IConfiguration configuration, IOptions<JwtSettings> jwtSettings)
    {
        _configuration = configuration;
        _jwtSettings = _configuration.GetSection("JwtSettings").Get<JwtSettings>();
    }
    public string GenerateToken(User user)
    {
        var key = new byte[32]; // 256 бит
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(key);
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, user.Role)
        }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
