namespace StellarMap.Domain.Galaxies;

public class Star(StarId id, Name name, StarClassification classification, float size) : Entity<StarId>(id)
{
    public Name Name { get; } = name;
    public StarClassification Classification { get; } = classification;
    public float Size { get; } = size;
}