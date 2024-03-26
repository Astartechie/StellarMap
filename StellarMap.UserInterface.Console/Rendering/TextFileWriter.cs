namespace StellarMap.UserInterface.Console.Rendering
{
    internal class TextFileWriter(string path) : IWriter
    {
        public void WriteLine(string line)
            => _lines.Add(line);

        public void Save()
        {
            File.WriteAllLines(path, _lines);
        }

        private readonly List<string> _lines = [];
    }
}