using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodOrder.Domain.Entities {
	public class User : Entity {
		public string UserName { get; set; }
		[EmailAddress] 
		public string Email { get; set; }
		public bool IsAdmin { get; set; }
		public bool IsDisabled { get; set; }
		public ICollection<Order> Orders { get; set; }
	}
}