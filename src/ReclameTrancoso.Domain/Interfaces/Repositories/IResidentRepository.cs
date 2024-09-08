using Domain.Models;

namespace Domain.Interfaces;

public interface IResidentRepository : IRepositoryBase<Resident>
{
    Task<bool> AnyByEmail(string email);
    Task<bool> AnyByCPF(string cpf);

}