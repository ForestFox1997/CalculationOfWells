using CalculationOfWells.Models;

namespace CalculationOfWells.Services.Interfaces
{
    /// <summary>
    /// Предоставляет возможности агрегации (расчёта) параметров скважин
    /// </summary>
    public interface IAggregationService
    {
        WellSummary Summarize(Well well);

        List<WellSummary> SummarizeAll(IEnumerable<Well> wells);
    }
}
