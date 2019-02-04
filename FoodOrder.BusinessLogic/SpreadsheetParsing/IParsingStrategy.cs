using System.Collections.Generic;
using FoodOrder.SpreadsheetIntegration.Core;

namespace FoodOrder.BusinessLogic.SpreadsheetParsing
{
    public interface IParsingStrategy
    {
        IEnumerable<ParsingResult> ExtractFood(ValuesRange valuesRange);
    }
}