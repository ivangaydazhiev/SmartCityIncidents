using Microsoft.EntityFrameworkCore;
using SmartCity.Domain.Entities;

namespace SmartCity.Infrastructure.Persistence
{
    public class SmartCityDbContext : DbContext
    {
        public SmartCityDbContext(DbContextOptions<SmartCityDbContext> options)
            : base(options) 
        {
        }

        public DbSet<Incident> Incidents { get; set; } = null!;
        public DbSet<Location> Locations { get; set; } = null!;
        public DbSet<ResponseTeam> ResponseTeams { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Incident>()
                .HasOne(i => i.Location)
                .WithMany()
                .HasForeignKey(i => i.LocationId);

            modelBuilder.Entity<Location>().HasData(
                new Location
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    City = "Sofia",
                    Address = "Main Street 1",
                    Latitude = 42.6977,
                    Longitude = 23.3219

                }
            );
        }
    }
}
