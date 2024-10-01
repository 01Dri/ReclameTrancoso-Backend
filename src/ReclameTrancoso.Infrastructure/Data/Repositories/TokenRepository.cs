using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class TokenRepository : RepositoryBase<TokenEntity>, ITokenRepository
{
    public TokenRepository(DataContext dataContext) : base(dataContext)
    {
    }

    public async Task<TokenEntity?> GetByRefreshTokenAsync(string refreshToken)
    {
        return await this.DbSet
            .Include(x => x.User)
            .Where(x => x.RefreshToken == refreshToken)
            .FirstOrDefaultAsync(); 
    }
}