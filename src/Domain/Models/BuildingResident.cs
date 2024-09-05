namespace Domain.Models;

public class BuildingResident : BaseEntity
{
    public long BuildingId { get; set; }
    public  virtual Building Building { get; set; }
    
    public long ResidentId { get; set; }
    public virtual Resident Resident { get; set; }
}