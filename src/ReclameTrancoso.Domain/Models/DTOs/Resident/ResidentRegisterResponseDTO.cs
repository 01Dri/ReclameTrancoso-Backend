using Domain.Interfaces;

namespace Domain.Models.DTOs.Resident;

public class ResidentRegisterResponseDTO : IUseCaseResponse
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;

}