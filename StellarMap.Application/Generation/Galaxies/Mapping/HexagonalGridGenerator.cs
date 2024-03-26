using System.Drawing;
using StellarMap.Domain.Galaxies;
using StellarMap.Domain.Galaxies.Mapping;

namespace StellarMap.Application.Generation.Galaxies.Mapping;

public class HexagonalGridGenerator(
    HexagonalGridGenerator.Settings settings,
    IGenerator<double> doubleGenerator,
    IGenerator<Star> starGenerator) : IGenerator<HexagonalGrid>
{
    public class Settings(int radius, float hexSize)
    {
        public int Radius { get; } = radius;
        public float HexSize { get; } = hexSize;
    }

    public HexagonalGrid Generate()
    {
        var resolution = (int)(settings.HexSize * 4 * settings.Radius);
        var grid = new HexagonalGrid(new Dictionary<Coordinate, Tile>());
        foreach (var position in HexagonalGrid.GetCoordinatesInRadius(settings.Radius))
        {
            var point = ToPixel(position);

            var sX = (int)(10000 * point.X / resolution / 2);
            var sY = (int)(10000 * point.Y / resolution / 2);

            var density = Math.Max(Math.Max(Core(sX, sY), Arms(sX, sY)), 0);
            var stars = Math.Round(78 * density * (1 + 2.2 * Math.Pow(doubleGenerator.Generate() - 0.5, 3)));

            if (stars < 36)
            {
                grid.SetTile(position, Tile.Empty);
                continue;
            }

            var star = starGenerator.Generate();
            grid.SetTile(position, new Tile(new SolarSystem(SolarSystemId.Create(Guid.NewGuid()), [star]), Faction.None));
        }

        return grid;
    }

    private PointF ToPixel(Coordinate position)
        => new(settings.HexSize * 3.0f / 2.0f * position.X, (float)(settings.HexSize * (Math.Sqrt(3.0) * (position.X / 2.0 + position.Y))));

    private static double Distance(int x, int y)
        => Math.Sqrt(x * x + y * y);

    public static double Angle(int x, int y)
        => Math.Atan((double)y / x);

    public static double Core(int x, int y)
        => 1 - Math.Pow(Distance(x, y) / 200, 2);

    public static double Arms(int x, int y)
        => Math.Exp(-Distance(x, y) / 1500) * 0.5 * Math.Pow(Math.Sin(Math.Pow(0.5 * Distance(x, y), 0.35) - Angle(x, y)), 2) + 0.5 - Distance(x, y) / 10000;
}