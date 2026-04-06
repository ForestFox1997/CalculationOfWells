namespace CalculationOfWells.Models.Domain
{
    /// <summary>
    /// Скважина (параметры скважины)
    /// </summary>
    public class Well
    {
        /// <summary>
        /// Идентификатор скважины
        /// </summary>
        public required string WellId { get; set; }

        /// <summary>
        /// Координата оси X
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Координата оси Y
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Интервалы
        /// </summary>
        public List<Interval> Intervals { get; } = [];
    }
}
