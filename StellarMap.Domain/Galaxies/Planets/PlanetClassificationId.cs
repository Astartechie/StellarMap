namespace StellarMap.Domain.Galaxies.Planets;

public class PlanetClassificationId : SingleValueObject<PlanetClassificationId, Guid>
{
    public static PlanetClassificationId Create(Guid value)
        => new(value);

    protected PlanetClassificationId(Guid value) : base(value)
    {
    }
}