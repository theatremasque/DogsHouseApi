using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DogsHouse.API.Dtos;
using DogsHouse.API.Entities;
using DogsHouse.API.Infrastructure;
using DogsHouse.API.QueryExtensions;
using Microsoft.EntityFrameworkCore;

namespace DogsHouse.API.Services;

public class DogService : IDogService
{
    private readonly PetsDbContext _ctx;
    private readonly IMapper _mapper;
    
    private static readonly Dictionary<string, Expression<Func<Dog, object>>> SortExpressions = new()
    {
        {"id", d => d.Id},
        {"name", d => d.Name},
        {"color", d => d.Color},
        {"tailLength", d => d.TailLength},
        {"weight", d => d.Weight}
    };
    
    public DogService(PetsDbContext ctx, IMapper mapper)
    {
        _ctx = ctx;
        _mapper = mapper;
    }

    public string Ping()
    {
        var message = "Dogshouseservice.Version1.0.1";

        return message;
    }

    public async Task<Dog> AddAsync(DogAddDto? dog, CancellationToken cancellationToken)
    {
        if (dog != null)
        {
            try
            {
                var entity = _mapper.Map<Dog>(dog);

                _ctx.Dogs.Add(entity);

                await _ctx.SaveChangesAsync(cancellationToken);

                return entity;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        throw new NullReferenceException(); 
    }

    public async Task<IEnumerable<DogDto>> ListAsync(string? attribute, string? order, int? pageNumber, int? pageSize, CancellationToken cancellationToken)
    {
        var query = _ctx.Dogs.AsQueryable();

        if (!string.IsNullOrWhiteSpace(attribute) && !string.IsNullOrWhiteSpace(order))
        {
            if (SortExpressions.TryGetValue(attribute, out var expression))
            {
                query = query.Sort(order, expression);
            }
        }

        if (pageNumber != null && pageSize != null)
        {
            if (pageNumber > 0 && pageSize > 0)
            {
                query = query
                    .Skip(((int)pageNumber - 1) * (int)pageSize)
                    .Take((int)pageSize);
            }
        }
        
        var data = await query
            .ProjectTo<DogDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        
        return data;
    }
}