namespace CalculationOfWells.Models.DTO
{
    /// <summary>
    /// Параметры скважины из строки файла
    /// </summary>
    public class ParsedRow
    {
        public required string WellId { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public double DepthFrom { get; set; }

        public double DepthTo { get; set; }

        public required string Rock { get; set; }

        public double Porosity { get; set; }
    }
}
