using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data.Context;

namespace Infrastructure.Data.Repositories;

public class ComplaintRepository  : RepositoryBase<Complaint>, IComplaintRepository
{
    public ComplaintRepository(DataContext dataContext) : base(dataContext)
    {
    }
}