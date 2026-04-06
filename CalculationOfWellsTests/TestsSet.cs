using CalculationOfWells.Models;
using CalculationOfWells.Services;

namespace CalculationOfWellsTests
{
    public class TestsSet
    {
        // Тесты валидации
        [Fact]
        public void Validate_ShouldReturnError_WhenDepthFromGreaterThanDepthTo()
        {
            var service = new ValidationService();

            var row = new ParsedRow
            {
                WellId = "A-001",
                DepthFrom = 20,
                DepthTo = 10,
                Rock = "Sandstone",
                Porosity = 0.2
            };

            var result = service.ValidateRows(new List<(int, ParsedRow)> { new(0, row) });

            Assert.Contains("DepthFrom >= DepthTo", result[0].Message);
        }

        [Theory]
        [InlineData(-0.1)]
        [InlineData(1.5)]
        public void Validate_ShouldReturnError_WhenPorosityInvalid(double porosity)
        {
            var service = new ValidationService();

            var row = new ParsedRow
            {
                WellId = "A-001",
                DepthFrom = 0,
                DepthTo = 10,
                Rock = "Sandstone",
                Porosity = porosity
            };

            var result = service.ValidateRows(new List<(int, ParsedRow)> { new(0, row) });

            Assert.Contains("Porosity вне [0..1]", result[0].Message);
        }

        [Fact]
        public void Validate_ShouldReturnError_WhenRockEmpty()
        {
            var service = new ValidationService();

            var row = new ParsedRow
            {
                WellId = "A-001",
                DepthFrom = 0,
                DepthTo = 10,
                Rock = "",
                Porosity = 0.2
            };

            var result = service.ValidateRows(new List<(int, ParsedRow)> { new(0, row) });

            Assert.Contains("Rock пустой", result[0].Message);
        }

        // Тесты агрегации
        [Fact]
        public void Aggregate_ShouldCalculateTotalDepth()
        {
            var service = new AggregationService();

            var well = new Well
            {
                WellId = "A-001"
            };
            well.Intervals.AddRange([
                new() { DepthFrom = 0, DepthTo = 10, Rock = "Sandstone", Porosity = 0.1 },
                new() { DepthFrom = 10, DepthTo = 30, Rock = "Limestone", Porosity = 0.2 }]);

            var summary = service.Summarize(well);

            Assert.Equal(30, summary.TotalDepth);
        }

        [Fact]
        public void Aggregate_ShouldReturnCorrectIntervalCount()
        {
            var service = new AggregationService();

            var well = new Well
            {
                WellId = "A-001"
            };
            well.Intervals.AddRange([
                new() { DepthFrom = 0, DepthTo = 10, Rock = "Sandstone", Porosity = 0.1 },
                new() { DepthFrom = 10, DepthTo = 20, Rock = "Limestone", Porosity = 0.2 }]);

            var summary = service.Summarize(well);

            Assert.Equal(2, summary.IntervalCount);
        }

        [Fact]
        public void Aggregate_ShouldCalculateWeightedPorosity()
        {
            var service = new AggregationService();

            var well = new Well
            {
                WellId = "A-001"
            };
            well.Intervals.AddRange([
                new() { DepthFrom = 0, DepthTo = 10, Rock = "Sandstone", Porosity = 0.1 },
                new() { DepthFrom = 10, DepthTo = 30, Rock = "Limestone", Porosity = 0.2 }]);

            var summary = service.Summarize(well);

            var expected = (10 * 0.1 + 20 * 0.2) / 30;

            Assert.Equal(expected, summary.WeightedPorosity);
        }
    }
}