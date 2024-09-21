using Domain.Interfaces;
using Domain.Models;
using Domain.Models.DTOs.Complaint;
using Domain.Models.Pagination;

namespace Application.UseCases.Complaint;

public class GetComplaintsByIdUseCase : IUseCaseHandler<GetRequestPaginated, PagedResponseDto<ComplaintDto>>
{
    private readonly IResidentComplaintRepository _residentComplaintRepository;

    public GetComplaintsByIdUseCase(IResidentComplaintRepository residentComplaintRepository)
    {
        _residentComplaintRepository = residentComplaintRepository;
    }

    public async Task<PagedResponseDto<ComplaintDto>> Handle(GetRequestPaginated? request, CancellationToken cancellationToken)
    {
        return await this._residentComplaintRepository.GetComplaintsById(request);
    }
}