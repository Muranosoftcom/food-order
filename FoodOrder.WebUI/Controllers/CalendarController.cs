using System;
using System.Collections.Generic;
using System.Linq;
using FoodOrder.BusinessLogic.Services;
using FoodOrder.Common.Extensions;
using FoodOrder.Domain.Entities;
using FoodOrder.WebUI.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrder.WebUI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : Controller {
        private readonly ICalendarService _calendarService;

        public CalendarController(ICalendarService calendarService) {
            _calendarService = calendarService;
        }
        
        [HttpGet("next-two-weeks")]
        public DayDto[] NextTwoWeeks() {
            var today = DateTime.UtcNow;
            IEnumerable<Day> holidays = _calendarService.GetAllHolidays();
            DayDto[] dates = Enumerable.Range(0, 13)
                .Select(dayNumber => {
                    var day = today.AddDays(dayNumber);

                    return new DayDto {
                        Date = day.Date,
                        IsHoliday = holidays.FirstOrDefault(d => d.Date.PrettifyDate() == day.Date.PrettifyDate()) != null
                    };

                })
                .ToArray();
            
            return dates;
        }
        
        [HttpGet("holidays")]
        public DayDto[] Holidays() {
            return _calendarService.GetAllHolidays()
                .Select(day => new DayDto {
                    Date = day.Date,
                    IsHoliday = day.IsHoliday,
                })
                .ToArray();
        }
    }
}