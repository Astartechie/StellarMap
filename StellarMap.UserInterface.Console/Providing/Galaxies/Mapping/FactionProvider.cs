using Microsoft.Extensions.Logging;
using StellarMap.Application.Generation;
using StellarMap.Domain.Galaxies;
using StellarMap.Domain.Galaxies.Mapping;

namespace StellarMap.UserInterface.Console.Providing.Galaxies.Mapping;

internal class FactionProvider(
    ILogger<FactionProvider> logger,
    IReader<FactionProvider> reader
) : IProvider<IList<Faction>>
{
    private const char Delimiter = ',';
    private const int ExpectedColumnCount = 4;

    public IList<Faction> Provide()
    {
        if (_factions.Count > 0) return _factions;
        var lineCount = 0;
        foreach (var line in reader.ReadLines())
        {
            lineCount++;
            var columns = line.Split(Delimiter);
            if (columns.Length != ExpectedColumnCount)
            {
                logger.LogWarning("Line {lineCount} has {columns} instead of the expected {expectedColumnCount}.", lineCount, columns.Length, ExpectedColumnCount);
                continue;
            }

            var id = FactionId.Create(Guid.Parse(columns[0]));
            var name = Name.Create(columns[1]);
            var borderColour = Colour.Create(columns[2]);
            var fillColour = Colour.Create(columns[3]);
            _factions.Add(new Faction(id, name, borderColour, fillColour));
        }

        return _factions;
    }

    private readonly List<Faction> _factions = [];
}