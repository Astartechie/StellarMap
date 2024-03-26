using StellarMap.Application.Rendering;

namespace StellarMap.UserInterface.Console.Rendering.ScalableVectorGraphics;

internal class RenderTarget(IWriter writer) : IRenderTarget
{
    public IGraphics Start(int width, int height)
    {
        writer.WriteLine($"<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"{width}\" height=\"{height}\" viewBox=\"0 0 {width} {height} \">");
        return new Graphics(writer);
    }

    public void End()
    {
        writer.WriteLine("</svg>");
        writer.Save();
    }
}