using CalculationOfWells.Models;

namespace CalculationOfWells.Services.Interfaces
{
    /// <summary>
    /// Предоставляет возможности импорта данных из файла
    /// </summary>
    public interface IImportService
    {
        Task<ICollection<(int Line, ParsedRow? Row, string? ParseError)>> ReadAllAsync(
            string path, CancellationToken token = default);
    }
}
