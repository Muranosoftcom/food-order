using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Core;
using SpreadsheetIntegration.Core;

namespace BusinessLogic.SpreadsheetParsing
{
    public class KafeParsingStrategy : IParsingStrategy
    {
        public IEnumerable<ParsingResult> ExtractFood(ValuesRange valuesRange)
        {
            string lastCategory = string.Empty;

            Regex rx = new Regex("^(.*?)(?<price>\\d{1,3})$");

            foreach (var row in valuesRange.AsEnumerable())
            {

                foreach (var cell in row.Where(x => !string.IsNullOrEmpty(x.Value))
                    .Select((x, index) => new {x, index = index + 1}))
                {
                    if (cell.x.Value.StartsWith("__"))
                    {
                        lastCategory = cell.x.Value.Trim('_');
                    }
                    else
                    {
                        yield return new ParsingResult
                        {
                            Category = lastCategory,
                            Name = cell.x.Value,
                            Price = decimal.TryParse(rx.Match(cell.x.Value).Groups["price"].Value, out decimal price) ? price : 0,
                            Day = (Week) cell.index
                        };
                    }
                }
            }
        }
    }
}