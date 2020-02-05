using FlightBooking.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightBooking.Entities.Context
{
    public partial class FlightDBContext : DbContext
    {
        public FlightDBContext() { }
        public FlightDBContext(DbContextOptions<FlightDBContext> options)
           : base(options)
        {
        }

        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<Plane> Planes { get; set; }
        public virtual DbSet<Airport> Airports { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flight>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("Flight");

                entity.HasOne(d => d.Plane)
                    .WithMany(p => p.Flights)
                    .HasForeignKey(d => d.PlaneId)
                    .HasConstraintName("FK_Flight_Plane");

                entity.HasOne(d => d.FlightFrom)
                   .WithMany(p => p.FlightFroms)
                   .HasForeignKey(d => d.FlightFromId)
                   .HasConstraintName("FK_FlightFrom_Airport");

                entity.HasOne(d => d.FlightTo)
                  .WithMany(p => p.FlightTos)
                  .HasForeignKey(d => d.FlightToId)
                  .HasConstraintName("FK_FlightTo_Airport");
            });

            modelBuilder.Entity<Plane>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("Plane");
            });

            modelBuilder.Entity<Airport>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("Airport");
            });

            modelBuilder.Entity<Plane>().HasData(
              new Plane(){  Name = "Wright Flyer", CreationDate = DateTime.Now, ComsumptionEffort = 123, ComsumptionRate=30, Speed=200 },
              new Plane(){  Name = "Supermarine Spitfire", CreationDate = DateTime.Now, ComsumptionEffort = 340, ComsumptionRate=50, Speed = 600 },
              new Plane(){  Name = "Boeing 787", CreationDate = DateTime.Now, ComsumptionEffort = 400, ComsumptionRate=100, Speed = 1000 },
              new Plane(){  Name = "Learjet 23", CreationDate = DateTime.Now, ComsumptionEffort = 300, ComsumptionRate=145, Speed = 450 },
              new Plane(){  Name = "Lockheed C-130", CreationDate = DateTime.Now, ComsumptionEffort = 140, ComsumptionRate=80, Speed = 500 });

            modelBuilder.Entity<Airport>().HasData(
             new Airport() { Name = "Hartsfield–Jackson Atlanta International Airport", CreationDate = DateTime.Now, City = "Atlanta, Georgia", Country = "United States", Latitude = -29.83245, Longitude = 31.04034 },
             new Airport() { Name = "Paris-Charles de Gaulle Airport", CreationDate = DateTime.Now, City = "Roissy-en-France, Île-de-Franc", Country = "France", Latitude = -0.83245, Longitude = 31.04034 },
             new Airport() { Name = "Tokyo Haneda Airport", CreationDate = DateTime.Now, City = "Ōta, Tokyo", Country = "Japan", Latitude = -51.39792, Longitude = -0.12084 },
             new Airport() { Name = " Dubai International Airport", CreationDate = DateTime.Now, City = "Garhoud, Dubai", Country = "United Arab Emirates", Latitude = 77.2167, Longitude = 28.6667 },
             new Airport() { Name = "Mohammed V Airport", CreationDate = DateTime.Now, City = "Casablanca", Country = "Morocco", Latitude = -34.83245, Longitude = 28.6667 },
             new Airport() { Name = "Toronto Pearson International Airport", CreationDate = DateTime.Now, City = "Mississauga, Ontario", Country = "Canada", Latitude = 77.0333, Longitude = 77.0333 },
             new Airport() { Name = "Barcelona–El Prat Airport", CreationDate = DateTime.Now, City = "Barcelona", Country = "Spain", Latitude = -28.4667, Longitude = -0.83245, });
        }

    }
}
