using Domain.Models;
using Domain.Models.DTOs.Union;
using Microsoft.EntityFrameworkCore;
using ReclameTrancoso.Domain.Models;

namespace Infrastructure.Extensions;

public static class EntityPrimaryKeyMappingExtension
{
    public static ModelBuilder ConfigurePrimaryKey(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Apartment>()
            .HasKey(a => a.Id);
        
        modelBuilder.Entity<Building>()
            .HasKey(a => a.Id);
        
        modelBuilder.Entity<Resident>()
            .HasKey(a => a.Id);
        
        modelBuilder.Entity<User>()
            .HasKey(a => a.Id);
        
        modelBuilder.Entity<Complaint>()
            .HasKey(a => a.Id);
        
        modelBuilder.Entity<ResidentComplaint>()
            .HasKey(a => a.Id);

        modelBuilder.Entity<ApartmentResident>()
            .HasKey(a => a.Id);
        
        modelBuilder.Entity<BuildingResident>()
            .HasKey(a => a.Id);

        modelBuilder.Entity<TokenEntity>()
            .HasKey(t => t.Id);
        
        modelBuilder.Entity<Manager>()
            .HasKey(t => t.Id);
        
        modelBuilder.Entity<Comment>()
            .HasKey(t => t.Id);
        
        modelBuilder.Entity<ManagerComplaintComments>()
            .HasKey(t => t.Id);
        
        
        return modelBuilder;

    }
    
}