using Domain.Interfaces;
using Domain.Models;
using Domain.Models.DTOs.Complaint;
using Domain.Models.Pagination;

namespace Application.UseCases.Complaint;

public class ComplaintsGetByResidentIdUseCase : IUseCaseHandler<GetRequestPaginatedById, PagedResponseDto<ComplaintDto>>
{
    private readonly IResidentComplaintRepository _residentComplaintRepository;

    public ComplaintsGetByResidentIdUseCase(IResidentComplaintRepository residentComplaintRepository)
    {
        _residentComplaintRepository = residentComplaintRepository;
    }

    public async Task<PagedResponseDto<ComplaintDto>> Handle(GetRequestPaginatedById request, CancellationToken cancellationToken)
    {
        if (!await _residentComplaintRepository.ExistsByResidentIdAsync(request.Id))
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
        return await this._residentComplaintRepository.
            GetComplaintsByIdAsync(request);
    }
}