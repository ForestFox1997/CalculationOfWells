using System.Globalization;
using CsvHelper.Configuration;
using CalculationOfWells.Models;

namespace CalculationOfWells.Configuration
{
    /// <summary>
    /// Конфигурация CSV ридера
    /// </summary>
    public static class CsvReaderConfig
    {
        public sealed class CsvRowMap : ClassMap<ParsedRow>
        {
            public CsvRowMap()
            {
                Map(m => m.WellId).Index(0);
                Map(m => m.X).Index(1).TypeConverterOption.CultureInfo(CultureInfo.InvariantCulture);
                Map(m => m.Y).Index(2).TypeConverterOption.CultureInfo(CultureInfo.InvariantCulture);
                Map(m => m.DepthFrom).Index(3).TypeConverterOption.CultureInfo(CultureInfo.InvariantCulture);
                Map(m => m.DepthTo).Index(4).TypeConverterOption.CultureInfo(CultureInfo.InvariantCulture);
                Map(m => m.Rock).Index(5);
                Map(m => m.Porosity).Index(6).TypeConverterOption.CultureInfo(CultureInfo.InvariantCulture);
            }
        }

        public readonly static CsvConfiguration CsvConfig = new(CultureInfo.InvariantCulture)
        {
            Delimiter = ";",
            HasHeaderRecord = false,
            BadDataFound = null,
            MissingFieldFound = null,
            IgnoreBlankLines = true,
            TrimOptions = TrimOptions.Trim
        };
    }
}
