namespace SpreadsheetIntegration.Core {
	public class SpreadsheetUpdateRequest {
		public SpreadsheetUpdateRequest(string sheet, ValuesRange requestData) {
			Sheet = sheet;
			RequestData = requestData;
		}

		public string Sheet { get; }
		public ValuesRange RequestData { get; }
	}
}