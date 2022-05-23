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
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryService _deliveryService;
        private readonly IResponseHelper _responseHelper;

        public DeliveryController(IDeliveryService deliveryService, IResponseHelper responseHelper)
        {
            _deliveryService = deliveryService;
            _responseHelper = responseHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetDeliveries()
        {
            try
            {
                var delivery = await _deliveryService.GetDeliveries();
                if (delivery.Any())
                {
                    return Ok(_responseHelper.GetSuccessResponse("Delivery retrieved successfully", delivery));
                }
                return Ok(_responseHelper.GetSuccessResponse("No delivery found", new { }));
            } catch (Exception e)
            {
                return BadRequest(_responseHelper.GetErrorResponse(e.Message));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDelivery(Guid id)
        {
            try
            {
                var delivery = await _deliveryService.GetDelivery(id);
                if (delivery != null)
                {
                    return Ok(_responseHelper.GetSuccessResponse("Delivery retrieved successfully", delivery));
                }
                return NotFound(_responseHelper.GetSuccessResponse("Delivery nor found", new { }));
            } catch(Exception e)
            {
                return BadRequest(_responseHelper.GetErrorResponse(e.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDelivery(DeliveryDto deliveryDetail)
        {
            try
            {
                var delivery = await _deliveryService.CreateDelivery(deliveryDetail);
                if(delivery != null)
                {
                    return Ok(_responseHelper.GetSuccessResponse("Delivery created successfully", delivery));
                }
                return BadRequest(_responseHelper.GetErrorResponse("Delivery not created"));
            } catch(Exception e)
            {
                return BadRequest(_responseHelper.GetErrorResponse(e.Message));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDelivery(Guid id, DeliveryDto deliveryDetail)
        {
            try
            {
                var delivery = await _deliveryService.UpdateDelivery(id, deliveryDetail);
                if (delivery)
                {
                    return Ok(_responseHelper.GetSuccessResponse("Delivery updated successfully", delivery));
                }
                return BadRequest(_responseHelper.GetErrorResponse("Delivery not updated"));
            } catch (Exception e)
            {
                return BadRequest(_responseHelper.GetErrorResponse(e.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDelivery(Guid id)
        {
            try
            {
                var delivery = await _deliveryService.DeleteDelivery(id);
                if (delivery)
                {
                    return Ok(_responseHelper.GetSuccessResponse("Delivery deleted successfully", delivery));
                }
                return BadRequest(_responseHelper.GetErrorResponse("Delivery not deleted"));
            } catch (Exception e)
            {
                return BadRequest(_responseHelper.GetErrorResponse(e.Message));
            }
        }
    }
}
