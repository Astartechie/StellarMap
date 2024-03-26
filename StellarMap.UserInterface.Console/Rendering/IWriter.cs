namespace StellarMap.UserInterface.Console.Rendering;

internal interface IWriter
{
    void WriteLine(string line);
    void Save();
}