namespace StellarMap.Domain.Galaxies;

public class SolarSystem(SolarSystemId id, IEnumerable<Star> stars) : Entity<SolarSystemId>(id)
{
    public static readonly SolarSystem Empty = new(SolarSystemId.Create(Guid.Empty), Enumerable.Empty<Star>());
    public IReadOnlyList<Star> Stars => _stars.AsReadOnly();

    private readonly List<Star> _stars = [.. stars];
}