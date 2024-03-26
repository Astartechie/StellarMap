using StellarMap.Domain;

namespace StellarMap.Application.Rendering;

public class Point : ValueObject<Point>
{
    public static Point Create(float x, float y)
        => new(x, y);

    protected Point(float x, float y)
    {
        X = x;
        Y = y;
    }

    public float X { get; }
    public float Y { get; }

    public static Point operator +(Point left, Point right)
        => new(left.X + right.X, left.Y + right.Y);

    public static Point operator -(Point left, Point right)
        => new(left.X - right.X, left.Y - right.Y);

    public static Point operator /(Point left, int right)
        => new(left.X / right, left.Y / right);

    public static Point operator *(Point left, int right)
        => new(left.X * right, left.Y * right);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return X;
        yield return Y;
    }
}