namespace Domain.Models;

public class User : BaseEntity
{
    public string Cpf { get; set; }
    public string Password { get; set; }
    public long? ResidentId { get; set; }
    public virtual Resident Resident { get; set; }
    
}