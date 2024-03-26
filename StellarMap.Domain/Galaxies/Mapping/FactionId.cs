namespace StellarMap.Domain.Galaxies.Mapping;

public class FactionId : SingleValueObject<FactionId, Guid>
{
    public static FactionId Create(Guid value)
        => new(value);

    protected FactionId(Guid value) : base(value)
    {
    }
}