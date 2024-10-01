using Domain.Interfaces;

namespace Domain.Models.DTOs.Resident;

public record ResidentResponseDTO(
    long? Id,
    string Name,
    string Email,
    string Cpf,
    IEnumerable<long>  BuildingsIds,
    IEnumerable<long> ApartmentsIds,
    long? UserId
    ) :  IUseCaseResponse;