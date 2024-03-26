using StellarMap.Domain.Galaxies;
using StellarMap.Domain.Galaxies.Mapping;

namespace StellarMap.Application.Rendering.Galaxies.Mapping;

public class HexagonalGridRenderer(IRenderTarget renderTarget, HexagonalGridRenderer.Settings settings) : IRenderer<HexagonalGrid>
{
    public class Settings(int width, int height, float hexSize)
    {
        public int Width { get; } = width;
        public int Height { get; } = height;
        public float HexSize { get; } = hexSize;
    }

    public void Render(HexagonalGrid input)
    {
        var graphics = renderTarget.Start(settings.Width, settings.Height);
        graphics.FillRectangle(0, 0, settings.Width, settings.Height, Brush.Create(Colour.Defined.Black));
        foreach (var entry in input.Tiles)
        {
            var centerPoint = ToPoint(entry.Key);
            var hexagon = Hexagon(centerPoint, settings.HexSize);

            if (entry.Value.System.Id != SolarSystem.Empty.Id)
            {
                foreach (var star in entry.Value.System.Stars)
                {
                    graphics.FillCircle(centerPoint, (float)(star.Size.Value / StarSize.Maximum.Value * settings.HexSize), Brush.Create(star.Classification.Colour));
                }
            }

            graphics.DrawPolygon(hexagon, GridLinesPen);
        }

        renderTarget.End();
    }

    private IList<Point> Hexagon(Point center, float sideLength)
    {
        var points = new List<Point>();

        // Calculate points
        for (var i = 0; i < 6; i++)
        {
            float angleDeg = 60 * i; // Angle in degrees
            var angleRad = (float)(Math.PI / 180) * angleDeg; // Angle in radians

            var x = center.X + sideLength * (float)Math.Cos(angleRad);
            var y = center.Y + sideLength * (float)Math.Sin(angleRad);

            points.Add(Point.Create(x, y));
        }

        return points;
    }

    private static readonly Pen GridLinesPen = Pen.Create(1, Colour.Defined.White);

    private Point ToPoint(Coordinate position)
        => Point.Create((settings.HexSize * 3.0f / 2.0f * position.X) + (float)settings.Width / 2, (float)(settings.HexSize * (Math.Sqrt(3.0) * (position.X / 2.0 + position.Y))) + (float)settings.Height / 2);
}