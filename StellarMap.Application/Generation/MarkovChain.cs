namespace StellarMap.Application.Generation;

public class MarkovChainBuilder<T>
{
    public void AddTransition(T fromState, T toState, double weight)
    {
        if (!_transitions.TryGetValue(fromState, out var value))
        {
            value = [];
            _transitions[fromState] = value;
        }

        value.TryAdd(toState, 0);

        value[toState] += weight;
    }

    public MarkovChain<T> Build()
        => new(_transitions.ToDictionary(transition => transition.Key, transition => new WeightedList<T>(transition.Value.Select(x => WeightedValue<T>.Create(x.Key, Weighting.Create(x.Value / transition.Value.Count))))));

    private readonly Dictionary<T, Dictionary<T, double>> _transitions = [];
}

public class MarkovChain<T>(IReadOnlyDictionary<T, WeightedList<T>> transitions)
{
    public bool TryGetWeightedList(T key, out WeightedList<T>? weightedList)
        => transitions.TryGetValue(key, out weightedList);
}