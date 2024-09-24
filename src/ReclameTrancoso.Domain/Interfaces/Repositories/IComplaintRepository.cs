using Domain.Models;
using Domain.Models.DTOs.Complaint;
using Domain.Models.Pagination;

namespace Domain.Interfaces;

public interface IComplaintRepository : IRepositoryBase<Complaint>
{
    Task<PagedResponseDto<ComplaintDto>> GetComplaintsAsync(GetRequestPaginated requestPaginated);
}