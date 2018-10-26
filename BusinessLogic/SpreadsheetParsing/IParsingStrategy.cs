using BusinessLogic.DTOs;
using SpreadsheetIntegration.Core;

namespace BusinessLogic.SpreadsheetParsing
{
    public interface IParsingStrategy
    {
        FoodDTO[] ExtractFood(ValuesRange valuesRange);
    }
}