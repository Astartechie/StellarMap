namespace StellarMap.Application.Rendering;

public interface IRenderer<in T>
{
    public void Render(T input);
}