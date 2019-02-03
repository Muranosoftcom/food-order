using System;
using Core;

namespace WebUI.Dto {
    public class DayDto {
        public DateTime Date { get; set; }
        public string ShortDate => Date.PrettifyDate();
        public bool IsHoliday { get; set; }
    }
}