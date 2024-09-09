namespace Domain.Models;

public class Apartment : BaseEntity
{
    public int Number { get; set; }
    public long BuildingId { get; set; }
    public Building Building { get; set; }
    public  virtual IEnumerable<ApartmentResident> ApartmentResidents { get; set; }
}