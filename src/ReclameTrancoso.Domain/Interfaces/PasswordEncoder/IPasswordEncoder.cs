namespace ReclameTrancoso.Domain.Interfaces.PasswordEncoder;

public interface IPasswordEncoder
{
    Task<string> HashPasswordAsync(string password);
}