using System.Linq.Expressions;
using DogsHouse.API.Dtos;
using DogsHouse.API.Entities;
using DogsHouse.API.FilterRequest;
using DogsHouse.API.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DogsHouse.API.Services;

public class DogService : IDogService
{
    private readonly PetsDbContext _ctx;
    
    private static readonly Dictionary<string, Expression<Func<Dog, object>>> SortExpressions = new()
    {
        {"id", d => d.Id},
        {"name", d => d.Name},
        {"color", d => d.Color},
        {"tailLength", d => d.TailLength},
        {"weight", d => d.Weight}
    };
    
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

    public async Task<IEnumerable<DogDto>> ListAsync(DogRequest request, CancellationToken cancellationToken)
    {
        var query = _ctx.Dogs.AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(request.Attribute) && !string.IsNullOrWhiteSpace(request.Order))
        {
            var orderValue = request.Order.ToLower();
            
            if (SortExpressions.TryGetValue(request.Attribute, out var expression))
            {
                query = orderValue switch
                {
                    "asc" => query.OrderBy(expression),
                    "desc" => query.OrderByDescending(expression),
                    _ => query
                };
            }
        }

        var data = query
            .Select(d => new DogDto
            {
                Id = d.Id,
                Name = d.Name,
                Color = d.Color,
                TailLength = d.TailLength,
                Weight = d.Weight
            })
            .ToListAsync(cancellationToken);
        
        return await data;
    }
}