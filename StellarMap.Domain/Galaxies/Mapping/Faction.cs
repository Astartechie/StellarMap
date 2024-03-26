namespace StellarMap.Domain.Galaxies.Mapping;

public class Faction(FactionId id, Name name, Colour borderColour, Colour fillColour) : Entity<FactionId>(id)
{
    public static readonly Faction None = new(FactionId.Create(Guid.Empty), Name.None, Colour.Black, Colour.Black);

    public Name Name { get; } = name;
    public Colour BorderColour { get; } = borderColour;
    public Colour FillColour { get; } = fillColour;
}