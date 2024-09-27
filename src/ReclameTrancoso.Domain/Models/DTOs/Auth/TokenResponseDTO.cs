using Domain.Interfaces;

namespace Domain.Models.DTOs.Auth;

public class TokenResponseDTO : IUseCaseResponse
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime? AccessTokenExpiresAt { get; set; }
    public DateTime? RefreshTokenExpiresAt { get; set; }
    public EntityIdResponseDTO? EntityId { get; set; }

    // Construtor
    public TokenResponseDTO(string accessToken, string refreshToken, DateTime? accessTokenExpiresAt, DateTime? refreshTokenExpiresAt, EntityIdResponseDTO? entityId)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        AccessTokenExpiresAt = accessTokenExpiresAt;
        RefreshTokenExpiresAt = refreshTokenExpiresAt;
        EntityId = entityId;
    }
}