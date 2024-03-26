namespace StellarMap.Domain.Galaxies;

public class Name : SingleValueObject<Name, string>
{
    public static readonly Name None = new(string.Empty);

    public static Name Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value));
        return new Name(value);
    }

    protected Name(string value) : base(value)
    {
    }
}