using System.Threading;
using System.Threading.Tasks;
using SpreadsheetIntegration.Core;

namespace SpreadsheetIntegration {
	public interface IAsyncSpreadsheetProvider {
		Task UpdateAsync(string sheetId, SpreadsheetUpdateRequest updateRequest, CancellationToken cancellationToken);
	}
}