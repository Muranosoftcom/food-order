using System;
using System.Text;

namespace SpreadsheetIntegration.Core {
	public struct Column : IComparable<Column> {
		private const int MaxColumn = 16384;

		private bool Equals(Column other) {
			return CompareTo(other) == 0;
		}

		public override bool Equals(object obj) {
			if (ReferenceEquals(null, obj)) {
				return false;
			}
			return obj.GetType() == GetType() && Equals((Column)obj);
		}

		public override int GetHashCode() {
			return (_column != null ? StringComparer.InvariantCultureIgnoreCase.GetHashCode(_column) : 0);
		}

		public override string ToString() => _column;

		private readonly string _column;

		public static Column Default { get; } = FromNumber(1);
		public static Column Max { get; } = FromNumber(MaxColumn);

		public Column(string column) {
			_column = column;
		}

		public int CompareTo(Column other) {
			return ToNumber(_column).CompareTo(ToNumber(other._column));
		}

		public static int ToNumber(string columnName) {
			columnName = columnName.ToUpperInvariant();

			int sum = 0;

			foreach (char t in columnName) {
				sum *= 26;
				sum += (t - 'A' + 1);
			}

			return sum;
		}

		public static Column FromNumber(int number) {
			var value = new StringBuilder();

			int val = number - 1;

			do {
				value.Insert(0, Convert.ToChar(65 + val % 26));
				val = (val - val % 26) / 26 - 1;
			} while (val >= 0);

			return new Column(value.ToString());
		}

		public static Column operator +(Column left, Column right) => new Column(FromNumber(ToNumber(left._column) + ToNumber(right._column)));

		public static bool operator >(Column left, Column right) => left.CompareTo(right) > 0;

		public static bool operator <(Column left, Column right) => left.CompareTo(right) < 0;

		public static explicit operator Column(string column) => new Column(column);

		public static implicit operator string(Column column) => column._column;
	}
}