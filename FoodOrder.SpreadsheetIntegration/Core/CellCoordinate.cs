using System;
using System.Text.RegularExpressions;

namespace FoodOrder.SpreadsheetIntegration.Core {
	public struct CellCoordinate {
		private const string Regex = "(?<column>[A-z]{1,2})(?<row>\\d{1,7})";
		private static readonly Regex SingleCellRegex = new Regex($"^(?<from>{Regex})$");
		private static readonly Regex RangeRegex = new Regex($"^(?<from>{Regex})(:(?<to>{Regex}))?$");

		public static CellCoordinate Min { get; } = new CellCoordinate {
			Column = Column.Default,
			Row = 1
		};

		public static CellCoordinate Max { get; } = new CellCoordinate {
			Column = Column.Max,
			Row = 1048576
		};

		public CellCoordinate(int row, Column column) {
			Row = row;
			Column = column;
		}

		public bool Equals(CellCoordinate other) {
			return Row == other.Row && Column.Equals(other.Column);
		}

		public override bool Equals(object obj) {
			if (ReferenceEquals(null, obj)) {
				return false;
			}
			return obj is CellCoordinate coordinate && Equals(coordinate);
		}

		public override int GetHashCode() {
			unchecked {
				return (Row * 397) ^ (Column != null ? Column.GetHashCode() : 0);
			}
		}

		public int Row { get; set; }
		public Column Column { get; set; }

		public override string ToString() => $"{Column.ToString()}{Row.ToString()}";

		public static CellCoordinate Parse(string coordinate) {
			Match match = SingleCellRegex.Match(coordinate);

			if (!match.Success) {
				throw new FormatException($"Unexpected cell coordinate {coordinate}");
			}

			return new CellCoordinate(int.Parse(match.Groups["row"].Value), new Column(match.Groups["column"].Value));
		}

		public static (CellCoordinate from, CellCoordinate to) ParseRange(string range) {
			Match matching = RangeRegex.Match(range);

			if (!matching.Success) {
				throw new FormatException($"Unexpected cell range coordinates {range}");
			}

			Match from = SingleCellRegex.Match(matching.Groups["from"].Value);
			Match to = SingleCellRegex.Match(matching.Groups["to"].Value);

			CellCoordinate fromCoordinate = Parse(from.Value);

			return (fromCoordinate, string.IsNullOrEmpty(to.Value) ? fromCoordinate : Parse(to.Value));
		}
	}
}