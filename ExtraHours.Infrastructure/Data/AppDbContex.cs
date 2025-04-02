using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ExtraHours.Core.Models;
using Microsoft.EntityFrameworkCore;
using ExtraHours.Core.Models;

namespace ExtraHours.Infrastructure.Data
{
    public class AppDbContex : DbContext
    {
        public AppDbContex(DbContextOptions<AppDbContex> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }

                public DbSet<Area> Areas { get; set; } // Add this line

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User-Area relationship
            modelBuilder.Entity<User>()
                .HasOne(u => u.Area)
                .WithMany(a => a.Users)
                .HasForeignKey(u => u.AreaId);
        }

        public DbSet<Report> Reports { get; set; }
        public DbSet<Settings> Settings { get; set; }
    }
    
}
