using Domain.Interfaces;
using Infrastructure.Data.Context;
using ReclameTrancoso.Domain.Models;

namespace Infrastructure.Data.Repositories;

public class CommentRepository : RepositoryBase<Comment>, ICommentRepository
{
    public CommentRepository(DataContext dataContext) : base(dataContext)
    {
    }
}