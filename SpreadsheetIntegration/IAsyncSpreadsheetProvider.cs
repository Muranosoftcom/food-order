using System.Threading;
using System.Threading.Tasks;
using SpreadsheetIntegration.Core;

namespace SpreadsheetIntegration {
	public interface IAsyncSpreadsheetProvider {
		Task<ValuesRange> GetAsync(string sheetId, SpreadsheetGetRequest updateRequest, CancellationToken cancellationToken);
	}
}