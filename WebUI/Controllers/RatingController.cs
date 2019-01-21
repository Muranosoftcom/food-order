using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : Controller {
        private readonly IRepository _repo;

        public RatingController(IRepository repo) {
            _repo = repo;
        }
        
        [HttpPut]
        [Route("increment-rating")]
        [Authorize]
        public async Task<ActionResult> IncrementRating(int dishItemId) {
            var dishItem = _repo.GetById<DishItem>(dishItemId);
            dishItem.PositiveReviews++;
            _repo.Update(dishItem);
            await _repo.SaveAsync();
            return new OkResult();
        }

        [HttpPut]
        [Route("decrement-rating")]
        [Authorize]
        public async Task<ActionResult> DecrementRating(int dishItemId) {
            var dishItem = _repo.GetById<DishItem>(dishItemId);
            dishItem.NegativeReviews++;
            _repo.Update(dishItem);
            await _repo.SaveAsync();
            return new OkResult();
        }
    }
}