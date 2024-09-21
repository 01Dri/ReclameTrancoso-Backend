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

    public async Task<PagedResponseDto<ComplaintDto>> Handle(GetRequestPaginated? request, CancellationToken cancellationToken)
    {
        if (!await _residentComplaintRepository.ExistsByResidentIdAsync(request.Id))
        {
            throw new NotFoundException("Residente não possui tickets cadastrados.");
        }
        return await this._residentComplaintRepository.
            GetComplaintsById(request);
    }
}