using StellarMap.Application.Rendering;

namespace StellarMap.UserInterface.Console.Rendering.ScalableVectorGraphics;

internal class RenderTarget(IWriter writer) : IRenderTarget
{
    public IGraphics Start(int width, int height)
    {
        _writer.WriteLine($"<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 {width} {height} \">");
        return new Graphics(_writer);
    }

    public void End()
    {
        _writer.WriteLine("</svg>");
        _writer.Save();
    }

    private readonly IWriter _writer = writer;
}