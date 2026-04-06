using CalculationOfWells.Models.DTO;

namespace CalculationOfWells.Services.Interfaces
{
    /// <summary>
    /// Предоставляет возможности импорта данных из файла
    /// </summary>
    public interface IWellImportService
    {
        Task<ICollection<(int Line, ParsedRow? Row, string? ParseError)>> ReadAllAsync(
            string path, CancellationToken token = default);
    }
}
