using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTOs
{

    public class SupplierDto
    {
        [Required]
        public int SupplierId { get; set; }        
        public string SupplierName { get; set; }
        [Required]
        public CategoryDto[] Categories { get; set; }
    }
}