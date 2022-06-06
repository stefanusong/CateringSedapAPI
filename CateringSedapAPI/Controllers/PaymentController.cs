using CateringSedapAPI.Dto;
using CateringSedapAPI.Entitties;
using CateringSedapAPI.Factories;
using CateringSedapAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CateringSedapAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly IResponseFactory _responseFactory;
        
        public PaymentController(IPaymentService paymentService, IResponseFactory responseFactory)
        {
            _paymentService = paymentService;
            _responseFactory = responseFactory;
        }

        [HttpGet]
        public async Task<IActionResult> GetPayments()
        {
            try
            {
                var payments = await _paymentService.GetPayments();
                if (payments.Any())
                {
                    return Ok(_responseFactory.GetSuccessResponse("Payment retrieved successfully", payments));
                }
                else
                {
                    return NotFound(_responseFactory.GetSuccessResponse("No payment found", new { }));
                }
            }
            catch (Exception e)
            {
                return BadRequest(_responseFactory.GetErrorResponse(e.Message));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPayment(Guid id)
        {
            try
            {
                var payment = await _paymentService.GetPayment(id);
                if (payment != null)
                {
                    return Ok(_responseFactory.GetSuccessResponse("Payment retrieved successfully", payment));
                }
                else
                {
                    return NotFound(_responseFactory.GetSuccessResponse("Payment not found", new { }));
                }
            }
            catch (Exception e)
            {
                return BadRequest(_responseFactory.GetErrorResponse(e.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment(PaymentDto paymentDetail)
        {
            try
            {
                var payment = await _paymentService.CreatePayment(paymentDetail);
                if (payment != null)
                {
                    return Ok(_responseFactory.GetSuccessResponse("Payment created successfully", payment));
                }
                else
                {
                    return BadRequest(_responseFactory.GetErrorResponse("Payment failed to be created"));
                }
            }
            catch(Exception e)
            {
                return BadRequest(_responseFactory.GetErrorResponse(e.Message));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment (Guid id, Payment.PaymentStatus status)
        {
            try
            {
                var payment = await _paymentService.UpdatePaymentStatus(id, status);
                if (payment)
                {
                    return Ok(_responseFactory.GetSuccessResponse("Payment status updated successfully", payment));
                }
                else
                {
                    return BadRequest(_responseFactory.GetErrorResponse("Payment status not updated"));
                }
            }
            catch (Exception e)
            {
                return BadRequest (_responseFactory.GetErrorResponse(e.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(Guid id)
        {
            try
            {
                var payment = await _paymentService.CancelPayment(id);
                if (payment)
                {
                    return Ok(_responseFactory.GetSuccessResponse("Payment cancelled successfully", payment));
                }
                else
                {
                    return BadRequest(_responseFactory.GetErrorResponse("Payment not cancelled"));
                }
            }
            catch (Exception e)
            {
                return BadRequest(_responseFactory.GetErrorResponse(e.Message));
            }
        }

    }
}
