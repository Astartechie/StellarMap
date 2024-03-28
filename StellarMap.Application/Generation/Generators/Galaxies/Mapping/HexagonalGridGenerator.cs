using StellarMap.Domain.Galaxies;
using StellarMap.Domain.Galaxies.Mapping;

namespace StellarMap.Application.Generation.Generators.Galaxies.Mapping;

public class HexagonalGridGenerator(
    IStellarNoise stellarNoise,
    IStarGenerator starGenerator,
    IRandomIntGenerator intGenerator,
    IProvider<IList<Faction>> factionProvider,
    HexagonalGridGenerator.Settings settings) : IHexagonalGridGenerator
{
    private const int MinimumStarCount = 36; // Value determined by what looked right

    public class Settings(int radius)
    {
        public int Radius { get; } = radius;
    }

    public HexagonalGrid Generate()
    {
        var factions = factionProvider.Provide();
        var grid = new HexagonalGrid(new Dictionary<Coordinate, Tile>());
        foreach (var position in HexagonalGrid.GetCoordinatesInRadius(settings.Radius))
        {
            var starCount = stellarNoise.StarCount(position);
            if (starCount < MinimumStarCount)
            {
                grid.SetTile(position, new Tile(SolarSystem.Empty, Faction.None));
                continue;
            }

            var star = starGenerator.Generate();
            grid.SetTile(position, new Tile(new SolarSystem(SolarSystemId.Create(Guid.NewGuid()), [star]), Faction.None));
        }

        var available = new HashSet<Coordinate>(grid.Tiles.Keys);
        var players = new List<Player>();


        var minClaims = available.Count / settings.Radius;
        var maxClaims = available.Count / (settings.Radius / 2);
        foreach (var faction in factions)
        {
            var bestStartingPosition = available.Where(x => !grid.GetTile(x).IsEmpty()).MaxBy(position => StartingScore(position, players));

            var tile = grid.GetTile(bestStartingPosition);
            tile.ChangeOwner(faction);

            players.Add(new Player(faction, bestStartingPosition, [bestStartingPosition], intGenerator.Generate((minClaims, maxClaims))));
        }

        var outs = new List<Player>();

        while (players.Count > 0)
        {
            foreach (var player in players)
            {
                if (player.OpenPositions.Count < 1 || player.RemainingClaims < 1)
                {
                    outs.Add(player);
                    continue;
                }

                var choices = new List<Choice>();
                var closed = new HashSet<Coordinate>();
                foreach (var openPosition in player.OpenPositions)
                {
                    var openChoices = new List<Choice>();
                    foreach (var neighbouringEntry in grid.GetNeighbouringTiles(openPosition))
                    {
                        if (neighbouringEntry.Value.IsOwned()) continue;
                        // if (neighbouringEntry.Value.IsEmpty()) continue;
                        if (!available.Contains(neighbouringEntry.Key)) continue;
                        var cost = CalculateCost(player, neighbouringEntry.Key, neighbouringEntry.Value);

                        openChoices.Add(new Choice(neighbouringEntry.Key, cost));
                    }

                    if (openChoices.Count == 0)
                    {
                        closed.Add(openPosition);
                        continue;
                    }

                    choices.AddRange(openChoices);
                }

                foreach (var position in closed)
                {
                    player.OpenPositions.Remove(position);
                }

                if (choices.Count == 0)
                {
                    outs.Add(player);
                    continue;
                }

                var bestPossible = choices.MinBy(x => x.Cost);

                player.RemainingClaims--;
                player.OpenPositions.Add(bestPossible.Position);
                var tile = grid.GetTile(bestPossible.Position);
                tile.ChangeOwner(player.Faction);
                available.Remove(bestPossible.Position);
            }

            foreach (var faction in outs)
            {
                players.Remove(faction);
            }

            outs.Clear();
        }

        return grid;
    }

    record Choice(Coordinate Position, double Cost);

    class Player(Faction faction, Coordinate startingPosition, HashSet<Coordinate> openPositions, int remainingClaims)
    {
        public Faction Faction { get; } = faction;
        public Coordinate StartingPosition { get; } = startingPosition;
        public HashSet<Coordinate> OpenPositions { get; } = openPositions;
        public int RemainingClaims { get; set; } = remainingClaims;
    }

    private double StartingScore(Coordinate position, IList<Player> players)
    {
        var score = settings.Radius - position.Length();

        if (players.Count > 0)
        {
            score *= players.Min(player => (player.StartingPosition - position).Length());
        }

        return score;
    }

    private double CalculateCost(Player player, Coordinate position, Tile tile)
    {
        var score = 0;
        score += intGenerator.Generate((1, (int)(player.StartingPosition - position).Length()));
        score *= tile.IsEmpty() ? 2 : 1;
        return score;
    }
}