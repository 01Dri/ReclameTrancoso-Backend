using Domain.Models.DTOs.Union;

namespace Domain.Interfaces;

public interface IManagerRepository : IRepositoryBase<Manager>
{

    Task<long?> ExistByUserIdAsync(long? userId);

}