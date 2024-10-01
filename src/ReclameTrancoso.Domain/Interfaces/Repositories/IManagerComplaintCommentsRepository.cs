using ReclameTrancoso.Domain.Models;

namespace Domain.Interfaces;

public interface IManagerComplaintCommentsRepository : IRepositoryBase<ManagerComplaintComments>
{
    Task<ManagerComplaintComments?> GetByCommentIdAsync(long? entityId);
    Task DeleteByComplaintIdAsync(long complaintId);
}