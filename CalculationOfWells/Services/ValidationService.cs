using CalculationOfWells.Models.Domain;
using CalculationOfWells.Models.DTO;
using CalculationOfWells.Services.Interfaces;

namespace CalculationOfWells.Services
{
    public class ValidationService : IValidationService
    {
        public (List<ValidationError> Errors, List<Well> Wells) ValidateAndBuildWells(
            IEnumerable<(int Line, ParsedRow Row)> rows)
        {
            var errors = new List<ValidationError>();
            var byWell = new Dictionary<string, (double X, double Y, List<(int Line, Interval Interval)>)>(
                StringComparer.OrdinalIgnoreCase);

            foreach (var (line, row) in rows)
            {
                if (string.IsNullOrWhiteSpace(row.WellId))
                {
                    errors.Add(new ValidationError {
                        LineNumber = line, WellId = row.WellId, Message = "WellId был пуст" });
                    continue;
                }
                if (row.DepthFrom < 0)
                {
                    errors.Add(new ValidationError {
                        LineNumber = line, WellId = row.WellId, Message = "DepthFrom был меньше 0" });
                }
                if (!(row.DepthFrom < row.DepthTo))
                {
                    errors.Add(new ValidationError {
                        LineNumber = line, WellId = row.WellId, Message = "DepthFrom больше, или равен DepthTo" });
                }
                if (row.Porosity < 0 || row.Porosity > 1) 
                {
                    errors.Add(new ValidationError {
                        LineNumber = line, WellId = row.WellId, Message = "Porosity вне значения от 0 до 1" });
                }
                if (string.IsNullOrWhiteSpace(row.Rock)) 
                {
                    errors.Add(new ValidationError{ LineNumber = line, WellId = row.WellId, Message = "Rock был пуст" });
                }

                if (!byWell.TryGetValue(row.WellId, out var entry))
                {
                    entry = (row.X, row.Y, new List<(int, Interval)>());
                    byWell[row.WellId] = entry;
                }
                else if (!AreClose(entry.X, row.X) || !AreClose(entry.Y, row.Y))
                {
                    errors.Add(new ValidationError
                    {
                        LineNumber = line,
                        WellId = row.WellId,
                        Message = $"Несовпадающие координаты ({entry.X},{entry.Y}) != ({row.X},{row.Y})"
                    });
                }

                entry.Item3.Add((line, new Interval {
                    DepthFrom = row.DepthFrom, DepthTo = row.DepthTo, Rock = row.Rock, Porosity = row.Porosity }));
                byWell[row.WellId] = entry;
            }

            var wells = new List<Well>();
            foreach (var kv in byWell)
            {
                var list = kv.Value.Item3.OrderBy(i => i.Interval.DepthFrom).ToList();
                for (int i = 1; i < list.Count; i++)
                {
                    var prev = list[i - 1].Interval;
                    var cur = list[i].Interval;
                    if (cur.DepthFrom < prev.DepthTo)
                    {
                        errors.Add(new ValidationError
                        {
                            LineNumber = list[i].Line,
                            WellId = kv.Key,
                            Message = $"Пересечение: {cur.DepthFrom}-{cur.DepthTo} с {prev.DepthFrom}-{prev.DepthTo}"
                        });
                    }
                }

                var well = new Well { WellId = kv.Key, X = kv.Value.X, Y = kv.Value.Y };
                well.Intervals.AddRange(list.Select(t => t.Interval));
                wells.Add(well);
            }

            return (errors, wells);
        }

        private static bool AreClose(double a, double b, double tolerance = 0.000001)
        {
            double difference = Math.Abs(a - b);
            return difference <= tolerance;
        }

        public List<ValidationError> ValidateRows(IEnumerable<(int Line, ParsedRow Row)> rows)
        {
            var errors = new List<ValidationError>();
            foreach (var (line, row) in rows)
            {
                if (string.IsNullOrWhiteSpace(row.WellId))
                {
                    errors.Add(
                        new ValidationError { LineNumber = line, WellId = row.WellId, Message = "WellId пустой" });
                }
                if (row.DepthFrom < 0)
                {
                    errors.Add(
                        new ValidationError { LineNumber = line, WellId = row.WellId, Message = "DepthFrom < 0" });
                }
                if (!(row.DepthFrom < row.DepthTo))
                {
                    errors.Add(
                        new ValidationError {
                            LineNumber = line, WellId = row.WellId, Message = "DepthFrom >= DepthTo" });
                }
                if (row.Porosity < 0 || row.Porosity > 1)
                {
                    errors.Add(
                        new ValidationError {
                            LineNumber = line, WellId = row.WellId, Message = "Porosity вне [0..1]" });
                }
                if (string.IsNullOrWhiteSpace(row.Rock))
                {
                    errors.Add(
                        new ValidationError { LineNumber = line, WellId = row.WellId, Message = "Rock пустой" });
                }
            }
            return errors;
        }
    }
}
