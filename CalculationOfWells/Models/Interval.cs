namespace CalculationOfWells.Models
{
    /// <summary>
    /// Интервал (параметры) скважины
    /// </summary>
    public class Interval
    {
        /// <summary>
        /// Глубина: от
        /// </summary>
        public double DepthFrom { get; set; }

        /// <summary>
        /// Глубина: до
        /// </summary>
        public double DepthTo { get; set; }

        /// <summary>
        /// Тип породы
        /// </summary>
        public required string Rock { get; set; }

        /// <summary>
        /// Пористость
        /// </summary>
        public double Porosity { get; set; }

        /// <summary>
        /// Разница между наименьшей и наибольшей глубиной скважины
        /// </summary>
        public double DepthDifference => DepthTo - DepthFrom;
    }
}
