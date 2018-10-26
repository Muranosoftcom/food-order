using System.Collections.Generic;
using SpreadsheetIntegration.Core;

namespace BusinessLogic.SpreadsheetParsing
{
    public interface IParsingStrategy
    {
        IEnumerable<ParsingResult> ExtractFood(ValuesRange valuesRange);
    }
}