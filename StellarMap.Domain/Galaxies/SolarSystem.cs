using StellarMap.Domain.Galaxies.Planets;
using StellarMap.Domain.Galaxies.Stars;

namespace StellarMap.Domain.Galaxies;

public class SolarSystem(SolarSystemId id, IEnumerable<Star> stars, IEnumerable<Planet> planets) : Entity<SolarSystemId>(id)
{
    public static readonly SolarSystem Empty = new(SolarSystemId.Create(Guid.Empty), [], []);
    public IReadOnlyList<Star> Stars => _stars.AsReadOnly();
    public IReadOnlyList<Planet> Planets => _planets.AsReadOnly();

    private readonly List<Star> _stars = [.. stars];
    private readonly List<Planet> _planets = [.. planets];
}