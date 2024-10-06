using Domain.Interfaces;
using Domain.Models;
using Domain.Models.DTOs.Complaint;
using Domain.Models.Pagination;

namespace Application.UseCases.Complaint;

public class ComplaintGetUseCase : IUseCaseHandler<GetRequestPaginated, PagedResponseDto<ComplaintDto>>
{
    private readonly IResidentComplaintRepository _residentComplaintRepository;

    public ComplaintGetUseCase(IResidentComplaintRepository residentComplaintRepository)
    {
        _residentComplaintRepository = residentComplaintRepository;
    }
    public async Task<PagedResponseDto<ComplaintDto>> Handle(GetRequestPaginated request, CancellationToken cancellationToken)
    {
        var result = await _residentComplaintRepository.GetComplaintsAsync(request);
        if (result.Data.Count == 0)
        {
            return new PagedResponseDto<ComplaintDto>()
            {
                Data = [],
                HasNext = false,
                HasPrevious = false,
                PageNumber = 0,
                PageSize = 0,
                TotalPages = 0,
                TotalRecords = 0
            };
        }

        return result;
    }
}