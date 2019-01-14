using System;
using BusinessLogic.Utils;

namespace BusinessLogic.DTOs {
    public class DayDto {
        public DateTime Date { get; set; }
        public string ShortDate => Date.PretifyDate();
        public bool IsHoliday { get; set; }
    }
}