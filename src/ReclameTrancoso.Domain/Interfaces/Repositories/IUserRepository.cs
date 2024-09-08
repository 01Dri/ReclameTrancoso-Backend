using Domain.Models;

namespace Domain.Interfaces;

public interface IUserRepository : IRepositoryBase<User>
{
    Task<bool> AnyByCPFAsync(string cpf);
    Task<User?> GetByCPFAsync(string cpf);
    Task<User> GetByEmailAsync(string email);
}