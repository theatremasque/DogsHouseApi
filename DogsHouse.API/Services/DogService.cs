using DogsHouse.API.Dtos;
using DogsHouse.API.Entities;
using DogsHouse.API.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DogsHouse.API.Services;

public class DogService : IDogService
{
    private readonly PetsDbContext _ctx;

    public DogService(PetsDbContext ctx)
    {
        _ctx = ctx;
    }

    public string Ping()
    {
        var message = "Dogshouseservice.Version1.0.1";

        return message;
    }

    public async Task AddAsync(DogAddDto? dog, CancellationToken cancellationToken)
    {
        if (dog != null)
        {
            var entity = new Dog()
            {
                Name = dog.Name,
                Color = dog.Color,
                TailLength = dog.TailLength,
                Weight = dog.Weight
            };

            _ctx.Dogs.Add(entity);

            await _ctx.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<IEnumerable<DogDto>> ListAsync(CancellationToken cancellationToken)
    {
        var dogs = await _ctx.Dogs
            .Select(d => new DogDto
            {
                Id = d.Id,
                Name = d.Name,
                Color = d.Color,
                TailLength = d.TailLength,
                Weight = d.Weight
            })
            .ToListAsync(cancellationToken);

        return dogs;
    }
}