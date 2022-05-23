using System.Security.Claims;
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
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IResponseHelper _responseHelper;
        public ReservationController(IReservationService reservationService, IResponseHelper responseHelper)
        {
            _reservationService = reservationService;
            _responseHelper = responseHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReservations()
        {
            try
            {
                var reservations = await _reservationService.GetAllReservations();
                if (reservations == null)
                {
                    return Ok(_responseHelper.GetSuccessResponse("No reservations found", new { }));
                }
                return Ok(_responseHelper.GetSuccessResponse("reservations retrieved", reservations));
            }
            catch (Exception ex)
            {
                return BadRequest(_responseHelper.GetErrorResponse(ex.Message));
            }
        }

        [HttpGet]
        [Route("get-reservation-by-id")]
        public async Task<IActionResult> GetReservationByIdAsync(Guid id)
        {
            try
            {
                var reservation = await _reservationService.GetReservationDetail(id);
                if (reservation == null)
                {
                    return NotFound(_responseHelper.GetErrorResponse("Reservation not found"));
                }
                return Ok(_responseHelper.GetSuccessResponse("Reservation found", reservation));
            }
            catch (Exception e)
            {
                return BadRequest(_responseHelper.GetErrorResponse(e.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(ReservationDto reservation)
        {
            try
            {
                // Get user
                var userId = User.FindFirst("userId")?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                // Create Reservation
                var userIdInGuid = Guid.Parse(userId);
                var res = await _reservationService.CreateReservation(userIdInGuid, reservation);
                if (res == Guid.Empty)
                {
                    return BadRequest();
                }

                return Ok(_responseHelper.GetSuccessResponse("Reservation created", res));
            }
            catch (Exception e)
            {
                return BadRequest(_responseHelper.GetErrorResponse(e.Message));
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateReservationStatus(Guid id, ReservationStatus status)
        {
            try
            {
                var userId = User.FindFirst("userId")?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                var userIdInGuid = Guid.Parse(userId);
                await _reservationService.UpdateReservationStatus(id, status);
                return Ok(_responseHelper.GetSuccessResponse("Reservation updated", new { }));
            }
            catch (Exception e)
            {
                return BadRequest(_responseHelper.GetErrorResponse(e.Message));
            }
        }
    }
}
