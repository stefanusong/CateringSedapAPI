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
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly IResponseHelper _responseHelper;
        
        public PaymentController(IPaymentService paymentService, IResponseHelper responseHelper)
        {
            _paymentService = paymentService;
            _responseHelper = responseHelper;
        }
    }
}
