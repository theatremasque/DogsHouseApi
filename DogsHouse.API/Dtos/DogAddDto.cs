namespace DogsHouse.API.Dtos;

public record DogAddDto
{
    public string Name { get; set; } = null!;
    
    public string Color { get; set; } = null!;

    public int TailLength { get; set; }

    public int Weight { get; set; }
}