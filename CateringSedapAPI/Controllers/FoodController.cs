using CateringSedapAPI.Context;
using CateringSedapAPI.Entitties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CateringSedapAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FoodController : ControllerBase
    {
        private readonly ApplicationContext _db;
        public FoodController(ApplicationContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var foods = await _db.Foods.ToListAsync();
            return Ok(foods);
        }

        [HttpGet]
        [Route("get-food-by-id")]
        public async Task<IActionResult> GetFoodByIdAsync(Guid id)
        {
            var food = await _db.Foods.FindAsync(id);
            return Ok(food);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Food newFood)
        {
            _db.Foods.Add(newFood);
            await _db.SaveChangesAsync();
            return Created($"/get-food-by-id?id={newFood.Id}", newFood);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(Food foodToUpdate)
        {
            _db.Foods.Update(foodToUpdate);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var foodToDelete = await _db.Foods.FindAsync(id);
            if (foodToDelete == null)
            {
                return NotFound();
            }

            _db.Foods.Remove(foodToDelete);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
