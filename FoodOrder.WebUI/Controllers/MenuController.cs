using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using FoodOrder.BusinessLogic.DTOs;
using FoodOrder.BusinessLogic.Services;
using FoodOrder.Common.Extensions;
using FoodOrder.WebUI.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrder.WebUI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : Controller {
        private readonly IFoodService _foodService;
        private readonly IWeekMenuService _menuService;

        public MenuController(IFoodService foodService, IWeekMenuService menuService) {
            _foodService = foodService;
            _menuService = menuService;
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
            DateTime[] nextWeekDays = GetNextWeekDays();
            WeekDayDto[] weekDaysMenu = _menuService.GetWeekDaysMenu();

            return weekDaysMenu.Select(dayMenu => new DayMenuDto {
                ShortDate = ToShortDate(nextWeekDays, dayMenu.DayOfWeek),
                Suppliers = dayMenu.Suppliers,
            }).ToArray();
        }

        private string ToShortDate(DateTime[] daysOfWeek, DayOfWeek dayOfWeek) {
            return daysOfWeek
                .First(day => day.DayOfWeek == dayOfWeek)
                .PrettifyDate();
        }

        private DateTime[] GetNextWeekDays() {
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