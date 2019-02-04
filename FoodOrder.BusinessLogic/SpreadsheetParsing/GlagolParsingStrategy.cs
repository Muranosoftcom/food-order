using System.Collections.Generic;
using System.Linq;
using FoodOrder.Domain.Enumerations;
using FoodOrder.SpreadsheetIntegration.Core;

namespace FoodOrder.BusinessLogic.SpreadsheetParsing
{
    public class GlagolParsingStrategy : IParsingStrategy
    {
        public IEnumerable<ParsingResult> ExtractFood(ValuesRange valuesRange)
        {
            string lastCategory = string.Empty;
            foreach (var row in valuesRange.AsEnumerable())
            {
                lastCategory = string.IsNullOrEmpty(row.First().Value)
                    ? lastCategory
                    : row.First().Value;
                foreach (var cell in row.Skip(1).Where(x => !string.IsNullOrEmpty(x.Value))
                    .Select((x, index) => new {x, index = index + 1}))
                {
                    yield return new ParsingResult
                    {
                        Category = lastCategory,
                        Name = cell.x.Value,
                        Price = 0,
                        Day = (Week) cell.index
                    };
                }
            }
        }
    }
}