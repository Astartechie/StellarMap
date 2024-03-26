namespace StellarMap.Domain.Galaxies.Mapping;

public class Coordinate : ValueObject<Coordinate>
{
    public static Coordinate Create(int x, int y)
        => new(x, y);

    protected Coordinate(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X { get; }
    public int Y { get; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return X;
        yield return Y;
    }

    public static Coordinate operator +(Coordinate left, Coordinate right)
        => new(left.X + right.X, left.Y + right.Y);

    public static Coordinate operator -(Coordinate left, Coordinate right)
        => new(left.X - right.X, left.Y - right.Y);

    public static Coordinate operator /(Coordinate left, int right)
        => new(left.X / right, left.Y / right);

    public static Coordinate operator *(Coordinate left, int right)
        => new(left.X * right, left.Y * right);

    public double Length()
        => Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));
}