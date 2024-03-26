namespace StellarMap.UserInterface.Console.Providing;

internal interface IReader<T>
{
    IEnumerable<string> ReadLines();
}