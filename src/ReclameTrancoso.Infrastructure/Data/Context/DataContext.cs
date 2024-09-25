using Domain.Models;
using Domain.Models.DTOs.Union;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using ReclameTrancoso.Domain.Models;

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
    public DbSet<Complaint> Complaints { get; set; }
    public DbSet<ResidentComplaint> ResidentComplaints { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Manager> Managers { get; set; }
    public DbSet<ManagerComplaintComments> ManagerComplaintComments { get; set; }

    
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ConfigurePrimaryKey();
        modelBuilder.ConfigureRelationship();

    }
    
}