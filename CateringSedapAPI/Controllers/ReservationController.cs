using CateringSedapAPI.Context;
using CateringSedapAPI.Entitties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CateringSedapAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly ApplicationContext _db;
        public ReservationController(ApplicationContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var reservations = await _db.Reservations.ToListAsync();
            return Ok(reservations);
        }

        [HttpGet]
        [Route("get-reservation-by-id")]
        public async Task<IActionResult> GetReservationByIdAsync(Guid id)
        {
            var reservation = await _db.Reservations.FindAsync(id);
            return Ok(reservation);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Reservation reservation)
        {
            _db.Reservations.Add(reservation);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
