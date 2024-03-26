namespace StellarMap.Application.Rendering;

public interface IRenderTarget
{
    IGraphics Start(int width, int height);
    void End();
}