using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class UserRepository: RepositoryBase<User>, IUserRepository
{
    public UserRepository(DataContext context) : base(context)
    {
    }

    public async Task<bool> AnyByCPFAsync(string cpf)
    {
        return await this.DbSet.AnyAsync(x => x.Cpf == cpf);
    }

    public async Task<User?> GetByCPFAsync(string cpf)
    {
        
        return await this.DbSet
            .Include(x => x.Token)
            .Where(x => x.Cpf == cpf)
            .FirstOrDefaultAsync();
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        return await this.DbSet
            .Include(x => x.Token)
            .Where(x => x.Email == email)
            .FirstOrDefaultAsync();
    }
}