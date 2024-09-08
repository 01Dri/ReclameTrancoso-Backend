using Domain.Interfaces;

namespace Domain.Models.DTOs.Auth;

public class LoginRequestDTO : IUseCaseRequest
{
    public string Cpf { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; }= string.Empty;
}