using BusinessLogic.DTOs;

namespace WebUI.Dto {
    public class DayMenuDto {
        public string ShortDate { get; set; }
        public SupplierDto[] Suppliers { get; set; }
    }
}