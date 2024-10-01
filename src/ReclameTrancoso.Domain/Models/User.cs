using ReclameTrancoso.Domain.Enums;

namespace Domain.Models;

public class User : BaseEntity
{
    public string Email { get; set; }
    public string Cpf { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
    public virtual TokenEntity? Token { get; set; }
    

}