using System.Threading;
using System.Threading.Tasks;
using FoodOrder.SpreadsheetIntegration.Core;

namespace FoodOrder.SpreadsheetIntegration {
	public interface IAsyncSpreadsheetProvider {
		Task<ValuesRange> GetAsync(string sheetId, SpreadsheetGetRequest updateRequest, CancellationToken cancellationToken);
	}
}