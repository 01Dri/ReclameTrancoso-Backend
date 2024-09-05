using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data.Context;

namespace Infrastructure.Data.Repositories;

public class ResidentRepository : RepositoryBase<Resident>, IResidentRepository
{
    public ResidentRepository(DataContext context) : base(context)
    {
    }
}