using Domain.Models;
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


        modelBuilder.Entity<User>()
            .HasOne<Resident>(u => u.Resident)
            .WithOne(r => r.User)
            .HasForeignKey<User>( r => r.ResidentId)
            .OnDelete(DeleteBehavior.Cascade);
        

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

        modelBuilder.Entity<UnionComplaintComments>()
            .HasOne(uc => uc.Union)
            .WithMany(uc => uc.Comments)
            .HasForeignKey(uc => uc.UnionId);

        modelBuilder.Entity<UnionComplaintComments>()
            .HasOne(uc => uc.Complaint)
            .WithMany(uc => uc.UnionComplaintComments)
            .HasForeignKey(uc => uc.ComplaintId);

        modelBuilder.Entity<UnionComplaintComments>()
            .HasOne(uc => uc.Comment)
            .WithMany()
            .HasForeignKey(uc => uc.CommentId);

        return modelBuilder;
    }
}