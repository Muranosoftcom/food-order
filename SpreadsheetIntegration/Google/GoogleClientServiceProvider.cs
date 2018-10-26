using System.IO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;

namespace SpreadsheetIntegration.Google {
	public static class GoogleClientServiceProvider {
		public static SheetsService GetService() {
			string[] scopes = { SheetsService.Scope.Spreadsheets };

			string ApplicationName = "Food ordering";

			ServiceAccountCredential credential;
			using (var stream = new FileStream("D:/Food ordering-9e07ded32cf7.json", FileMode.Open, FileAccess.Read)) {
				credential = GoogleCredential.FromStream(stream).CreateScoped(scopes).UnderlyingCredential as ServiceAccountCredential;
			}

			// Create Google Sheets API service.
			var service = new SheetsService(new BaseClientService.Initializer {
				HttpClientInitializer = credential,
				ApplicationName = ApplicationName,
			});

			return service;
		}
	}
}