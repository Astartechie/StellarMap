namespace StellarMap.Domain.Galaxies.Planets;

public class PlanetId : SingleValueObject<PlanetId, Guid>
{
    public static PlanetId Create(Guid value)
        => new(value);

    protected PlanetId(Guid value) : base(value)
    {
    }
}