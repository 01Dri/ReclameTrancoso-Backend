using Domain.Models;
using Domain.Models.DTOs.Complaint;
using Domain.Models.Pagination;

namespace Domain.Interfaces;

public interface IResidentComplaintRepository : IRepositoryBase<ResidentComplaint>
{
    Task<PagedResponseDto<ComplaintDto>> GetComplaintsByIdAsync(GetRequestPaginatedById requestPaginatedById);
    Task<PagedResponseDto<ComplaintDto>> GetComplaintsAsync(GetRequestPaginated requestPaginatedById);
    Task<bool> ExistsByResidentIdAsync(long? id);
}