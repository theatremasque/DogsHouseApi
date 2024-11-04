using DogsHouse.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DogsHouse.API.Infrastructure.EntityConfigurations;

public class DogConfiguration : IEntityTypeConfiguration<Dog>
{
    public void Configure(EntityTypeBuilder<Dog> builder)
    {
        builder.ToTable(e => e.HasCheckConstraint("\"CK_TailLength\"", "\"TailLength\" > 0"));
        
        builder.ToTable(e => e.HasCheckConstraint("\"CK_Weight\"", "\"Weight\" > 0"));
    }
}