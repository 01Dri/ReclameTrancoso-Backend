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
}