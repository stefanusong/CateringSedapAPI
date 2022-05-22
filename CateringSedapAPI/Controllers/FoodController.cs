using CateringSedapAPI.Context;
using CateringSedapAPI.Dto;
using CateringSedapAPI.Entitties;
using CateringSedapAPI.Helpers;
using CateringSedapAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CateringSedapAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class FoodController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly IFoodService _foodService;
        public readonly IResponseHelper _responseHelper;
        public FoodController(ApplicationContext db, IFoodService foodService, IResponseHelper responseHelper)
        {
            _db = db;
            _foodService = foodService;
            _responseHelper = responseHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetFoods()
        {
            try
            {
                var foods = await _foodService.GetFoods();
                if (foods.Any())
                {
                    return Ok(_responseHelper.GetSuccessResponse("foods retrieved successfully", foods));
                }
                return Ok(_responseHelper.GetSuccessResponse("there is no food yet", foods));
            }
            catch (Exception e)
            {
                return BadRequest(_responseHelper.GetErrorResponse(e.Message));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFood(Guid id)
        {
            try
            {
                var food = await _foodService.GetFood(id);
                if (food != null)
                {
                    return Ok(_responseHelper.GetSuccessResponse("food retrieved successfully", food));
                }
                return NotFound(_responseHelper.GetSuccessResponse("there is no food with this id", new { }));
            }
            catch (Exception e)
            {
                return BadRequest(_responseHelper.GetErrorResponse(e.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateFood(FoodDto food)
        {
            try
            {
                var res = await _foodService.CreateFood(food);
                if (!string.IsNullOrEmpty(res))
                {
                    return Ok(_responseHelper.GetSuccessResponse("food created successfully", res));
                }
                return BadRequest(_responseHelper.GetErrorResponse("food not created"));
            }
            catch (Exception e)
            {
                return BadRequest(_responseHelper.GetErrorResponse(e.Message));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFood(Guid id, FoodDto food)
        {
            try
            {
                var res = await _foodService.UpdateFood(id, food);
                if (res)
                {
                    return Ok(_responseHelper.GetSuccessResponse("food updated successfully", res));
                }
                return BadRequest(_responseHelper.GetErrorResponse("food not updated"));
            }
            catch (Exception e)
            {
                return BadRequest(_responseHelper.GetErrorResponse(e.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFood(Guid id)
        {
            try
            {
                var res = await _foodService.DeleteFood(id);
                if (res)
                {
                    return Ok(_responseHelper.GetSuccessResponse("food deleted successfully", res));
                }
                return BadRequest(_responseHelper.GetErrorResponse("food not deleted"));
            }
            catch (Exception e)
            {
                return BadRequest(_responseHelper.GetErrorResponse(e.Message));
            }
        }

        private void ValidateFood(Food food)
        {
            if (food.Name == null || food.Name == "")
            {
                throw new Exception("Name is required");
            }

            if (food.Price <= 0)
            {
                throw new Exception("Price must be greater than 0");
            }
        }
    }
}
