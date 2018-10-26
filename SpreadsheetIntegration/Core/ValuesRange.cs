using System.Collections.Generic;
using System.Linq;

namespace SpreadsheetIntegration.Core {
	public class ValuesRange {
		private readonly Dictionary<CellCoordinate, Cell> _store = new Dictionary<CellCoordinate, Cell>();

		public CellCoordinate MaxCell { get; private set; } = CellCoordinate.Min;
		public CellCoordinate MinCell { get; private set; } = CellCoordinate.Max;

		public ValuesRange() { }

		public ValuesRange(IEnumerable<Cell> range) {
			AddRange(range);
		}

		public void Add(Cell cell) {
			if (_store.TryGetValue(cell.Coordinate, out Cell item)) {
				item.Value = cell.Value; //update
			} else {
				_store.Add(cell.Coordinate, cell); //insert
			}

			UpdateMinMax(cell);
		}

		private void UpdateMinMax(Cell cell) {
			int minRow = MinCell.Row < cell.Coordinate.Row ? MinCell.Row : cell.Coordinate.Row;
			int maxRow = MaxCell.Row > cell.Coordinate.Row ? MaxCell.Row : cell.Coordinate.Row;

			Column minColl = MinCell.Column < cell.Coordinate.Column ? MinCell.Column : cell.Coordinate.Column;
			Column maxColl = MaxCell.Column > cell.Coordinate.Column ? MaxCell.Column : cell.Coordinate.Column;

			MaxCell = new CellCoordinate(maxRow, maxColl);
			MinCell = new CellCoordinate(minRow, minColl);
		}

		public void AddRange(IEnumerable<Cell> range) {
			foreach (Cell cell in range) {
				Add(cell);
			}
		}

		public IEnumerable<IEnumerable<Cell>> AsEnumerable(bool replaceEmpty = false) => GetRows(MinCell, MaxCell, replaceEmpty);

		public object this[CellCoordinate coordinate] => _store.GetValueOrDefault(coordinate).Value;
		public object this[string coordinate] => _store.GetValueOrDefault(CellCoordinate.Parse(coordinate)).Value;

		public ValuesRange GetSubRange(string range) {
			(CellCoordinate from, CellCoordinate to) = CellCoordinate.ParseRange(range);

			IEnumerable<Cell> elems = GetRows(@from, to).SelectMany(x => x);
			return new ValuesRange(elems);
		}

		private IEnumerable<IEnumerable<Cell>> GetRows(CellCoordinate @from, CellCoordinate to, bool replaceEmpty = false) {
			for (int i = from.Row; i <= to.Row; i++) {
				yield return GetRow(i, from.Column, to.Column, replaceEmpty);
			}
		}

		private IEnumerable<Cell> GetRow(int rowIndex, Column @from, Column to, bool replaceEmpty = false) {
			for (int i = Column.ToNumber(from); i <= Column.ToNumber(to); i++) {
				var coordinate = new CellCoordinate(rowIndex, Column.FromNumber(i));
				if (_store.TryGetValue(coordinate, out Cell item)) {
					yield return GetCell(item, replaceEmpty);
				} else {
					yield return new Cell {
						Coordinate = coordinate
					};
				}
			}
		}

		private Cell GetCell(Cell cell, bool replaceEmpty) => new Cell(cell.Coordinate, (replaceEmpty && cell.Value == null) ? string.Empty : cell.Value);
	}
}