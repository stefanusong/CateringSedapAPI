using CateringSedapAPI.Dto;
using CateringSedapAPI.Entitties;
using CateringSedapAPI.Repositories;

namespace CateringSedapAPI.Services
{
    public interface IDeliveryService
    {
        Task<List<Delivery>> GetDeliveries();
        Task<Delivery?> GetDelivery(Guid id);
        Task<string> CreateDelivery(DeliveryDto deliveryDetail);
        Task<bool> UpdateDelivery(Guid id, DeliveryDto deliveryDetail);
        Task<bool> DeleteDelivery(Guid id);
    }

    public class DeliveryService : IDeliveryService
    {
        public readonly IDeliveryRepository _deliveryRepository;
        public readonly IUserRepository _userRepository;

        public DeliveryService(IDeliveryRepository deliveryRepository, IUserRepository userRepository)
        {
            _deliveryRepository = deliveryRepository;
            _userRepository = userRepository;
        }

        public async Task<List<Delivery>> GetDeliveries()
        {
            var delivery = await _deliveryRepository.GetDeliveries();
            return delivery;
        }

        public async Task<Delivery?> GetDelivery(Guid id)
        {
            var delivery = await _deliveryRepository.GetDelivery(id);
            return delivery;
        }

        public async Task<string> CreateDelivery(DeliveryDto deliveryDetail)
        {
            await ValidateDelivery(deliveryDetail);
            try
            {
                Delivery newDelivery = new Delivery
                {
                    ReservationId = deliveryDetail.ReservationId,
                    DriverId = deliveryDetail.DriverId
                };
                var deliveryId = await _deliveryRepository.CreateDelivery(newDelivery);
                return deliveryId.ToString();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> UpdateDelivery(Guid id, DeliveryDto deliveryDetail)
        {
            await ValidateDelivery(deliveryDetail);

            try
            {
                var delivery = await _deliveryRepository.GetDelivery(id);
                if(delivery != null)
                {
                    delivery.ReservationId = deliveryDetail.ReservationId;
                    delivery.DriverId = deliveryDetail.DriverId;
                    await _deliveryRepository.UpdateDelivery(delivery);
                    return true;
                }
                throw new Exception("Delivery Id not found");
            } catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<bool> DeleteDelivery(Guid id)
        {
            try
            {
                await _deliveryRepository.DeleteDelivery(id);
                return true;
            } catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        private async Task ValidateDelivery(DeliveryDto delivery)
        {
            var user = await _userRepository.GetUserById(delivery.DriverId);
            if (user == null)
            {
                throw new Exception("Driver does not exist");
            }
            if(user.Role != User.UserRole.driver)
            {
                throw new Exception("DriverId is not a driver");
            }
        }
    }


}
