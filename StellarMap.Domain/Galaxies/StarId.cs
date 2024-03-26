namespace StellarMap.Domain.Galaxies;

public class StarId : SingleValueObject<StarId, Guid>
{
    public static StarId Create(Guid value)
        => new(value);

    protected StarId(Guid value) : base(value)
    {
    }
}