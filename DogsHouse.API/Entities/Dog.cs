namespace DogsHouse.API.Entities;

public class Dog
{
    /// <summary>
    /// I know that in table, pk can be any field, or a couple of field, but
    /// added ID, because attached to Name like a pk is worse case. For example: we have 2 dogs with the same names,
    /// so we can`t add second dog, but second dog is another pet.
    /// also it will be good in future when we want to find some dog by id.
    /// </summary>
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    /// <summary>
    /// I thought about make it enum, and add some types of colors, but if it`s mixing, just stay it in String, and user can
    /// add any dogs with any type of color
    /// </summary>
    public string Color { get; set; } = null!;

    public int TailLength { get; set; }

    public int Weight { get; set; }
}