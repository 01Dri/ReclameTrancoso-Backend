using Domain.Interfaces;

namespace Domain.Models.DTOs.Auth;

public class RefreshTokenRequestDTO : IUseCaseRequest
{
    public string RefreshToken { get; set; }
}