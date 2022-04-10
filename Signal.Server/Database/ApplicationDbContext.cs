using Microsoft.EntityFrameworkCore;
using Signal.Server.Entities;

namespace Signal.Server.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<AuthorizedSensor> AuthorizedSensors { get; set; }
    public DbSet<SensorStatusTrack> SensorStatusTracks { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<AuthorizedSensor>()
            .HasIndex(a => a.MAC)
            .IsUnique();
    }
}