using DogsHouse.API.Dtos;
using DogsHouse.API.Entities;

namespace DogsHouse.API.Services;

public interface IDogService
{
    public string Ping();

    public Task<Dog> AddAsync(DogAddDto? dog, CancellationToken cancellationToken);

    public Task<IEnumerable<DogDto>> ListAsync(string? attribute, string? order, int? pageNumber, int? pageSize, CancellationToken cancellationToken);
}