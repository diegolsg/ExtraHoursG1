using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace ExtraHours.Infrastructure.Data
{


    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql("Host=dpg-d1k715ur433s73c6ukt0-a.oregon-postgres.render.com;Port=5432;Database=extra_hours;Username=extra_hours_user;Password=0OgKeV5ZcZNnEdZoGABy4rnfk1ESCmRF");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}