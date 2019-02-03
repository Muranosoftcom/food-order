using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.DTOs;
using BusinessLogic.Services;
using Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebUI.Dto;

namespace WebUI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : Controller {
        private readonly IFoodService _foodService;

        public MenuController(IFoodService foodService) {
            _foodService = foodService;
        }
        
        [HttpPost]
        [Authorize]
        [Route("sync")]
        public async Task<IActionResult> Sync() {
            string syncResult = "Синхронизация успешно завершена";
            
            try {
                await _foodService.SynchronizeFood();
            }
            catch {
                syncResult = "Ошибка при выполнении синхронизации";
            }

            return Ok(new {
                syncResult
            });
        }

        [HttpGet]
        [Authorize]
        [Route("week-menu")]
        public ActionResult<DayMenuDto[]> WeekMenu() {
            WeekMenuDto weekMenuDto = _foodService.GetWeekMenu();

            var daysOfWeek = WeekDays();
            
            return weekMenuDto.WeekDays.Select(wd => new DayMenuDto {
                ShortDate = ToShortDate(daysOfWeek, wd.WeekDay),
                Suppliers = wd.Suppliers,
            }).ToArray();
        }

        private string ToShortDate(DateTime[] daysOfWeek, string dayName) {
            return daysOfWeek
                .First(day => day.ToString("ddd") == dayName)
                .PrettifyDate();
        }

        private DateTime[] WeekDays() {
            var today = DateTime.Today;
            bool isForNextWeek = today.DayOfWeek >= DayOfWeek.Tuesday && today.DayOfWeek <= DayOfWeek.Sunday;
                
            DateTime startOfWeek = DateTime.Today.AddDays(
                (int) CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek - 
                (int) DateTime.Today.DayOfWeek + (isForNextWeek ? 7 : 0));

            return Enumerable
                .Range(0, 7)
                .Select(i => startOfWeek.AddDays(i))
                .ToArray();
        }
    }
}