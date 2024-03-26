using System.Globalization;

namespace StellarMap.Domain.Galaxies.Mapping;

public class Colour : ValueObject<Colour>
{
    private const byte DefaultAlpha = 255;
    private const char Hash = '#';
    private const int RgbHexCodeLength = 6;
    private const int RgbaHexCodeLength = 8;


    public static class Defined
    {
        public static readonly Colour Black = Create(0, 0, 0);
        public static readonly Colour White = Create(255, 255, 255);
        public static readonly Colour None = Create(0, 0, 0, 0);
    }

    public static Colour Create(byte red, byte green, byte blue, byte alpha = DefaultAlpha)
        => new(red, green, blue, alpha);

    public static Colour Create(string hexCode)
    {
        hexCode = hexCode.TrimStart(Hash);

        if (hexCode.Length != RgbHexCodeLength && hexCode.Length != RgbaHexCodeLength) throw new ArgumentOutOfRangeException(nameof(hexCode));

        var red = byte.Parse(hexCode[..2], NumberStyles.HexNumber);
        var green = byte.Parse(hexCode[2..4], NumberStyles.HexNumber);
        var blue = byte.Parse(hexCode[4..6], NumberStyles.HexNumber);

        var alpha = hexCode.Length == RgbaHexCodeLength
            ? byte.Parse(hexCode[6..8], NumberStyles.HexNumber)
            : DefaultAlpha;

        return new Colour(red, green, blue, alpha);
    }

    protected Colour(byte red, byte green, byte blue, byte alpha)
    {
        Red = red;
        Green = green;
        Blue = blue;
        Alpha = alpha;
    }

    public byte Red { get; }
    public byte Green { get; }
    public byte Blue { get; }
    public byte Alpha { get; }

    public string ToHexCode()
        => $"{Hash}{Red:X2}{Green:X2}{Blue:X2}{(Alpha == DefaultAlpha ? string.Empty : $"{Alpha:X2}")}";

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Red;
        yield return Green;
        yield return Blue;
        yield return Alpha;
    }
}