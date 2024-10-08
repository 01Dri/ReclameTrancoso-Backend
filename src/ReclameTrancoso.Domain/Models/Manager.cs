using ReclameTrancoso.Domain.Models;

namespace Domain.Models.DTOs.Union;

public class Manager : BaseEntity, IBaseDocuments
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; }= string.Empty;
    public string Cpf { get; set; }= string.Empty;
    public long? UserId { get; set; }
    public virtual Models.User User { get; set; }
    public virtual IEnumerable<ManagerComplaintComments> Comments { get; set; }
    
}