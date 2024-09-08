namespace ReclameTrancoso.Domain.Interfaces.PasswordEncoder;

public interface IPasswordEncoder
{
    Task<string> HashPasswordAsync(string password);
    Task<bool> IsValidAsync(string password, string encodedPassword);
}