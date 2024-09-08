using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data.Context;

namespace Infrastructure.Data.Repositories;

public class BuildingsResidentsRepository : RepositoryBase<BuildingResident>, IBuildingResidentsRepository
{
    public BuildingsResidentsRepository(DataContext dataContext) : base(dataContext)
    {
    }
}