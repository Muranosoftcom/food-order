using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrder.Domain.Entities {
	public class Supplier : Entity {
		[StringLength(20)] public string Name { get; set; }

		public bool CanMultiSelect { get; set; }

		[Column(TypeName = "Money")] public decimal AvailableMoneyToOrder { get; set; }
	}
}