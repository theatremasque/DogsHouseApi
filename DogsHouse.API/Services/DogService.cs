using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DogsHouse.API.Dtos;
using DogsHouse.API.Entities;
using DogsHouse.API.Infrastructure;
using DogsHouse.API.QueryExtensions;
using DogsHouse.API.QueryParameters;
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

   

    public async Task<Dog> AddAsync(DogAddDto dog, CancellationToken cancellationToken)
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
            throw new DbUpdateException($"{e.Message}");
        }
    }

    public async Task<IEnumerable<DogDto>> ListAsync(DogQueryParameter? parameters, CancellationToken cancellationToken)
    {
        var query = _ctx.Dogs.AsQueryable();

        if (IsValidFilterParameters(parameters))
        {
            if (SortExpressions.TryGetValue(parameters!.Attribute!, out var expression))
            {
                query = query.Sort(parameters.Order!, expression);
            }
        }
        else
        {
            query = query.OrderBy(d => d.Id);
        }

        if (IsValidPaginationParameters(parameters))
        {
            query = query
                .Skip(((int)parameters!.PageNumber! - 1) * (int)parameters.PageSize!)
                .Take((int)parameters.PageSize);
        }
        
        var data = await query
            .ProjectTo<DogDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        
        return data;
    }

    private bool IsValidPaginationParameters(DogQueryParameter? parameters)
    {
        if (parameters != null)
        {
            return parameters.PageNumber != null && parameters.PageSize  != null &&
                   parameters.PageNumber > 0 && parameters.PageSize > 0;
        }
        
        return false;
    }

    private bool IsValidFilterParameters(DogQueryParameter? parameters)
    {
        if (parameters != null)
        {
            return !string.IsNullOrWhiteSpace(parameters.Attribute) && !string.IsNullOrWhiteSpace(parameters.Order);
        }

        return false;
    }
}