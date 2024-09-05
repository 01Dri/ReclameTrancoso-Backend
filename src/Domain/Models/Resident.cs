namespace Domain.Models;

public class Resident : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public virtual IEnumerable<BuildingResident> BuildingResidents { get; set; }
    public virtual IEnumerable<ApartmentResident> ApartmentResidents { get; set; }
    public string Cpf { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
}
