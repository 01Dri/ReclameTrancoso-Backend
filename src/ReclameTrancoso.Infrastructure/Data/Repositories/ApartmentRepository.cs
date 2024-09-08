using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data.Context;

namespace Infrastructure.Data.Repositories;

public class ApartmentRepository: RepositoryBase<Apartment>, IApartmentRepository
{
    public ApartmentRepository(DataContext context) : base(context)
    {
    }
}