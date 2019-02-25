using System;
using FoodOrder.BusinessLogic.DTOs;
using FoodOrder.BusinessLogic.Services;
using FoodOrder.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrder.WebUI.Controllers.MenuEditor {
	[Route("api/menu-editor/[controller]")]
	[ApiController]
	public class CategoryController : Controller {
		private readonly IMenuEditorService _menuEditorService;

		public CategoryController(IMenuEditorService menuEditorService) {
			_menuEditorService = menuEditorService;
		}

		[HttpPost]
		public IActionResult Post([FromBody] CategoryDto categoryDto) {
			var category = new DishCategory {
				Id = categoryDto.Id,
				Name = categoryDto.Name,
				Position = categoryDto.Position,
			};
			
			_menuEditorService.CreateCategory(category, categoryDto.SupplierId);
			
			return Ok();
		}

		[HttpPut]
		public IActionResult Put([FromBody] CategoryDto categoryDto) {
			if (categoryDto.Id.Equals(Guid.Empty)) {
				return BadRequest("Category Id can't be empty");
			}
			
			var category = new DishCategory {
				Id = categoryDto.Id,
				Name = categoryDto.Name,
				Position = categoryDto.Position,
			};

			_menuEditorService.UpdateCategory(category);
			
			return Ok();
		}

		[HttpDelete]
		public IActionResult Delete(Guid categoryId) {
			_menuEditorService.DeleteCategory(categoryId);
			
			return Ok();
		}
	}
}