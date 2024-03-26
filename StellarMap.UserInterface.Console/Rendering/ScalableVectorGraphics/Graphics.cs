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
            if (!IsVisible(pen, brush)) return;

            var builder = new StringBuilder();
            builder.Append($"<circle cx=\"{center.X}\" cy=\"{center.Y}\" r=\"{radius}\"");

            AddPenAndBrush(builder, pen, brush);
            builder.Append("/>");

            writer.WriteLine(builder.ToString());
        }

        public void DrawRectangle(int x, int y, int width, int height, Pen pen)
            => DrawAndFillRectangle(x, y, width, height, pen, Brush.None);

        public void FillRectangle(int x, int y, int width, int height, Brush brush)
            => DrawAndFillRectangle(x, y, width, height, Pen.None, brush);

        public void DrawAndFillRectangle(int x, int y, int width, int height, Pen pen, Brush brush)
        {
            if (!IsVisible(pen, brush)) return;

            var builder = new StringBuilder();
            builder.Append($"<rect x=\"{x}\" y=\"{y}\" width=\"{width}\" height=\"{height}\"");

            AddPenAndBrush(builder, pen, brush);

            builder.Append("/>");

            writer.WriteLine(builder.ToString());
        }

        public void DrawPolygon(IList<Point> points, Pen pen)
            => DrawAndFillPolygon(points, pen, Brush.None);

        public void FillPolygon(IList<Point> points, Brush brush)
            => DrawAndFillPolygon(points, Pen.None, brush);

        public void DrawAndFillPolygon(IList<Point> points, Pen pen, Brush brush)
        {
            if (!IsVisible(pen, brush)) return;

            var builder = new StringBuilder();
            var count = 0;
            builder.Append($"<path d=\"");

            foreach (var point in points)
            {
                if (count == 0)
                {
                    builder.Append("M ");
                }

                builder.Append($"{point.X} {point.Y}");
                builder.Append(count == 0 ? " L " : " ");
                count++;
            }

            builder.Append("Z\"");

            AddPenAndBrush(builder, pen, brush);

            builder.Append("/>");

            writer.WriteLine(builder.ToString());
        }

        private static bool IsVisible(Pen pen, Brush brush)
            => pen != Pen.None || brush != Brush.None;

        private static void AddPenAndBrush(StringBuilder builder, Pen pen, Brush brush)
        {
            if (pen != Pen.None)
            {
                builder.Append($" stroke=\"{pen.Colour.ToHexCode()}\" stroke-width=\"{pen.Thickness}\"");
            }
            else
            {
                builder.Append(" stroke=\"none\"");
            }

            if (brush != Brush.None)
            {
                builder.Append($" fill=\"{brush.Colour.ToHexCode()}\"");
            }
            else
            {
                builder.Append(" fill=\"none\"");
            }
        }
    }
}