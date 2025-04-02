using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public DbSet<Report> Reports { get; set; }
        public DbSet<Settings> Settings { get; set; }
    }
    
}

public class AppDbContext : DbContext
{
    public DbSet<Role> Roles { get; set; }  // para crear la tabla "Roles"

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // Configuración de la relación muchos-a-muchos
    modelBuilder.Entity<Role>()
        .HasMany(r => r.Permissions)
        .WithMany(p => p.Roles)
        .UsingEntity<Dictionary<string, object>>(
            "RolePermissions", // Nombre de la tabla intermedia
            j => j.HasOne<Permission>().WithMany().HasForeignKey("PermissionId"),
            j => j.HasOne<Role>().WithMany().HasForeignKey("RoleId")
        );

    // Opcional: Insertar datos iniciales directamente aquí
    modelBuilder.Entity<Permission>().HasData(
        new Permission { Id = 1, Code = "users.create", Description = "Crear usuarios" },
        new Permission { Id = 2, Code = "users.delete", Description = "Eliminar usuarios" }
    );
}

