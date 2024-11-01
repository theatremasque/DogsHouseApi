using DogsHouse.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DogsHouse.API.Infrastructure;

public class PetsDbContext : DbContext
{
    public DbSet<Dog> Dogs { get; set; }
    
    public PetsDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dog>()
            .ToTable(d => d.HasCheckConstraint("\"CK_TailLengthAndWeight\"", "\"TailLength\" > 0 AND \"Weight\" > 0"));
        
        base.OnModelCreating(modelBuilder);
    }
}