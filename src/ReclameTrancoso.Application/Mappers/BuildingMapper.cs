using Domain.Models;
using Domain.Models.DTOs.Building;

namespace Application.Mappers;

public static class BuildingMapper
{

    public static BuildingDTO ToDto(this Building building)
    {
        return new BuildingDTO(building.Id, building.Number);
    }
}