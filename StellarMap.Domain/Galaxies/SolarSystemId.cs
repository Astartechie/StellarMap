namespace StellarMap.Domain.Galaxies;

public class SolarSystemId : SingleValueObject<SolarSystemId, Guid>
{
    public static SolarSystemId Create(Guid value)
        => new(value);

    protected SolarSystemId(Guid value) : base(value)
    {
    }
}