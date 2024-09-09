using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data.Context;

namespace Infrastructure.Data.Repositories;

public class TokenRepository : RepositoryBase<TokenEntity>, ITokenRepository
{
    public TokenRepository(DataContext dataContext) : base(dataContext)
    {
    }
}