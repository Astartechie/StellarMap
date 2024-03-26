using Microsoft.Extensions.Logging;
using StellarMap.Application.Generation;
using StellarMap.Domain.Galaxies;
using StellarMap.Domain.Galaxies.Mapping;

namespace StellarMap.UserInterface.Console.Providing.Galaxies;

internal class StarClassificationProvider(
    ILogger<StarClassificationProvider> logger,
    IReader<StarClassificationProvider> reader
) : IProvider<WeightedList<StarClassification>>
{
    private const char Delimiter = ',';
    private const int ExpectedColumnCount = 5;

    public WeightedList<StarClassification> Provide()
    {
        if (_starClassifications.Items.Any()) return _starClassifications;

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

            var id = StarClassificationId.Create(columns[0].First());
            var colour = Colour.Create(columns[1]);
            var sizeRange = Range<StarSize>.Create(StarSize.Create(double.Parse(columns[2])), StarSize.Create(double.Parse(columns[3])));

            var classification = new StarClassification(id, colour, sizeRange);

            var weighting = Weighting.Create(double.Parse(columns[4]));
            _starClassifications.Add(classification, weighting);
        }

        return _starClassifications;
    }

    private readonly WeightedList<StarClassification> _starClassifications = new([]);
}