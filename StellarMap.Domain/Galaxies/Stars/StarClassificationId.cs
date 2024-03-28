namespace StellarMap.Domain.Galaxies.Stars;

public class StarClassificationId : SingleValueObject<StarClassificationId, char>
{
    public static StarClassificationId Create(char value)
        => new(value);

    protected StarClassificationId(char value) : base(value)
    {
    }
}