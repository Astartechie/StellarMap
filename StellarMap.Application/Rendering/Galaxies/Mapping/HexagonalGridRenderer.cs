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
        foreach (var (position, tile) in input.Tiles)
        {
            var centerPoint = ToPoint(position);
            var hexagon = Hexagon(centerPoint, settings.HexSize);

            if (tile.IsOwned())
            {
                graphics.FillPolygon(hexagon, Brush.Create(tile.Owner.FillColour));

                foreach (var (neighbouringPosition, neighbouringTile) in input.GetNeighbouringTiles(position))
                {
                    if (neighbouringTile.Owner.Id == tile.Owner.Id) continue;
                    var neighbouringCenterPoint = ToPoint(neighbouringPosition);
                    var direction = neighbouringCenterPoint - centerPoint;

                    var midPoint = centerPoint + direction / 2;
                    var normalised = Normalize(direction);

                    var start = midPoint - (normalised * 2) + PerpendicularClockwise(normalised) * (settings.HexSize / 2 - 1);
                    var end = midPoint - (normalised * 2) + PerpendicularCounterClockwise(normalised) * (settings.HexSize / 2 - 1);
                    graphics.DrawLine(start, end, Pen.Create(1, tile.Owner.BorderColour));
                }
            }

            if (!tile.IsEmpty())
            {
                var angle = 2 * Math.PI / tile.System.Stars.Count;
                var maxSize = settings.HexSize / tile.System.Stars.Count;

                var index = 0;
                foreach (var star in tile.System.Stars)
                {
                    var starCenterPoint = centerPoint;
                    if (tile.System.Stars.Count > 1)
                    {
                        var xOffset = Math.Cos(angle * index) * (settings.HexSize / 2);
                        var yOffset = Math.Sin(angle * index) * (settings.HexSize / 2);

                        starCenterPoint += Point.Create((float)xOffset, (float)yOffset);
                    }

                    graphics.FillCircle(starCenterPoint, (float)Math.Min(star.Size.Value / StarSize.Maximum.Value * settings.HexSize, maxSize), Brush.Create(star.Classification.Colour));
                    index++;
                }
            }

            graphics.DrawPolygon(hexagon, GridLinesPen);
        }

        renderTarget.End();
    }

    private static Point Normalize(Point position)
        => position / (float)position.Length();

    private static Point PerpendicularClockwise(Point position)
        => Point.Create(position.Y, -position.X);

    private static Point PerpendicularCounterClockwise(Point position)
        => Point.Create(-position.Y, position.X);

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