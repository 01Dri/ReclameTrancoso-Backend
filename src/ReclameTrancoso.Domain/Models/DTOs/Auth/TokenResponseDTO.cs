using Domain.Interfaces;

namespace Domain.Models.DTOs.Auth;

public record TokenResponseDTO(
    long? ResidentId,
    string AccessToken,
    string RefreshToken,
    DateTime? AccessTokenExpiresAt,
    DateTime? RefreshTokenExpiresAt) : IUseCaseResponse;
