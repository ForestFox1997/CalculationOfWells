namespace CalculationOfWells.Models
{
    public class WellSummary
    {
        public required string WellId { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public double TotalDepth { get; set; }

        public int IntervalCount { get; set; }

        public double WeightedPorosity { get; set; }

        public required string TopRock { get; set; }
    }
}
