using System;
using System.Linq;
using FoodOrder.BusinessLogic.DTOs;
using FoodOrder.BusinessLogic.Services;
using FoodOrder.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrder.WebUI.Controllers.MenuEditor {
	[Route("api/menu-editor/[controller]")]
	[ApiController]
	public class DishController : Controller {
		private readonly IMenuEditorService _menuEditorService;

		public DishController(IMenuEditorService menuEditorService) {
			_menuEditorService = menuEditorService;
		}
	
		
		[HttpPut]
		public IActionResult Put([FromBody] DishDto dishDto) {
			if (dishDto.Id.Equals(Guid.Empty)) {
				return BadRequest("Dish Id can't be empty");
			}
			
			var dish = new Dish {
				Id = dishDto.Id,
				Name = dishDto.Name,
				Price = dishDto.Price,
				AvailableAt = dishDto.AvailableAt.Cast<DayOfWeek>().ToArray()
			};
			
			_menuEditorService.UpdateDish(dish);

			return Ok();
		}
		
		[HttpPost]
		public IActionResult Post([FromBody] DishDto dishDto) {
			if (dishDto.CategoryId.Equals(Guid.Empty)) {
				return BadRequest("Category Id can't be empty");
			}
			
			var dish = new Dish {
				Name = dishDto.Name,
				Price = dishDto.Price,
				AvailableAt = dishDto.AvailableAt.Cast<DayOfWeek>().ToArray()
			};
			
			_menuEditorService.CreateDish(dish, dishDto.CategoryId);

			return Ok();
		}
		
		[HttpDelete]
		public IActionResult Delete(Guid id) {
			_menuEditorService.DeleteDish(id);
			
			return Ok();
		}
	}
}