using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
        {

        }

        public DbSet<VaccinationCenter> VaccinationCenters { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
         public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

         builder.Entity<Reservation>()
                   .HasOne(r => r.AppUser)
                   .WithMany(u => u.Reservations)
                   .HasForeignKey(r => r.AppUserId)
                   .OnDelete(DeleteBehavior.Restrict); // Remove cascading delete

  builder.Entity<Reservation>()
                .HasOne(u => u.Vaccine)
                .WithMany(u => u.Reservations)
                .HasForeignKey(p => p.VaccineId)
                  .OnDelete(DeleteBehavior.Restrict);

 builder.Entity<Reservation>()
                .HasOne(u => u.VaccinationCenter)
                .WithMany(u => u.Reservations)
                .HasForeignKey(p => p.VaccinationCenterId)
                  .OnDelete(DeleteBehavior.Restrict);

    
        // Configure the relationship between Reservation and 
      
  
            builder.Entity<VaccinationCenter>()
                .HasOne(vc => vc.AppUser)
                .WithOne(au => au.VaccinationCenter)
                .HasForeignKey<VaccinationCenter>(vc => vc.ManagerId);
           
           
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "VaccinationCenter",
                    NormalizedName = "VaccinationCenter"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}