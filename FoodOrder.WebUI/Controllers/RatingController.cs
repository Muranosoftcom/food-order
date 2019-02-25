using System;
using System.Threading.Tasks;
using FoodOrder.Domain.Entities;
using FoodOrder.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrder.WebUI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : Controller {
        private readonly IFoodOrderRepository _repo;

        public RatingController(IFoodOrderRepository repo) {
            _repo = repo;
        }
        
        [HttpPut]
        [Route("increment-rating")]
        [Authorize]
        public async Task<ActionResult> IncrementRating(Guid dishItemId) {
            var dishItem = _repo.GetById<Dish>(dishItemId);
            dishItem.PositiveReviews++;
            _repo.Update(dishItem);
            await _repo.SaveAsync();
            return new OkResult();
        }

        [HttpPut]
        [Route("decrement-rating")]
        [Authorize]
        public async Task<ActionResult> DecrementRating(Guid dishItemId) {
            var dishItem = _repo.GetById<Dish>(dishItemId);
            dishItem.NegativeReviews++;
            _repo.Update(dishItem);
            await _repo.SaveAsync();
            return new OkResult();
        }
    }
}