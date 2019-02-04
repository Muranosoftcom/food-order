﻿using FoodOrder.SpreadsheetIntegration.Core;

namespace FoodOrder.SpreadsheetIntegration {
	public interface ISpreadsheetProvider {
		ValuesRange Get(string sheetId, SpreadsheetGetRequest getRequest);
		void Update(string sheetId, SpreadsheetUpdateRequest updateRequest);
		ValuesRange UpdateAndGet(string sheetId, SpreadsheetGetRequest getRequest, SpreadsheetUpdateRequest updateRequest);
	}
}