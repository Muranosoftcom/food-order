using System;

namespace FoodOrder.BusinessLogic.DTOs {
	public class SupplierDto {
		public Guid SupplierId { get; set; }

		public string SupplierName { get; set; }

		public bool CanMultiSelect { get; set; } = true;

		public decimal AvailableMoneyToOrder { get; set; } = 68;
		public int Position { get; set; }

		public CategoryDto[] Categories { get; set; }
	}
}