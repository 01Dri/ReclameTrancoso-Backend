using Domain.Models;

namespace Domain.Interfaces;

public interface ITokenRepository : IRepositoryBase<TokenEntity>
{
    Task<TokenEntity?> GetByRefreshTokenAsync(string refreshToken);

}