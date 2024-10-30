using DogsHouse.API.Dtos;
using DogsHouse.API.FilterRequest;

namespace DogsHouse.API.Services;

public interface IDogService
{
    public string Ping();

    public Task AddAsync(DogAddDto? dog, CancellationToken cancellationToken);

    public Task<IEnumerable<DogDto>> ListAsync(DogRequest request, CancellationToken cancellationToken);
}