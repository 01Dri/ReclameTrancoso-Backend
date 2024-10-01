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

    public async Task DeleteByComplaintIdAsync(long complaintId)
    {
        var entity = await this.DbSet.Where(x =>
            x.ComplaintId == complaintId).FirstOrDefaultAsync();

        if (entity != null)
        {
            this.DbSet.Remove(entity);
        }
        
    }
}