using Microsoft.EntityFrameworkCore;

namespace DogsHouse.API.Infrastructure;

public class DogDbContext : DbContext
{
    public DogDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}