namespace DogsHouse.API.QueryParameters;

public class DogQueryParameter
{
    public string? Attribute { get; set; }
    public string? Order { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}