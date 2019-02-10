﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using dTax.Models;

namespace dTax.Migrations
{
    [DbContext(typeof(DbPostrgreContext))]
    [Migration("20190209154028_DbPostrgreContext")]
    partial class DbPostrgreContext
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("dTax.Models.Cab", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<int>("CarModelId");

                    b.Property<int>("DriverId");

                    b.Property<string>("LicensePlate");

                    b.Property<int>("ManufactureYear");

                    b.HasKey("Id");

                    b.HasIndex("CarModelId");

                    b.HasIndex("DriverId");

                    b.ToTable("Cabs");
                });

            modelBuilder.Entity("dTax.Models.CabRide", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AddressEndPoint");

                    b.Property<string>("AddressStartPoint");

                    b.Property<bool>("Canceled");

                    b.Property<string>("EndPointGPS");

                    b.Property<int>("PaymentTypeId");

                    b.Property<decimal>("Price");

                    b.Property<DateTime>("RideEndTime");

                    b.Property<DateTime>("RideStartTime");

                    b.Property<int>("ShiftId");

                    b.Property<string>("StartPointGPS");

                    b.HasKey("Id");

                    b.HasIndex("PaymentTypeId");

                    b.HasIndex("ShiftId");

                    b.ToTable("CabRides");
                });

            modelBuilder.Entity("dTax.Models.CabRideStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CabRideId");

                    b.Property<int>("CustomerId");

                    b.Property<int>("ShiftId");

                    b.Property<string>("StatusDetails");

                    b.Property<int>("StatusId");

                    b.Property<DateTime>("StatusTime");

                    b.HasKey("Id");

                    b.HasIndex("CabRideId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ShiftId");

                    b.HasIndex("StatusId");

                    b.ToTable("CabRideStatuses");
                });

            modelBuilder.Entity("dTax.Models.CarModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BrandName");

                    b.Property<string>("ModelDescription");

                    b.Property<string>("ModelName");

                    b.HasKey("Id");

                    b.ToTable("CarModels");
                });

            modelBuilder.Entity("dTax.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("dTax.Models.Driver", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DrivingLicence");

                    b.Property<DateTime>("ExpiryDate");

                    b.Property<int>("UserId");

                    b.Property<bool>("Working");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Drivers");
                });

            modelBuilder.Entity("dTax.Models.PaymentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("TypeName");

                    b.HasKey("Id");

                    b.ToTable("PaymentTypes");
                });

            modelBuilder.Entity("dTax.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("dTax.Models.Shift", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CabId");

                    b.Property<int>("DriverId");

                    b.Property<DateTime>("LoginTime");

                    b.HasKey("Id");

                    b.HasIndex("CabId");

                    b.HasIndex("DriverId");

                    b.ToTable("Shifts");
                });

            modelBuilder.Entity("dTax.Models.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("StatusName");

                    b.HasKey("Id");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("dTax.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<bool>("FullReg");

                    b.Property<bool>("IsDriver");

                    b.Property<string>("LastName");

                    b.Property<string>("Login");

                    b.Property<string>("Password");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("dTax.Models.Cab", b =>
                {
                    b.HasOne("dTax.Models.CarModel", "CarModel")
                        .WithMany()
                        .HasForeignKey("CarModelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("dTax.Models.Driver", "Driver")
                        .WithMany()
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("dTax.Models.CabRide", b =>
                {
                    b.HasOne("dTax.Models.PaymentType", "PamentType")
                        .WithMany()
                        .HasForeignKey("PaymentTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("dTax.Models.Shift", "Shift")
                        .WithMany()
                        .HasForeignKey("ShiftId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("dTax.Models.CabRideStatus", b =>
                {
                    b.HasOne("dTax.Models.CabRide", "CabRide")
                        .WithMany()
                        .HasForeignKey("CabRideId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("dTax.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("dTax.Models.Shift", "Shift")
                        .WithMany()
                        .HasForeignKey("ShiftId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("dTax.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("dTax.Models.Customer", b =>
                {
                    b.HasOne("dTax.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("dTax.Models.Driver", b =>
                {
                    b.HasOne("dTax.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("dTax.Models.Shift", b =>
                {
                    b.HasOne("dTax.Models.Cab", "Cab")
                        .WithMany()
                        .HasForeignKey("CabId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("dTax.Models.Driver", "Driver")
                        .WithMany()
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("dTax.Models.User", b =>
                {
                    b.HasOne("dTax.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
