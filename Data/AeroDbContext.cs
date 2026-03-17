using Microsoft.EntityFrameworkCore;
using aeroWebApi.Entity;    

namespace aeroWebApi.Data
{
    public class AeroDbContext : DbContext
    {
        public DbSet<Passenger> Passengers { get; set; }
        public AeroDbContext(DbContextOptions<AeroDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Passenger>()
                .HasIndex(p => p.Email)
                .IsUnique();
        }

    }
}