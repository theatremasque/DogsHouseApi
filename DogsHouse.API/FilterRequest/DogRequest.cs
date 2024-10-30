﻿namespace DogsHouse.API.FilterRequest;

public class DogRequest
{
    public string? Attribute { get; set; }
    public string? Order { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}