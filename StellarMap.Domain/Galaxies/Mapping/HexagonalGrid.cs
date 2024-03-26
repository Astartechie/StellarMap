namespace StellarMap.Domain.Galaxies.Mapping;

public class HexagonalGrid(IDictionary<Coordinate, Tile> tiles)
{
    public static readonly IReadOnlyList<Coordinate> Directions =
    [
        Coordinate.Create(1, 0),
        Coordinate.Create(-1, 0),
        Coordinate.Create(0, 1),
        Coordinate.Create(0, -1),
        Coordinate.Create(1, -1),
        Coordinate.Create(-1, 1),
    ];

    public static IEnumerable<Coordinate> GetCoordinatesInRadius(int radius)
    {
        for (var q = -radius; q <= radius; q++)
        {
            var r1 = Math.Max(-radius, -q - radius);
            var r2 = Math.Min(radius, -q + radius);

            for (var r = r1; r <= r2; r++)
            {
                yield return Coordinate.Create(q, r);
            }
        }
    }

    public IReadOnlyDictionary<Coordinate, Tile> Tiles => tiles.AsReadOnly();

    public IReadOnlyDictionary<Coordinate, Tile> GetNeighbouringTiles(Coordinate position)
        => Directions.Select(direction => position + direction).ToDictionary(neighbourPosition => neighbourPosition, GetTile);

    public static IEnumerable<Coordinate> GetNeighbours(Coordinate position)
        => Directions.Select(direction => direction + position);

    public Tile GetTile(Coordinate position)
        => tiles.TryGetValue(position, out var tile) ? tile : Tile.Empty;

    public void SetTile(Coordinate position, Tile tile)
        => tiles[position] = tile;
}