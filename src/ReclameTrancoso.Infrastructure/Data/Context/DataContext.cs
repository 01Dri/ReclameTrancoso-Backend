using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Context;

public class DataContext : DbContext
{
    public DbSet<Apartment> Apartments { get; set; }
    public DbSet<Building> Buildings { get; set; }
    public DbSet<Resident> Residents { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<ApartmentResident> ApartmentResidents { get; set; }
    public DbSet<BuildingResident> BuildingResidents { get; set; }
    public DbSet<TokenEntity> TokenEntities { get; set; }

    
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Apartment>()
            .HasKey(a => a.Id);
        
        modelBuilder.Entity<Building>()
            .HasKey(a => a.Id);
        
        modelBuilder.Entity<Resident>()
            .HasKey(a => a.Id);
        
        modelBuilder.Entity<User>()
            .HasKey(a => a.Id);
        
        
        modelBuilder.Entity<ApartmentResident>()
            .HasKey(a => a.Id);
        
        modelBuilder.Entity<BuildingResident>()
            .HasKey(a => a.Id);

        modelBuilder.Entity<TokenEntity>()
            .HasKey(t => t.Id);
        
        
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
    }
    
}