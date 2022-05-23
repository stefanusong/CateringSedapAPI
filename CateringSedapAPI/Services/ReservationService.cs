namespace CateringSedapAPI.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CateringSedapAPI.Dto;
    using CateringSedapAPI.Entitties;
    using CateringSedapAPI.Repositories;

    public interface IReservationService
    {
        Task<List<Reservation>> GetReservations();
        Task<Reservation?> GetReservation(Guid id);
        Task<string> CreateReservation(ReservationDto reservation);
        Task<bool> UpdateReservation(Guid reservationId, ReservationDto reservation);
        Task<bool> DeleteReservation(Guid id);
    }
    // public class ReservationService : IReservationService
    // {
    //     private readonly IReservationRepository _reservationRepository;
    //     private readonly IUserRepository _userRepository;

    //     public ReservationService(IReservationRepository reservationRepository, IUserRepository userRepository)
    //     {
    //         _reservationRepository = reservationRepository;
    //         _userRepository = userRepository;
    //     }

    //     public async Task<Reservation> CreateReservation(ReservationDto reservation)
    //     {
    //         var user = await _userRepository.GetUserById(reservation.CustomerId);
    //         if (user == null)
    //         {
    //             throw new Exception("User not found");
    //         }

    //         var reservationToCreate = _reservationMapper.MapReservation(reservation);
    //         reservationToCreate.User = user;
    //         reservationToCreate.UserId = user.Id;
    //         reservationToCreate.ReservationDate = DateTime.Now;
    //         reservationToCreate.ReservationStatus = ReservationStatus.Pending;

    //         var createdReservation = await _reservationRepository.CreateReservation(reservationToCreate);
    //         return _reservationMapper.MapReservation(createdReservation);
    //     }

    //     public async Task<Reservation> GetReservationById(int id)
    //     {
    //         var reservation = await _reservationRepository.GetReservationById(id);
    //         return _reservationMapper.MapReservation(reservation);
    //     }

    //     public async Task<IEnumerable<Reservation>> GetReservationsByUserId(int userId)
    //     {
    //         var reservations = await _reservationRepository.GetReservationsByUserId(userId);
    //         return _reservationMapper.MapReservations(reservations);
    //     }
    // }
}