namespace CalculationOfWells.Models.DTO
{
    /// <summary>
    /// Предоставляет набор параметров, импортированных из файла
    /// </summary>
    public class ImportResult
    {
        public IList<WellSummary> Summaries = [];

        public IList<ValidationError> Errors = [];
    }
}
