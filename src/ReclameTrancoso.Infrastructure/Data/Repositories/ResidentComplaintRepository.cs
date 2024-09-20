using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data.Context;

namespace Infrastructure.Data.Repositories;

public class ResidentComplaintRepository : RepositoryBase<ResidentComplaint>, IResidentComplaintRepository
{
    public ResidentComplaintRepository(DataContext dataContext) : base(dataContext)
    {
    }
}