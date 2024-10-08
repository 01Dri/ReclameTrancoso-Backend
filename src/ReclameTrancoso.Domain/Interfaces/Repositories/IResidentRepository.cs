﻿using Domain.Models;

namespace Domain.Interfaces;

public interface IResidentRepository : IRepositoryBase<Resident>
{
    Task<bool> AnyByEmailAsync(string email);
    Task<bool> AnyByCPFAsync(string cpf);

    Task<Resident?> GetResidentByIdAsync(long id);
    
    Task<long?> ExistByUserIdAsync(long? userId);



}