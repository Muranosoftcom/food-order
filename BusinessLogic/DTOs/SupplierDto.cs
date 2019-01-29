using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTOs {
    public class SupplierDto {
        [Required] 
        public int SupplierId { get; set; }

        public string SupplierName { get; set; }

        public bool CanMultiSelect { get; set; } = true;
        
        public decimal AvailableMoneyToOrder { get; set; } = 51;

        [Required] 
        public CategoryDto[] Categories { get; set; }
    }
}