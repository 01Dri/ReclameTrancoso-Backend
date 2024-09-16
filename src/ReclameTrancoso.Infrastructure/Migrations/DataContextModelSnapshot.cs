﻿// <auto-generated />
using System;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-preview.7.24405.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Models.Apartment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("BuildingId")
                        .HasColumnType("bigint");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BuildingId");

                    b.ToTable("Apartments");
                });

            modelBuilder.Entity("Domain.Models.ApartmentResident", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("ApartmentId")
                        .HasColumnType("bigint");

                    b.Property<long>("ResidentId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ApartmentId");

                    b.HasIndex("ResidentId");

                    b.ToTable("ApartmentResidents");
                });

            modelBuilder.Entity("Domain.Models.Building", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Buildings");
                });

            modelBuilder.Entity("Domain.Models.BuildingResident", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("BuildingId")
                        .HasColumnType("bigint");

                    b.Property<long>("ResidentId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("BuildingId");

                    b.HasIndex("ResidentId");

                    b.ToTable("BuildingResidents");
                });

            modelBuilder.Entity("Domain.Models.Complaint", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("AdditionalInformation1")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("AdditionalInformation2")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("AdditionalInformation3")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ComplaintType")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Complaints");
                });

            modelBuilder.Entity("Domain.Models.Resident", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Residents");
                });

            modelBuilder.Entity("Domain.Models.ResidentComplaint", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("ComplaintId")
                        .HasColumnType("bigint");

                    b.Property<long>("ResidentId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ComplaintId");

                    b.HasIndex("ResidentId");

                    b.ToTable("ResidentComplaints");
                });

            modelBuilder.Entity("Domain.Models.TokenEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("AccessToken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("AccessTokenExpiresAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("RefreshTokenExpiresAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("TokenEntities");
                });

            modelBuilder.Entity("Domain.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("ResidentId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ResidentId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Models.Apartment", b =>
                {
                    b.HasOne("Domain.Models.Building", "Building")
                        .WithMany("Apartments")
                        .HasForeignKey("BuildingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Building");
                });

            modelBuilder.Entity("Domain.Models.ApartmentResident", b =>
                {
                    b.HasOne("Domain.Models.Apartment", "Apartment")
                        .WithMany("ApartmentResidents")
                        .HasForeignKey("ApartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Resident", "Resident")
                        .WithMany("ApartmentResidents")
                        .HasForeignKey("ResidentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Apartment");

                    b.Navigation("Resident");
                });

            modelBuilder.Entity("Domain.Models.BuildingResident", b =>
                {
                    b.HasOne("Domain.Models.Building", "Building")
                        .WithMany("BuildingResidents")
                        .HasForeignKey("BuildingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Resident", "Resident")
                        .WithMany("BuildingResidents")
                        .HasForeignKey("ResidentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Building");

                    b.Navigation("Resident");
                });

            modelBuilder.Entity("Domain.Models.ResidentComplaint", b =>
                {
                    b.HasOne("Domain.Models.Complaint", "Complaint")
                        .WithMany("ResidentComplaints")
                        .HasForeignKey("ComplaintId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Resident", "Resident")
                        .WithMany("Complaints")
                        .HasForeignKey("ResidentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Complaint");

                    b.Navigation("Resident");
                });

            modelBuilder.Entity("Domain.Models.TokenEntity", b =>
                {
                    b.HasOne("Domain.Models.User", "User")
                        .WithOne("Token")
                        .HasForeignKey("Domain.Models.TokenEntity", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Models.User", b =>
                {
                    b.HasOne("Domain.Models.Resident", "Resident")
                        .WithOne("User")
                        .HasForeignKey("Domain.Models.User", "ResidentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Resident");
                });

            modelBuilder.Entity("Domain.Models.Apartment", b =>
                {
                    b.Navigation("ApartmentResidents");
                });

            modelBuilder.Entity("Domain.Models.Building", b =>
                {
                    b.Navigation("Apartments");

                    b.Navigation("BuildingResidents");
                });

            modelBuilder.Entity("Domain.Models.Complaint", b =>
                {
                    b.Navigation("ResidentComplaints");
                });

            modelBuilder.Entity("Domain.Models.Resident", b =>
                {
                    b.Navigation("ApartmentResidents");

                    b.Navigation("BuildingResidents");

                    b.Navigation("Complaints");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Models.User", b =>
                {
                    b.Navigation("Token");
                });
#pragma warning restore 612, 618
        }
    }
}
