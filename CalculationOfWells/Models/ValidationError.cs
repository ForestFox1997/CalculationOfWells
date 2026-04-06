namespace CalculationOfWells.Models
{
    /// <summary>
    /// Сведения и валидации значения параметров скважины
    /// </summary>
    public class ValidationError
    {
        public int LineNumber { get; set; }

        public string? WellId { get; set; }

        public string? Message { get; set; }

        public override string ToString() => $"{LineNumber}|{WellId}|{Message}";
    }
}
