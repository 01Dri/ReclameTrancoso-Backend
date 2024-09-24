using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Models;
using Domain.Models.DTOs.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ReclameTrancoso.Domain.Interfaces.Auth;

namespace Application.Services;

public class JWTService : ITokenService<User, TokenResponseDTO>
{
    private readonly IConfiguration _configuration;

    public JWTService(IConfiguration configuration)
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
            Expires = DateTime.UtcNow.AddDays(2),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
            Issuer = _configuration["Jwt:Issuer"], Audience = _configuration["Jwt:Audience"]
        };
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        var accessToken  = tokenHandler.WriteToken(token);
        var refreshToken = Guid.NewGuid().ToString();
        var refreshTokenExpires = DateTime.UtcNow.AddDays(7);
        return new TokenResponseDTO(user.ResidentId,accessToken, refreshToken, tokenDescriptor.Expires, refreshTokenExpires);
    }

    public bool ValidateToken(string token)
    {
        var secret = _configuration["Jwt:Key"];
        var key = Encoding.ASCII.GetBytes(secret);

        TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = _configuration["Jwt:Issuer"],
            ValidAudience = _configuration["Jwt:Audience"],
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero 
        };
        
        try
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
            var jwtToken = (JwtSecurityToken)validatedToken;
            return jwtToken.ValidTo > DateTime.UtcNow;
        }
        catch (SecurityTokenExpiredException)
        {
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }
}