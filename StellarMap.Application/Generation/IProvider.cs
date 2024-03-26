namespace StellarMap.Application.Generation;

public interface IProvider<out T>
{
    T Provide();
}