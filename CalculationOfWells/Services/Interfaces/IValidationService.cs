using CalculationOfWells.Models;

namespace CalculationOfWells.Services.Interfaces
{
    /// <summary>
    /// Предоставляет возможности валидации параметров скважин
    /// </summary>
    public interface IValidationService
    {
        List<ValidationError> ValidateRows(IEnumerable<(int Line, ParsedRow Row)> rows);

        (List<ValidationError> Errors, List<Well> Wells) ValidateAndBuildWells(
            IEnumerable<(int Line, ParsedRow Row)> rows);
    }
}
