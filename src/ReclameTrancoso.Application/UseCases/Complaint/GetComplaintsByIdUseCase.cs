using Domain.Interfaces;
using Domain.Models;
using Domain.Models.DTOs.Complaint;
using Domain.Models.Pagination;

namespace Application.UseCases.Complaint;

public class GetComplaintsByIdUseCase : IUseCaseHandler<GetByIdRequestPaginated, PagedResponseOffsetDto<ComplaintDto>>
{
    private readonly IResidentComplaintRepository _residentComplaintRepository;

    public GetComplaintsByIdUseCase(IResidentComplaintRepository residentComplaintRepository)
    {
        _residentComplaintRepository = residentComplaintRepository;
    }

    public async Task<PagedResponseOffsetDto<ComplaintDto>> Handle(GetByIdRequestPaginated? request, CancellationToken cancellationToken)
    {
        return await this._residentComplaintRepository.GetComplaintsById(request);
    }
}