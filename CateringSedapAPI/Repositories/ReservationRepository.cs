using CateringSedapAPI.Context;
using CateringSedapAPI.Entitties;
using Microsoft.EntityFrameworkCore;

namespace CateringSedapAPI.Repositories
{
    public interface IReservationRepository
    {
        Task<List<Reservation>> GetReservations();
        Task<Reservation?> GetReservation(Guid id);
        Task<List<ReservationFood>?> GetReservationFoods(Guid reservationId);
        Task<Guid> CreateReservation(Reservation reservation);
        Task CreateReservationFood(ReservationFood reservationFood);
        Task UpdateReservation(Reservation reservation);
        Task DeleteReservation(Guid id);
    }

    public class ReservationRepository : IReservationRepository
    {
        public readonly ApplicationContext _db;

        public ReservationRepository(ApplicationContext context)
        {
            _db = context;
        }

        public async Task<List<Reservation>> GetReservations()
        {
            return await _db.Reservations.ToListAsync();
        }

        public async Task<Reservation?> GetReservation(Guid id)
        {
            return await _db.Reservations.FindAsync(id);
        }

        public async Task<List<ReservationFood>?> GetReservationFoods(Guid reservationId)
        {
            return await _db.ReservationFoods.Where(x => x.ReservationId == reservationId).ToListAsync();
        }

        public async Task<Guid> CreateReservation(Reservation reservation)
        {
            _db.Reservations.Add(reservation);
            await _db.SaveChangesAsync();
            return reservation.Id;
        }

        public async Task CreateReservationFood(ReservationFood reservationFood)
        {
            _db.ReservationFoods.Add(reservationFood);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateReservation(Reservation reservation)
        {
            _db.Reservations.Update(reservation);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteReservation(Guid id)
        {
            var reservation = await _db.Reservations.FindAsync(id);
            if (reservation != null)
            {
                _db.Reservations.Remove(reservation);
                await _db.SaveChangesAsync();
            }
        }
    }
}