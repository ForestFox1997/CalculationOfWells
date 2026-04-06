using System.IO;
using CsvHelper;
using CalculationOfWells.Services.Interfaces;
using static CalculationOfWells.Configuration.CsvReaderConfig;
using CalculationOfWells.Models.DTO;

namespace CalculationOfWells.Services
{
    public class WellImportService : IWellImportService
    {
        public async Task<ICollection<(int Line, ParsedRow? Row, string? ParseError)>> ReadAllAsync(
            string path, CancellationToken token = default)
        {
            var result = new List<(int, ParsedRow?, string?)>();
            await using var fileStream = File.OpenRead(path);
            using var streamReader = new StreamReader(fileStream);
            using var reader = new CsvHelper.CsvReader(streamReader, CsvConfig);
            reader.Context.RegisterClassMap<CsvRowMap>();

            while (await reader.ReadAsync().ConfigureAwait(false))
            {
                //ct.ThrowIfCancellationRequested();
                var lineNumber = reader.Context.Parser!.Row;
                try
                {
                    var rec = reader.GetRecord<ParsedRow>();
                    var parsed = new ParsedRow
                    {
                        WellId = rec.WellId ?? "",
                        X = rec.X, Y = rec.Y,
                        DepthFrom = rec.DepthFrom,
                        DepthTo = rec.DepthTo,
                        Rock = rec.Rock ?? "",
                        Porosity = rec.Porosity 
                    };
                    result.Add((lineNumber, parsed, null));
                }
                catch (CsvHelperException ex)
                {
                    result.Add((lineNumber, null, "Не удалось провести парсинг строки"));
                }
                catch (Exception ex)
                {
                    result.Add((lineNumber, null, ex.Message));
                }
            }

            return result;
        }

    }
}