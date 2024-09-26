using Domain.Interfaces;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using ReclameTrancoso.Domain.Models;

namespace Infrastructure.Data.Repositories;

public class ManagerComplaintCommentsRepository : RepositoryBase<ManagerComplaintComments>, IManagerComplaintCommentsRepository
{
    public ManagerComplaintCommentsRepository(DataContext dataContext) : base(dataContext)
    {
    }

    public Task<ManagerComplaintComments?> GetByCommentIdAsync(long? entityId)
    {
        return this.DbSet.Where(x => x.CommentId == entityId).FirstOrDefaultAsync();
    }
}