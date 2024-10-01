using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class ResidentRepository : RepositoryBase<Resident>, IResidentRepository
{
    public ResidentRepository(DataContext context) : base(context)
    {
    }

    public async Task<bool> AnyByEmailAsync(string email)
    {
        return await this.DbSet.AnyAsync(X => X.Email == email);
    }
    
    public async Task<bool> AnyByCPFAsync(string cpf)
    {
        return await this.DbSet.AnyAsync(X => X.Cpf == cpf);
    }

    public async Task<Resident?> GetResidentByIdAsync(long id)
    {
        return await this.DbSet
            .Include( x => x.BuildingResidents)
            .Include( x => x.ApartmentResidents)
            .Include( x => x.User)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<long?> ExistByUserIdAsync(long? userId)
    {
        return  await this.DbSet.Where(x =>  x.UserId == userId)
            .Select(x => x.Id).FirstOrDefaultAsync();
    }
}