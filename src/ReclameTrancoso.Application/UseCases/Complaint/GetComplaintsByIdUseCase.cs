using Domain.Interfaces;
using Domain.Models;
using Domain.Models.DTOs.Complaint;
using Domain.Models.Pagination;
using ReclameTrancoso.Exceptions.Exceptions;

namespace Application.UseCases.Complaint;

public class GetComplaintsByIdUseCase : IUseCaseHandler<GetRequestPaginated, PagedResponseDto<ComplaintDto>>
{
    private readonly IResidentComplaintRepository _residentComplaintRepository;

    public GetComplaintsByIdUseCase(IResidentComplaintRepository residentComplaintRepository)
    {
        _residentComplaintRepository = residentComplaintRepository;
    }

    public async Task<PagedResponseDto<ComplaintDto>> Handle(GetRequestPaginated request, CancellationToken cancellationToken)
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
            GetComplaintsById(request);
    }
}