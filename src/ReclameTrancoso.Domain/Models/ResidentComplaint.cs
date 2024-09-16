namespace Domain.Models;

public class ResidentCompliant : BaseEntity
{
    public long ResidentId { get; set; }
    public virtual Resident Resident { get; set; }
    
    public long ComplaintId { get; set; }
    public virtual Complaint Complaint { get; set; }
    
    
}