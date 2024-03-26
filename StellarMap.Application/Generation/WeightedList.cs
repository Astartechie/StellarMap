namespace StellarMap.Application.Generation;

public class WeightedList<T>(IEnumerable<WeightedValue<T>> values)
{
    public void Add(T item, Weighting weighting)
        => _items.Add(WeightedValue<T>.Create(item, weighting));

    public IReadOnlyList<WeightedValue<T>> Items => _items.AsReadOnly();

    public T GetItemFromWeight(Weighting weight)
    {
        double cumulativeProbability = 0;

        // Iterate through classification weights to find the chosen classification
        foreach (var kvp in _items)
        {
            cumulativeProbability += kvp.Weight.Value;

            // If the random value is less than the cumulative probability, select this classification
            if (weight.Value < cumulativeProbability)
            {
                return kvp.Value;
            }
        }

        return _items.Last().Value;
    }

    private readonly List<WeightedValue<T>> _items = [..values];
}