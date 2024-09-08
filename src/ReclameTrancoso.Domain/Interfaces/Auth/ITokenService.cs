using Domain.Models.DTOs.Auth;

namespace ReclameTrancoso.Domain.Interfaces.Auth;

public interface ITokenService<T, TK>
{
    TK GenerateToken(T entity);
    

}