using Domain.Interfaces;
using MediatR;

namespace Domain.Models.DTOs.Resident;

public class ResidentRegisterRequestDTO : IUseCaseRequest
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public long BuildingId { get; set; }
    public long ApartmentId { get; set; }

}