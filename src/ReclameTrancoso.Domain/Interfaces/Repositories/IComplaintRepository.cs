using Domain.Models;
using Domain.Models.DTOs.Complaint;
using Domain.Models.Pagination;

namespace Domain.Interfaces;

public interface IComplaintRepository : IRepositoryBase<Complaint>
{
    Task<PagedResponseOffsetDto<ComplaintDto>> GetComplaintsAsync(GetByIdRequestPaginated requestPaginated);
}