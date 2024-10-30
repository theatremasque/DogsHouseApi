using DogsHouse.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DogsHouse.API.Infrastructure;

public class PetsDbContext : DbContext
{
    public DbSet<Dog> Dogs { get; set; }
    
    public PetsDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}