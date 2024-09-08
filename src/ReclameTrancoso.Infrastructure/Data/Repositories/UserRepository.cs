using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data.Context;

namespace Infrastructure.Data.Repositories;

public class UserRepository: RepositoryBase<User>, IUserRepository
{
    public UserRepository(DataContext context) : base(context)
    {
    }
}