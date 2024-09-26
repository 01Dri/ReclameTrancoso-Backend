using Domain.Models;
using Domain.Models.DTOs.Union;
using Microsoft.EntityFrameworkCore;
using ReclameTrancoso.Domain.Models;

namespace Infrastructure.Extensions;

public static class EntityRelationshipMappingExtension
{
    public static ModelBuilder ConfigureRelationship(this ModelBuilder modelBuilder)
    {
          modelBuilder.Entity<Apartment>()
            .HasOne<Building>(a => a.Building)
            .WithMany(b => b.Apartments)
            .HasForeignKey(a => a.BuildingId);

          modelBuilder.Entity<Complaint>().HasOne<Resident>(c => c.Resident)
              .WithMany().HasForeignKey(r => r.ResidentId);


        modelBuilder.Entity<ApartmentResident>()
            .HasOne<Resident>(ap => ap.Resident)
            .WithMany(r => r.ApartmentResidents)
            .HasForeignKey(ar => ar.ResidentId);
        
        modelBuilder.Entity<ApartmentResident>()
            .HasOne<Apartment>(ap => ap.Apartment)
            .WithMany(r => r.ApartmentResidents)
            .HasForeignKey(ar => ar.ApartmentId);
        
        
        modelBuilder.Entity<BuildingResident>()
            .HasOne<Resident>(ap => ap.Resident)
            .WithMany(r => r.BuildingResidents)
            .HasForeignKey(ar => ar.ResidentId);
        
        modelBuilder.Entity<BuildingResident>()
            .HasOne<Building>(ap => ap.Building)
            .WithMany(r => r.BuildingResidents)
            .HasForeignKey(ar => ar.BuildingId);
        

        modelBuilder.Entity<TokenEntity>()
            .HasOne(t => t.User)
            .WithOne(u => u.Token)
            .HasForeignKey<TokenEntity>(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ResidentComplaint>()
            .HasOne<Resident>(rc => rc.Resident)
            .WithMany(rc => rc.Complaints)
            .HasForeignKey(rc => rc.ResidentId);
            ;

        modelBuilder.Entity<ManagerComplaintComments>()
            .HasOne(uc => uc.Manager)
            .WithMany(uc => uc.Comments)
            .HasForeignKey(uc => uc.ManagerId);

        modelBuilder.Entity<ManagerComplaintComments>()
            .HasOne(uc => uc.Complaint)
            .WithOne(uc => uc.Comment).HasForeignKey<ManagerComplaintComments>
                (uc => uc.ComplaintId);

        modelBuilder.Entity<ManagerComplaintComments>()
            .HasOne(uc => uc.Comment)
            .WithMany()
            .HasForeignKey(uc => uc.CommentId);
        
        modelBuilder.Entity<ManagerComplaintComments>()
            .HasIndex(uc => uc.ComplaintId)
            .IsUnique();

        modelBuilder.Entity<Manager>()
            .HasOne<User>(m => m.User).WithOne()
            .HasForeignKey<Manager>(m => m.UserId);

        return modelBuilder;
    }
}