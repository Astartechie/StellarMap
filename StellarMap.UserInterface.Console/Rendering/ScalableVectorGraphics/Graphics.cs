using StellarMap.Application.Rendering;
using System.Text;

namespace StellarMap.UserInterface.Console.Rendering.ScalableVectorGraphics
{
    internal class Graphics(IWriter writer) : IGraphics
    {
        public void DrawCircle(Point center, float radius, Pen pen)
            => DrawAndFillCircle(center, radius, pen, Brush.None);

        public void FillCircle(Point center, float radius, Brush brush)
            => DrawAndFillCircle(center, radius, Pen.None, brush);

        public void DrawAndFillCircle(Point center, float radius, Pen pen, Brush brush)
        {
            var svgCircle = new StringBuilder();
            svgCircle.Append($"<circle cx=\"{center.X}\" cy=\"{center.Y}\" r=\"{radius}\"");

            if (pen != Pen.None)
            {
                svgCircle.Append($" stroke=\"{pen.Colour.ToHexCode()}\" stroke-width=\"{pen.Thickness}\"");
            }
            else
            {
                svgCircle.Append(" stroke=\"none\"");
            }

            if (brush != Brush.None)
            {
                svgCircle.Append($" fill=\"{brush.Colour.ToHexCode()}\"");
            }
            else
            {
                svgCircle.Append(" fill=\"none\"");
            }

            svgCircle.Append("/>");

            writer.WriteLine(svgCircle.ToString());
        }

        public void DrawRectangle(int x, int y, int width, int height, Pen pen)
            => DrawAndFillRectangle(x, y, width, height, pen, Brush.None);

        public void FillRectangle(int x, int y, int width, int height, Brush brush)
            => DrawAndFillRectangle(x, y, width, height, Pen.None, brush);

        public void DrawAndFillRectangle(int x, int y, int width, int height, Pen pen, Brush brush)
        {
            var svgRectangle = new StringBuilder();
            svgRectangle.Append($"<rect x=\"{x}\" y=\"{y}\" width=\"{width}\" height=\"{height}\"");

            if (pen != Pen.None)
            {
                svgRectangle.Append($" stroke=\"{pen.Colour.ToHexCode()}\" stroke-width=\"{pen.Thickness}\"");
            }
            else
            {
                svgRectangle.Append(" stroke=\"none\"");
            }

            if (brush != Brush.None)
            {
                svgRectangle.Append($" fill=\"{brush.Colour.ToHexCode()}\"");
            }
            else
            {
                svgRectangle.Append(" fill=\"none\"");
            }

            svgRectangle.Append("/>");

            writer.WriteLine(svgRectangle.ToString());
        }

        public void DrawPolygon(IList<Point> points, Pen pen)
            => DrawAndFillPolygon(points, pen, Brush.None);

        public void FillPolygon(IList<Point> points, Brush brush)
            => DrawAndFillPolygon(points, Pen.None, brush);

        public void DrawAndFillPolygon(IList<Point> points, Pen pen, Brush brush)
        {
            var svgPath = new StringBuilder();
            var count = 0;
            svgPath.Append($"<path d=\"");

            foreach (var point in points)
            {
                if (count == 0)
                {
                    svgPath.Append("M ");
                }

                svgPath.Append($"{point.X} {point.Y}");
                svgPath.Append(count == 0 ? " L " : " ");
                count++;
            }

            svgPath.Append("Z\"");

            if (pen != Pen.None)
            {
                svgPath.Append($" stroke=\"{pen.Colour.ToHexCode()}\" stroke-width=\"{pen.Thickness}\"");
            }
            else
            {
                svgPath.Append(" stroke=\"none\"");
            }

            if (brush != Brush.None)
            {
                svgPath.Append($" fill=\"{brush.Colour.ToHexCode()}\"");
            }
            else
            {
                svgPath.Append(" fill=\"none\"");
            }

            svgPath.Append("/>");

            writer.WriteLine(svgPath.ToString());
        }
    }
}