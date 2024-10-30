using DogsHouse.API.Dtos;

namespace DogsHouse.API.Services;

public interface IDogService
{
    public string Ping();

    public Task Add(DogAddDto? dog, CancellationToken cancellationToken);
}