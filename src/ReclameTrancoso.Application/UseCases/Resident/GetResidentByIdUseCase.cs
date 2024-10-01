using Application.Mappers;
using Domain.Interfaces;
using Domain.Models.DTOs;
using Domain.Models.DTOs.Resident;
using ReclameTrancoso.Exceptions.Exceptions;

namespace Application.UseCases.Resident;

public class GetResidentByIdUseCase : IUseCaseHandler<GetByIdRequest, ResidentResponseDTO>
{
    private readonly IResidentRepository _repository;

    public GetResidentByIdUseCase(IResidentRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResidentResponseDTO> Handle(GetByIdRequest? request, CancellationToken cancellationToken)
    {
        var resident = await this._repository.GetResidentByIdAsync(request.Id) ?? throw new NotFoundException("Resident não existe.");
        return resident.ToResidentResponseDto();
    }
}