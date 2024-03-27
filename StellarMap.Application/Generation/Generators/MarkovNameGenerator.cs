namespace StellarMap.Application.Generation.Generators;

public class MarkovNameGenerator(Random random)
{
    private readonly Dictionary<char, Dictionary<char, int>> _transitions = [];

    public void BuildTransitionTable(IEnumerable<string> names)
    {
        foreach (var name in names)
        {
            for (var i = 0; i < name.Length - 1; i++)
            {
                var currentState = name[i];
                var nextState = name[i + 1];

                if (!_transitions.TryGetValue(currentState, out var value))
                {
                    value = [];
                    _transitions[currentState] = value;
                }

                value.TryAdd(nextState, 0);

                value[nextState]++;
            }
        }
    }

    public string GenerateName(int minLength, int maxLength)
    {
        var name = "";
        var length = random.Next(minLength, maxLength + 1);

        var upper = _transitions.Keys.Where(char.IsUpper).ToList();
        var currentState = upper[random.Next(upper.Count)];

        while (name.Length < length)
        {
            name += currentState;

            if (!_transitions.ContainsKey(currentState))
                break;

            var possibleNextStates = _transitions[currentState];
            var nextState = GetWeightedRandom(possibleNextStates);
            currentState = nextState;
        }

        return name.Trim();
    }

    private char GetWeightedRandom(Dictionary<char, int> transitions)
    {
        var totalWeight = transitions.Values.Sum();

        var randomValue = random.Next(totalWeight);

        foreach (var kvp in transitions)
        {
            randomValue -= kvp.Value;
            if (randomValue <= 0)
                return kvp.Key;
        }

        return transitions.Keys.ToList()[random.Next(transitions.Keys.Count)];
    }
}