using Domain.Interfaces;
using Infrastructure.Data.Context;
using ReclameTrancoso.Domain.Models;

namespace Infrastructure.Data.Repositories;

public class ManagerComplaintCommentsRepository : RepositoryBase<ManagerComplaintComments>, IManagerComplaintCommentsRepository
{
    public ManagerComplaintCommentsRepository(DataContext dataContext) : base(dataContext)
    {
    }
}