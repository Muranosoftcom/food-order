using FoodOrder.Common;
using FoodOrder.Domain.Enumerations;

namespace FoodOrder.BusinessLogic.SpreadsheetParsing
{
    public class ParsingResult
    {
        public string Category { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DayOfWeek Day { get; set; }
    }
}