using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class ApartmentsResidentsRepository : RepositoryBase<ApartmentResident>, IApartmentsResidentsRepository
{
    public ApartmentsResidentsRepository(DataContext dataContext) : base(dataContext)
    {
    }

    public async Task<bool> AlreadyExistOwnerApartment(long? apartmentId)
    {
        return await this.DbSet.AnyAsync(x => x.ApartmentId == apartmentId);
    }
}