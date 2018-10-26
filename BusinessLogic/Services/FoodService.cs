using System;
using System.Threading;
using System.Threading.Tasks;
using BusinessLogic.DTOs;
using Domain.Repositories;
using SpreadsheetIntegration;
using SpreadsheetIntegration.Core;

namespace BusinessLogic.Services
{
    public class FoodService
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IAsyncSpreadsheetProvider _spreadsheetProvider;

        public FoodService(IFoodRepository foodRepository, IAsyncSpreadsheetProvider spreadsheetProvider)
        {
            _foodRepository = foodRepository;
            _spreadsheetProvider = spreadsheetProvider;
        }

        public async Task SynchronizeFood()
        {
            ValuesRange glagolFood = await _spreadsheetProvider.GetAsync(
                "1ik4DnYQL3hCbxF4PRmmeaGhcTXYW4BcCbGvtijoK_rk",
                new SpreadsheetGetRequest("Menu", "A2:F30"), CancellationToken.None);

            ValuesRange kafeFood = await _spreadsheetProvider.GetAsync("1sbbFJqa-X4KW91EmPyO9NTbP8bbuwg3szCSC3FTeR2c",
                new SpreadsheetGetRequest("Menu", "A1:E100"), CancellationToken.None);
            
            
            
        }
    }
}