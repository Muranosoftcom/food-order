using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SpreadsheetIntegration.Core;
using SpreadsheetIntegration.Google;
using Xunit;

namespace Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var result = new GoogleSpreadsheetProvider(GoogleClientServiceProvider.GetService()).Get(
                "1sbbFJqa-X4KW91EmPyO9NTbP8bbuwg3szCSC3FTeR2c", new SpreadsheetGetRequest("Menu", "A1:E100"));

            List<object> objects = new List<object>();
            string lastCategory = string.Empty;

            Regex rx = new Regex("^(.*?)(?<price>\\d{1,3})$");

            foreach (var row in result.AsEnumerable())
            {

                foreach (var cell in row.Where(x => !string.IsNullOrEmpty(x.Value)).Select((x, index) => new {x, index}))
                {
                    if (cell.x.Value.StartsWith("__"))
                    {
                        lastCategory = cell.x.Value.Trim('_');
                    }
                    else
                    {
                        objects.Add(new
                        {
                            Category = lastCategory,
                            Name = cell.x.Value,
                            Price = rx.Match(cell.x.Value).Groups["price"].Value,
                            Day = 
                        });
                    }
                }
            }
        }
    }
}