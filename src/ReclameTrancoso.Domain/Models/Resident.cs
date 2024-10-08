﻿namespace Domain.Models;

public class Resident : BaseEntity, IBaseDocuments
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public virtual IEnumerable<BuildingResident>? BuildingResidents { get; set; }
    public virtual IEnumerable<ApartmentResident>? ApartmentResidents { get; set; }
    public string Cpf { get; set; } = string.Empty;
    
    public long? UserId { get; set; }
    public User? User { get; set; }
    
    public virtual IEnumerable<ResidentComplaint> Complaints { get; set; }
}
