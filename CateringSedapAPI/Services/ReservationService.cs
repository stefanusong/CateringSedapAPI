using CateringSedapAPI.Dto;
using CateringSedapAPI.Entitties;
using CateringSedapAPI.Repositories;
using static CateringSedapAPI.Entitties.User;

namespace CateringSedapAPI.Services
{
    public interface IReservationService
    {
        Task<Guid> CreateReservation(Guid CustomerId, ReservationDto reservation);
        Task<List<ReservationDetailDto>> GetAllReservations();
        Task<ReservationDetailDto?> GetReservationDetail(Guid reservationId);
        Task UpdateReservationStatus(Guid reservationId, ReservationStatus status);
    }
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IFoodRepository _foodRepository;
        private readonly IUserRepository _userRepository;

        public ReservationService(IReservationRepository reservationRepository, IUserRepository userRepository, IFoodRepository foodRepository)
        {
            _reservationRepository = reservationRepository;
            _userRepository = userRepository;
            _foodRepository = foodRepository;
        }

        public async Task<Guid> CreateReservation(Guid CustomerId, ReservationDto reservation)
        {
            var user = await _userRepository.GetUserById(CustomerId);
            ValidateUser(user);

            var newReservation = new Reservation
            {
                CustomerId = CustomerId,
                Status = ReservationStatus.Received,
                ReservationDate = DateTime.Now,
                Address = reservation.Address
            };

            if (reservation != null && reservation.Foods != null)
            {
                try
                {
                    var reservationId = await _reservationRepository.CreateReservation(newReservation);

                    foreach (var food in reservation.Foods)
                    {
                        // check if food exists
                        var foodEntity = await _foodRepository.GetFood(food.FoodId);
                        if (foodEntity == null)
                        {
                            throw new Exception($"Food with id {food.FoodId} does not exist");
                        }

                        // check if food stock is sufficient
                        if (foodEntity.Stock < food.Quantity)
                        {
                            throw new Exception($"Food with id {food.FoodId} does not have enough stock");
                        }

                        // update food stock
                        foodEntity.Stock -= food.Quantity;
                        await _foodRepository.UpdateFood(foodEntity);

                        // add food to reservationFood
                        var reservationFood = new ReservationFood
                        {
                            ReservationId = reservationId,
                            FoodId = food.FoodId,
                            Quantity = food.Quantity,
                        };

                        await _reservationRepository.CreateReservationFood(reservationFood);
                    }

                    return reservationId;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            else
            {
                throw new Exception("Reservation is empty");
            }
        }

        public async Task<List<ReservationDetailDto>> GetAllReservations()
        {
            var reservations = await _reservationRepository.GetReservations();
            var reservationsDto = new List<ReservationDetailDto>();

            foreach (var reservation in reservations)
            {
                var reservationDetail = new ReservationDetailDto
                {
                    ReservationId = reservation.Id,
                    CustomerId = reservation.CustomerId,
                    Status = reservation.Status,
                    ReservationDate = reservation.ReservationDate,
                    Address = reservation.Address,
                    Foods = new List<ReservedFoodDetailDto>()
                };

                var reservationFoods = await _reservationRepository.GetReservationFoods(reservation.Id);
                if (reservationFoods != null)
                {
                    foreach (var reservationFood in reservationFoods)
                    {
                        var food = await _foodRepository.GetFood(reservationFood.FoodId);
                        if (food != null)
                        {
                            reservationDetail.Foods.Add(new ReservedFoodDetailDto
                            {
                                FoodId = food.Id,
                                Name = food.Name,
                                Description = food.Description,
                                Price = food.Price,
                                Quantity = reservationFood.Quantity
                            });
                        }
                    }
                }

                if (reservationDetail.Foods.Count == 0)
                {
                    throw new Exception("Reservation has no foods");
                }

                reservationsDto.Add(reservationDetail);
            }

            return reservationsDto;
        }

        public async Task<ReservationDetailDto?> GetReservationDetail(Guid reservationId)
        {
            var reservation = await _reservationRepository.GetReservation(reservationId);
            if (reservation == null)
            {
                throw new Exception("Reservation not found");
            }

            var reservationDetail = new ReservationDetailDto
            {
                ReservationId = reservation.Id,
                CustomerId = reservation.CustomerId,
                Status = reservation.Status,
                ReservationDate = reservation.ReservationDate,
                Address = reservation.Address,
                Foods = new List<ReservedFoodDetailDto>()
            };

            var reservationFoods = await _reservationRepository.GetReservationFoods(reservationId);
            if (reservationFoods != null)
            {
                foreach (var reservationFood in reservationFoods)
                {
                    var food = await _foodRepository.GetFood(reservationFood.FoodId);
                    if (food != null)
                    {
                        reservationDetail.Foods.Add(new ReservedFoodDetailDto
                        {
                            FoodId = food.Id,
                            Name = food.Name,
                            Description = food.Description,
                            Price = food.Price,
                            Quantity = reservationFood.Quantity
                        });
                    }
                }
            }

            if (reservationDetail.Foods.Count == 0)
            {
                throw new Exception("Reservation has no foods");
            }

            return reservationDetail;
        }

        public async Task UpdateReservationStatus(Guid reservationId, ReservationStatus status)
        {
            var reservation = await _reservationRepository.GetReservation(reservationId);
            if (reservation == null)
            {
                throw new Exception("Reservation not found");
            }

            reservation.Status = status;
            await _reservationRepository.UpdateReservation(reservation);
        }

        private static void ValidateUser(User? user)
        {
            if (user == null)
            {
                throw new Exception("user not found");
            }
            else if (user.Role != UserRole.customer)
            {
                throw new Exception("you are not allowed to make reservations");
            }
        }
    }
}