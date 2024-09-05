namespace Domain.Models;

public class Building : BaseEntity
{
    public int Number { get; set; }
    public virtual IEnumerable<BuildingResident> BuildingResidents { get; set; }
    public virtual IEnumerable<Apartment> Apartments { get; set; }

}