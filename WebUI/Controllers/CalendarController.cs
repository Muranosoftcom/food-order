using System;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : Controller {
        [HttpGet]
        [Route("next-two-weeks")]
        public IActionResult NextTwoWeeks() {
            var today = DateTime.UtcNow;
            var dates = Enumerable.Range(0, 13)
                .Select(x => {
                    var day = today.AddDays(x);
                    var dayOfWeek = day.DayOfWeek;

                    return new {
                        shortDate = day.Date.ToString("d", CultureInfo.InvariantCulture),
                        isHoliday = dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday
                    };

                })
                .ToList();
            
            return Json(dates);
        }
    }
}