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

		public GoogleSpreadsheetProvider(SheetsService clientService) {
			_clientService = clientService;
		}

		public ValuesRange Get(string sheetId, SpreadsheetGetRequest getRequest) {
			(CellCoordinate cellCoordinate, _) = CellCoordinate.ParseRange(getRequest.CellsRange);

			SpreadsheetsResource.ValuesResource.GetRequest request =
				_clientService.Spreadsheets.Values.Get(sheetId, $"{getRequest.Sheet}!{getRequest.CellsRange}");
			request.ValueRenderOption = SpreadsheetsResource.ValuesResource.GetRequest.ValueRenderOptionEnum.UNFORMATTEDVALUE;
			ValueRange response = request.Execute();
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

		public void Update(string sheetId, SpreadsheetUpdateRequest updateRequest) {
			IList<IList<object>> updateData = updateRequest.RequestData.AsEnumerable(replaceEmpty: true).Select(x => (IList<object>)x.Select(i => i.Value).ToList()).ToList();

			var vr = new ValueRange {
				Values = updateData
			};

			SpreadsheetsResource.ValuesResource.UpdateRequest request = 
				_clientService.Spreadsheets.Values.Update(vr, sheetId, $"{updateRequest.Sheet}!{updateRequest.RequestData.MinCell}:{updateRequest.RequestData.MaxCell}");
			request.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
			request.Execute();
		}

		public ValuesRange UpdateAndGet(string sheetId, SpreadsheetGetRequest getRequest, SpreadsheetUpdateRequest updateRequest) {
			Update(sheetId, updateRequest);
			return Get(sheetId, getRequest);
		}

		public async Task UpdateAsync(string sheetId, SpreadsheetUpdateRequest updateRequest, CancellationToken cancellationToken) {
			IList<IList<object>> updateData = updateRequest.RequestData.AsEnumerable(replaceEmpty: true).Select(x => (IList<object>)x.Select(i => i.Value).ToList()).ToList();

			var vr = new ValueRange {
				Values = updateData
			};

			SpreadsheetsResource.ValuesResource.UpdateRequest request = 
				_clientService.Spreadsheets.Values.Update(vr, sheetId, $"{updateRequest.Sheet}!{updateRequest.RequestData.MinCell}:{updateRequest.RequestData.MaxCell}");
			request.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
			await request.ExecuteAsync(cancellationToken);
		}
	}
}