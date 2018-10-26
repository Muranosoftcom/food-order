using Core;

namespace BusinessLogic.SpreadsheetParsing
{
    public class ParsingResult
    {
        public string Category { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Week Day { get; set; }
    }
}