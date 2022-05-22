using CateringSedapAPI.Context;
using CateringSedapAPI.Entitties;
using Microsoft.EntityFrameworkCore;

namespace CateringSedapAPI.Repositories
{
    public interface IFoodRepository
    {
        Task<List<Food>> GetFoods();
        Task<Food?> GetFood(Guid id);
        Task<string> CreateFood(Food food);
        Task UpdateFood(Food food);
        Task DeleteFood(Guid id);
    }

    public class FoodRepository : IFoodRepository
    {
        public readonly ApplicationContext _db;

        public FoodRepository(ApplicationContext context)
        {
            _db = context;
        }

        public async Task<List<Food>> GetFoods()
        {
            return await _db.Foods.ToListAsync();
        }

        public async Task<Food?> GetFood(Guid id)
        {
            return await _db.Foods.FindAsync(id);
        }

        public async Task<string> CreateFood(Food food)
        {
            _db.Foods.Add(food);
            await _db.SaveChangesAsync();
            return food.Id.ToString();
        }

        public async Task UpdateFood(Food food)
        {
            _db.Foods.Update(food);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteFood(Guid id)
        {
            var food = await _db.Foods.FindAsync(id);
            if (food != null)
            {
                _db.Foods.Remove(food);
                await _db.SaveChangesAsync();
            }
        }
    }
}