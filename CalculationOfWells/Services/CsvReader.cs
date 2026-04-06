using System.IO;
using CalculationOfWells.Services.Interfaces;

namespace CalculationOfWells.Services
{
    public class CsvReader : ICsvReader
    {
        public async IAsyncEnumerable<(int lineNumber, string line)> ReadLinesAsync(
            string path, CancellationToken token = default)
        {
            using var streamReader = new StreamReader(path);
            int line = 0;
            while (!streamReader.EndOfStream)
            {
                var text = await streamReader.ReadLineAsync(token);
                line++;
                if (text == null)
                {
                    continue;
                }

                yield return (line, text);
            }
        }
    }
}
