using Core;

namespace BusinessLogic.DTOs
{
    public class WeekDayDto
    {
        public string WeekDay { get; set; }
        public SupplierDto[] Suppliers { get; set; }
    }
}