using DogsHouse.API.Dtos;

namespace DogsHouse.UnitTests.Comparers;

public class DogDtoComparer : IEqualityComparer<DogDto>
{
    public bool Equals(DogDto x, DogDto y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        if (x.GetType() != y.GetType()) return false;
        return x.Id == y.Id && x.Name == y.Name && x.Color == y.Color && x.TailLength == y.TailLength && x.Weight == y.Weight;
    }

    public int GetHashCode(DogDto obj)
    {
        return HashCode.Combine(obj.Id, obj.Name, obj.Color, obj.TailLength, obj.Weight);
    }
}