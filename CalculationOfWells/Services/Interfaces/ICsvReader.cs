namespace CalculationOfWells.Services.Interfaces
{
    /// <summary>
    /// Представляет возможности чтения из CSV файла с возвращением данных строки и её номера
    /// </summary>
    public interface ICsvReader
    {
        /// <summary>
        /// Возвращает номер строки и данные о параметрах скважины из неё
        /// </summary>
        IAsyncEnumerable<(int lineNumber, string line)> ReadLinesAsync(string path, CancellationToken token = default);
    }
}
