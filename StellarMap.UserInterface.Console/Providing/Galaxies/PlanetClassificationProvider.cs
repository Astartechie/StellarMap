using Microsoft.Extensions.Logging;
using StellarMap.Application.Generation;
using StellarMap.Domain.Galaxies;
using StellarMap.Domain.Galaxies.Mapping;
using StellarMap.Domain.Galaxies.Planets;

namespace StellarMap.UserInterface.Console.Providing.Galaxies;

internal class PlanetClassificationProvider(
    ILogger<PlanetClassificationProvider> logger,
    IReader<PlanetClassificationProvider> reader
) : IProvider<WeightedList<PlanetClassification>>
{
    private const char Delimiter = ',';
    private const int ExpectedColumnCount = 4;

    public WeightedList<PlanetClassification> Provide()
    {
        if (_planetClassifications.Items.Any()) return _planetClassifications;

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

            var id = PlanetClassificationId.Create(Guid.Parse(columns[0]));
            var name = Name.Create(columns[1]);
            var colour = Colour.Create(columns[2]);

            var classification = new PlanetClassification(id, name, colour);

            var weighting = Weighting.Create(double.Parse(columns[3]));
            _planetClassifications.Add(classification, weighting);
        }

        return _planetClassifications;
    }

    private readonly WeightedList<PlanetClassification> _planetClassifications = new([]);
}