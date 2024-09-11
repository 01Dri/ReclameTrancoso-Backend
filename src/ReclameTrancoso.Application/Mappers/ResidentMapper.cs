using Domain.Models;
using Domain.Models.DTOs.Resident;

namespace Application.Mappers;

public static class ResidentMapper
{
    public static Resident ToEntity(this ResidentRegisterRequestDTO dto)
    {
        return new()
        {
            Name = dto.Name,
            Email = dto.Email,
            Cpf = dto.Cpf,
            User = new User()
            {
                Cpf = dto.Cpf,
                Password = dto.Password
            }
        };
    }
    
    public static ResidentRegisterResponseDTO ToRegisterUseCaseResult(this Resident entity)
    {
        return new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Email = entity.Email,
            CPF = entity.Cpf,
            ApartmentsIds = entity.ApartmentResidents?.Select(x => x.ApartmentId).ToList()?? [],
            BuildingsIds = entity.BuildingResidents?.Select(x => x.BuildingId).ToList() ?? [],
            UserId = entity.User?.Id
        };
    }
    
    public static ResidentResponseDTO ToResidentResponseDto(this Resident entity)
    {
        return new
        (
            entity.Id,
            entity.Name,
            entity.Email,
            entity.BuildingResidents?.Select(x => x.BuildingId).ToList() ?? [],
            entity.ApartmentResidents?.Select(x => x.ApartmentId).ToList() ?? [],
            entity.User?.Id
        );
    }
}