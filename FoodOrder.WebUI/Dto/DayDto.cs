using System;
using FoodOrder.Common.Extensions;

namespace FoodOrder.WebUI.Dto {
    public class DayDto {
        public DateTime Date { get; set; }
        public bool IsHoliday { get; set; }
        
        public string ShortDate => Date.PrettifyDate();
    }
}