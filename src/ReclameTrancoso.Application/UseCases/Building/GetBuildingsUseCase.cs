using Application.Mappers;
using Domain.Interfaces;
using Domain.Models.DTOs;
using Domain.Models.DTOs.Building;

namespace Application.UseCases.Building;

public class GetBuildingsUseCase : IUseCaseHandler<GetRequest, BuildingResponseDTO>
{
    private readonly IBuildingRepository _buildingRepository;

    public GetBuildingsUseCase(IBuildingRepository buildingRepository)
    {
        _buildingRepository = buildingRepository;
    }

    public async Task<BuildingResponseDTO> Handle(GetRequest? request, CancellationToken cancellationToken)
    {
        var buildings = await this._buildingRepository.GetAsync();
        return new BuildingResponseDTO(buildings.Select(x => x.ToDto()));
    }
}