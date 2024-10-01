using Domain.Interfaces;
using Domain.Models.DTOs.Union;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class ManagerRepository: RepositoryBase<Manager>, IManagerRepository
{
    public ManagerRepository(DataContext dataContext) : base(dataContext)
    {
    }

    public async Task<long?> ExistByUserIdAsync(long? userId)
    {
        return  await this.DbSet.Where(x =>  x.UserId == userId)
            .Select(x => x.Id).FirstOrDefaultAsync();
    }
}