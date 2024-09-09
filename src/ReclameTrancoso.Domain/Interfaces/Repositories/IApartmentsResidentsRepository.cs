using Domain.Models;

namespace Domain.Interfaces;

public interface IApartmentsResidentsRepository : IRepositoryBase<ApartmentResident>
{
    Task<bool> AlreadyExistOwnerApartmentAsync(long? apartmentId);
}
