using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using SpreadsheetIntegration.Core;

namespace SpreadsheetIntegration.Google {
	public class GoogleSpreadsheetProvider : IGoogleSpreadsheetProvider {
		private readonly SheetsService _clientService;

		public GoogleSpreadsheetProvider() {
			_clientService = GoogleClientServiceProvider.GetService();
		}

		public async Task<ValuesRange> GetAsync(string sheetId, SpreadsheetGetRequest getRequest, CancellationToken cancellationToken) {
			(CellCoordinate cellCoordinate, _) = CellCoordinate.ParseRange(getRequest.CellsRange);

			SpreadsheetsResource.ValuesResource.GetRequest request =
				_clientService.Spreadsheets.Values.Get(sheetId, $"{getRequest.Sheet}!{getRequest.CellsRange}");
			request.ValueRenderOption = SpreadsheetsResource.ValuesResource.GetRequest.ValueRenderOptionEnum.UNFORMATTEDVALUE;
			ValueRange response = await request.ExecuteAsync(cancellationToken);
			IList<IList<object>> values = response.Values;

			IEnumerable<Cell> cells = values.Select((x, row) => x.Select((y, coll) => new Cell {
				Coordinate = new CellCoordinate {
					Row = row + cellCoordinate.Row,
					Column = Column.FromNumber(coll) + cellCoordinate.Column,
				},
				Value = y as string
			})).SelectMany(x => x);

			var result = new ValuesRange(cells);

			return result;
		}
	}
}