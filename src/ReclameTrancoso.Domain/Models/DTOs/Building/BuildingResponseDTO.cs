using Domain.Interfaces;

namespace Domain.Models.DTOs.Building;

public class BuildingResponseDTO : IUseCaseResponse
{
    public IEnumerable<BuildingDTO> Buildings { get; set; }

    public BuildingResponseDTO(IEnumerable<BuildingDTO> buildings)
    {
        Buildings = buildings;
    }
}