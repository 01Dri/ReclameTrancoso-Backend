using Domain.Interfaces;

namespace Domain.Models.DTOs.Auth;

public record TokenResponseDTO(string AccessToken, string RefreshToken, DateTime? AccessTokenExpiresAt, DateTime? RefreshTokenExpiresAt) : IUseCaseResponse;
