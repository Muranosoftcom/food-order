namespace BusinessLogic.DTOs
{

    public class SupplierDto
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public CategoryDto[] Categories { get; set; }
    }
}