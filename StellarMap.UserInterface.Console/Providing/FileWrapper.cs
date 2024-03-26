namespace StellarMap.UserInterface.Console.Providing;

internal class FileWrapper<T>(string path) : IReader<T>
{
    public IEnumerable<string> ReadLines()
        => File.ReadLines(path);
}