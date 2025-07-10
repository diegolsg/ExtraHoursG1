using Microsoft.EntityFrameworkCore;
using ExtraHours.Core.Models;

namespace ExtraHours.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<ExtraHour> ExtraHours { get; set; }
        public DbSet<ExtraHourType> ExtraHourTypes { get; set; }
        public DbSet<RegistroHora> RegistroHoras { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExtraHourType>()
                                 .Property(e => e.Id)
                                 .ValueGeneratedNever();

            modelBuilder.Entity<ExtraHourType>().HasData(

                new ExtraHourType
                {
                    Id = 1,
                    TypeHourName = "Diurna",
                    Porcentaje = "25%",
                    StartExtraHour = new TimeSpan(6, 0, 0),
                    EndExtraHour = new TimeSpan(21, 0, 0),
                    Created = new DateTime(2025, 6, 15, 0, 0, 0, DateTimeKind.Utc),
                    Updated = new DateTime(2025, 6, 15, 0, 0, 0, DateTimeKind.Utc)

                },
                new ExtraHourType
                {
                    Id = 2,
                    TypeHourName = "Nocturna",
                    Porcentaje = "75%",
                    StartExtraHour = new TimeSpan(21, 0, 0),
                    EndExtraHour = new TimeSpan(6, 0, 0),
                    Created = new DateTime(2025, 6, 15, 0, 0, 0, DateTimeKind.Utc),
                    Updated = new DateTime(2025, 6, 15, 0, 0, 0, DateTimeKind.Utc)

                },
                new ExtraHourType
                {
                    Id = 3,
                    TypeHourName = "Dominical/Festiva Diurna",
                    Porcentaje = "100%",
                    StartExtraHour = new TimeSpan(6, 0, 0),
                    EndExtraHour = new TimeSpan(21, 0, 0),
                    Created = new DateTime(2025, 6, 15, 0, 0, 0, DateTimeKind.Utc),
                    Updated = new DateTime(2025, 6, 15, 0, 0, 0, DateTimeKind.Utc)

                },
                new ExtraHourType
                {
                    Id = 4,
                    TypeHourName = "Dominical/Festiva Nocturna",
                    Porcentaje = "150%",
                    StartExtraHour = new TimeSpan(21, 0, 0),
                    EndExtraHour = new TimeSpan(6, 0, 0),
                    Created = new DateTime(2025, 6, 15, 0, 0, 0, DateTimeKind.Utc),
                    Updated = new DateTime(2025, 6, 15, 0, 0, 0, DateTimeKind.Utc)

                }
            );


            modelBuilder.Entity<Setting>()
                                 .Property(e => e.Id)
                                 .ValueGeneratedNever();

            modelBuilder.Entity<Setting>().HasData(
                new Setting
                {
                    Id = 1,
                    LimitExtraHoursDay = 2,
                    LimitExtraHoursWeek = 12,
                    TotalHoursWeek = 46,
                    Created = new DateTime(2025, 6, 15, 0, 0, 0, DateTimeKind.Utc),
                    Updated = new DateTime(2025, 6, 15, 0, 0, 0, DateTimeKind.Utc)

                }
            );

            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    Name = "Administrador",
                },
                new Role
                {
                    Id = 2,
                    Name = "Empleado",
                }
            );
        }
    }
}