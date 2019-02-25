using System;
using System.Linq;
using FoodOrder.BusinessLogic.DTOs;
using FoodOrder.BusinessLogic.Services;
using FoodOrder.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrder.WebUI.Controllers.MenuEditor {
	[Route("api/menu-editor/[controller]")]
	[ApiController]
	public class SupplierController : Controller {
		private readonly IMenuEditorService _menuEditorService;

		public SupplierController(IMenuEditorService menuEditorService) {
			_menuEditorService = menuEditorService;
		}
        
		[HttpGet]
		public SupplierDto[] Get() {
			return _menuEditorService.GetAllSuppliers()
				.Select(s => new SupplierDto {
					SupplierId = s.Id,
					SupplierName = s.Name,
					CanMultiSelect = s.CanMultiSelect,
					AvailableMoneyToOrder = s.AvailableMoneyToOrder,
					Position = s.Position,
					Categories = s.Categories.Select(c => new CategoryDto {
						Id = c.Id,
						Position = c.Position,
						Name = c.Name,
						SupplierId = c.Supplier.Id,
						Dishes = c.DishItems.Select(d => new DishDto {
							Id = d.Id,
							Name = d.Name,
							Price = d.Price,
							CategoryId = d.Category.Id,
							AvailableAt = d.AvailableAt.Cast<int>().ToArray()
						}).OrderBy(d => d.Name).ToArray()
					}).ToArray(),
				}).ToArray();
		}
		
		[HttpPost]
		public IActionResult Post([FromBody] SupplierDto supplierDto) {
			var supplier = new Supplier {
				Name = supplierDto.SupplierName,
				CanMultiSelect = supplierDto.CanMultiSelect,
				AvailableMoneyToOrder = supplierDto.AvailableMoneyToOrder,
				Position = supplierDto.Position,
			};
	
			_menuEditorService.CreateSupplier(supplier);
			
			return Ok();
		}
		
		[HttpPut]
		public IActionResult Put([FromBody] SupplierDto supplierDto) {
			if (supplierDto.SupplierId.Equals(Guid.Empty)) {
				return BadRequest("SupplierId can't be empty");
			}
			
			var supplier = new Supplier {
				Id = supplierDto.SupplierId,
				Name = supplierDto.SupplierName,
				CanMultiSelect = supplierDto.CanMultiSelect,
				AvailableMoneyToOrder = supplierDto.AvailableMoneyToOrder,
				Position = supplierDto.Position,
			};
			
			_menuEditorService.UpdateSupplier(supplier);
			
			return Ok();
		}
		
		[HttpDelete]
		public IActionResult Delete(Guid supplierId) {
			_menuEditorService.DeleteSupplier(supplierId);
			
			return Ok();
		}
	}
}