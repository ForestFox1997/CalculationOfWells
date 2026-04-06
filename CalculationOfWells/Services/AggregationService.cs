using CalculationOfWells.Models.Domain;
using CalculationOfWells.Models.DTO;
using CalculationOfWells.Services.Interfaces;

namespace CalculationOfWells.Services
{
    public class AggregationService : IAggregationService
    {
        public WellSummary Summarize(Well well)
        {
            var totalDepth = well.Intervals.Count != 0 ? well.Intervals.Max(i => i.DepthTo) : 0.0;
            var count = well.Intervals.Count;
            var depthDifference = well.Intervals.Sum(i => i.DepthDifference);
            var weightedPorosity =
                depthDifference > 0 ? well.Intervals.Sum(i => i.Porosity * i.DepthDifference) / depthDifference : 0.0;
            var topRock = well.Intervals
                .GroupBy(i => i.Rock)
                .Select(g => new { Rock = g.Key, Thickness = g.Sum(i => i.DepthDifference) })
                .OrderByDescending(g => g.Thickness)
                .FirstOrDefault()?.Rock ?? string.Empty;

            return new WellSummary
            {
                WellId = well.WellId,
                X = well.X,
                Y = well.Y,
                TotalDepth = totalDepth,
                IntervalCount = count,
                WeightedPorosity = weightedPorosity,
                TopRock = topRock
            };
        }

        public List<WellSummary> SummarizeAll(IEnumerable<Well> wells)
        {
            return wells.Select(Summarize).ToList();
        }
    }
}
