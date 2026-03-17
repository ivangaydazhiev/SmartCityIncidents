
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
        }


    }
}
