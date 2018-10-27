namespace BusinessLogic.DTOs
{
    public class WeekDayDto
    {
        public string WeekDay { get; set; }
        public string UserName { get; set; }
        public SupplierDto[] Suppliers { get; set; }
    }
}