namespace FoodOrder.SpreadsheetIntegration.Core {
	public struct Cell {
		public CellCoordinate Coordinate { get; set; }
		public string Value { get; set; }

		public Cell(CellCoordinate coordinate, string value) {
			Coordinate = coordinate;
			Value = value;
		}
	}
}