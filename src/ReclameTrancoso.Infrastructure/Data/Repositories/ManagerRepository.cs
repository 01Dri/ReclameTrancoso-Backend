using Domain.Interfaces;
using Domain.Models.DTOs.Union;
using Infrastructure.Data.Context;

namespace Infrastructure.Data.Repositories;

public class ManagerRepository: RepositoryBase<Manager>, IManagerRepository
{
    public ManagerRepository(DataContext dataContext) : base(dataContext)
    {
    }
}