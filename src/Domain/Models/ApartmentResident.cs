namespace Domain.Models;

public class ApartmentResident : BaseEntity
{
    public long ApartmentId { get; set; }
    public Apartment Apartment { get; set; }
    
    public long ResidentId { get; set; }
    public Resident Resident { get; set; }
    
    
}