namespace StellarMap.Application.Rendering;

public interface IGraphics
{
    void DrawCircle(Point center, float radius, Pen pen);
    void FillCircle(Point center, float radius, Brush brush);
    void DrawAndFillCircle(Point center, float radius, Pen pen, Brush brush);

    void DrawRectangle(int x, int y, int width, int height, Pen pen);
    void FillRectangle(int x, int y, int width, int height, Brush brush);
    void DrawAndFillRectangle(int x, int y, int width, int height, Pen pen, Brush brush);
    void DrawPolygon(IList<Point> points, Pen pen);
    void FillPolygon(IList<Point> points, Brush brush);
    void DrawAndFillPolygon(IList<Point> points, Pen pen, Brush brush);
}