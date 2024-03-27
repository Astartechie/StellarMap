namespace StellarMap.Domain.Galaxies.Mapping;

public class Tile(SolarSystem system, Faction owner)
{
    public static readonly Tile Empty = new(SolarSystem.Empty, Faction.None);

    public SolarSystem System { get; } = system;

    public Faction Owner { get; private set; } = owner;

    public bool IsEmpty()
        => System.Id == SolarSystem.Empty.Id;

    public bool IsOwned()
        => Owner.Id != Faction.None.Id;

    public void ChangeOwner(Faction faction)
    {
        Owner = faction;
    }
}