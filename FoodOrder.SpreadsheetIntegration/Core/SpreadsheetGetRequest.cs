namespace FoodOrder.SpreadsheetIntegration.Core {
	public class SpreadsheetGetRequest {
		public SpreadsheetGetRequest(string sheet, string cellsRange) {
			Sheet = sheet;
			CellsRange = cellsRange;
		}

		public string Sheet { get; }
		public string CellsRange { get; }
	}
}