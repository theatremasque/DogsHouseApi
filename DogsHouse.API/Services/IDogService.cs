using DogsHouse.API.Dtos;
using DogsHouse.API.Entities;
using DogsHouse.API.QueryParameters;

namespace DogsHouse.API.Services;

public interface IDogService
{
    public Task<Dog> AddAsync(DogAddDto dog, CancellationToken cancellationToken);

    public Task<IEnumerable<DogDto>> ListAsync(DogQueryParameter? parameters, CancellationToken cancellationToken);
}