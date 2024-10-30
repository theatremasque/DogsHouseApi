using DogsHouse.API.Dtos;

namespace DogsHouse.API.Services;

public interface IDogService
{
    public string Ping();

    public Task AddAsync(DogAddDto? dog, CancellationToken cancellationToken);

    public Task<IEnumerable<DogDto>> ListAsync(string? attribute, string? order,CancellationToken cancellationToken);
}