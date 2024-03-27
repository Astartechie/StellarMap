using StellarMap.Domain.Galaxies;
using StellarMap.Domain.Galaxies.Mapping;

namespace StellarMap.Application.Generation.Generators.Galaxies.Mapping;

public class HexagonalGridGenerator(
    HexagonalGridGenerator.Settings settings,
    IStellarNoise stellarNoise,
    IStarGenerator starGenerator) : IHexagonalGridGenerator
{

    private const int MinimumStarCount = 36; // Value determined by what looked right

    public class Settings(int radius)
    {
        public int Radius { get; } = radius;
    }

    public HexagonalGrid Generate()
    {
        var grid = new HexagonalGrid(new Dictionary<Coordinate, Tile>());
        foreach (var position in HexagonalGrid.GetCoordinatesInRadius(settings.Radius))
        {
            var starCount = stellarNoise.StarCount(position);
            if (starCount < MinimumStarCount)
            {
                grid.SetTile(position, Tile.Empty);
                continue;
            }

            var star = starGenerator.Generate();
            grid.SetTile(position, new Tile(new SolarSystem(SolarSystemId.Create(Guid.NewGuid()), [star]), Faction.None));
        }

        return grid;
    }
}