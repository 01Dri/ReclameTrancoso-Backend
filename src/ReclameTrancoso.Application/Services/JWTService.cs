using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Models;
using Domain.Models.DTOs.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ReclameTrancoso.Domain.Interfaces.Auth;

namespace Application.Services;

public class JWTTokenResponseService : ITokenService<User, TokenResponseDTO>
{
    private readonly IConfiguration _configuration;

    public JWTTokenResponseService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public TokenResponseDTO GenerateToken(User user)
    {
        var secret = this._configuration["Jwt:Key"];
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        byte[] key = Encoding.ASCII.GetBytes(secret);
        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Cpf)
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
            Issuer = _configuration["Jwt:Issuer"], Audience = _configuration["Jwt:Audience"]
        };
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        var accessToken  = tokenHandler.WriteToken(token);
        var refreshToken = Guid.NewGuid().ToString();
        var refreshTokenExpires = DateTime.UtcNow.AddDays(7);
        return new TokenResponseDTO(accessToken, refreshToken, tokenDescriptor.Expires, refreshTokenExpires);
    }
}