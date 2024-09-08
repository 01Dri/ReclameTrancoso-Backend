using Domain.Interfaces;

namespace Domain.Models.DTOs.Resident;

public class ResidentRegisterResponseDTO : IUseCaseResponse
{
    public long? Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public IEnumerable<long>  BuildingsIds { get; set; }
    public IEnumerable<long> ApartmentsIds { get; set; }
    
    public long? UserId { get; set; }
}