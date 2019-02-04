using System.ComponentModel.DataAnnotations;

namespace FoodOrder.Domain.Entities {
	public class User : Entity {
		[StringLength(256)] public string UserName { get; set; }
		[EmailAddress] public string Email { get; set; }
		public bool IsAdmin { get; set; }
	}
}