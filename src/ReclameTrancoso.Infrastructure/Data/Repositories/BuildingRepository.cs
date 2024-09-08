using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data.Context;

namespace Infrastructure.Data.Repositories;

public class BuildingRepository: RepositoryBase<Building>, IBuildingRepository
{
    public BuildingRepository(DataContext context) : base(context)
    {
    }
}