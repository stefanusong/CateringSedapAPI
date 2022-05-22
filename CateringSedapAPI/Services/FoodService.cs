using CateringSedapAPI.Dto;
using CateringSedapAPI.Entitties;
using CateringSedapAPI.Repositories;

namespace CateringSedapAPI.Services
{
    public interface IFoodService
    {
        Task<List<Food>> GetFoods();
        Task<Food?> GetFood(Guid id);
        Task<string> CreateFood(FoodDto food);
        Task<bool> UpdateFood(Guid foodId, FoodDto food);
        Task<bool> DeleteFood(Guid id);
    }

    public class FoodService : IFoodService
    {
        public readonly IFoodRepository _repo;

        public FoodService(IFoodRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Food>> GetFoods()
        {
            var foods = await _repo.GetFoods();
            return foods;
        }

        public async Task<Food?> GetFood(Guid id)
        {
            var food = await _repo.GetFood(id);
            return food;
        }

        public async Task<string> CreateFood(FoodDto food)
        {
            ValidateFood(food);

            try
            {
                Food foodEntity = MapFoodFromDto(food);
                var id = await _repo.CreateFood(foodEntity);
                return id.ToString();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> UpdateFood(Guid foodId, FoodDto food)
        {
            ValidateFood(food);

            try
            {
                Food foodEntity = MapFoodFromDto(food);
                foodEntity.Id = foodId;

                await _repo.UpdateFood(foodEntity);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeleteFood(Guid id)
        {
            try
            {
                await _repo.DeleteFood(id);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void ValidateFood(FoodDto food)
        {
            if (food.Name is null)
            {
                throw new Exception("Name is required");
            }
            if (food.Description is null)
            {
                throw new Exception("Description is required");
            }
            if (food.Price <= 0)
            {
                throw new Exception("Price must be greater than 0");
            }
            if (food.Stock <= 0)
            {
                throw new Exception("Stock must be greater than 0");
            }
        }

        private Food MapFoodFromDto(FoodDto food)
        {
            Food foodEntity = new Food
            {
                Name = food.Name,
                Description = food.Description,
                Price = food.Price,
                Stock = food.Stock
            };
            return foodEntity;
        }
    }
}