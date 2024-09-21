using Domain.Models;
using Domain.Models.DTOs.Complaint;
using Domain.Models.Pagination;

namespace Domain.Interfaces;

public interface IResidentComplaintRepository : IRepositoryBase<ResidentComplaint>
{
    Task<PagedResponseOffsetDto<ComplaintDto>> GetComplaintsById(GetByIdRequestPaginated requestPaginated);

}