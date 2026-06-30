using Dsw2026Ej15.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Doctor> Doctors => Set<Doctor>();
        public DbSet<Speciality> Specialities => Set<Speciality>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>(e =>
            {
                e.HasKey(d => d.Id);
                e.Property(d => d.Name).IsRequired();
                e.Property(d => d.LicenseNumber).IsRequired();
                e.HasOne(d => d.Speciality)
                 .WithMany()
                 .HasForeignKey("SpecialityId");
            });

            modelBuilder.Entity<Speciality>(e =>
            {
                e.HasKey(s => s.Id);
                e.Property(s => s.Name).IsRequired();
            });
        }
    }

}
