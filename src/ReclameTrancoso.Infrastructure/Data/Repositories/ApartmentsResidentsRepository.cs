using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data.Context;

namespace Infrastructure.Data.Repositories;

public class ApartmentsResidentsRepository : RepositoryBase<ApartmentResident>, IApartmentsResidentsRepository
{
    public ApartmentsResidentsRepository(DataContext dataContext) : base(dataContext)
    {
    }
}