using ReclameTrancoso.Domain.Interfaces.PasswordEncoder;

namespace Infrastructure.Security;

public class BCryptPasswordEncoder : IPasswordEncoder
{
    public async Task<string> HashPasswordAsync(string password)
        => await Task.FromResult(this.HashPassword(password));

    public async Task<bool> IsValidAsync(string password, string encodedPassword)
        => await Task.FromResult(BCrypt.Net.BCrypt.Verify(password, encodedPassword));

    private string HashPassword(string password)
        => BCrypt.Net.BCrypt.HashPassword(password);
}