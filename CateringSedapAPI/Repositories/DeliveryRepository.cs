using CateringSedapAPI.Context;
using CateringSedapAPI.Entitties;
using Microsoft.EntityFrameworkCore;

namespace CateringSedapAPI.Repositories
{
    public interface IDeliveryRepository
    {
        Task<List<Delivery>> GetDeliveries();
        Task<Delivery?> GetDelivery(Guid id);
        Task<string> CreateDelivery(Delivery delivery);
        Task UpdateDelivery(Delivery delivery);
        Task DeleteDelivery(Guid id);

    }

    public class DeliveryRepository : IDeliveryRepository
    {
        public readonly ApplicationContext _db;

        public DeliveryRepository(ApplicationContext context)
        {
            _db = context;
        }

        public async Task<List<Delivery>> GetDeliveries()
        {
            return await _db.Deliveries.ToListAsync();
        }

        public async Task<Delivery?> GetDelivery(Guid id)
        {
            return await _db.Deliveries.FindAsync(id);
        }

        public async Task<string> CreateDelivery(Delivery delivery)
        {
            _db.Deliveries.Add(delivery);
            await _db.SaveChangesAsync();
            return delivery.Id.ToString();
        }

        public async Task UpdateDelivery(Delivery delivery)
        {
            _db.Deliveries.Update(delivery);
            await _db.SaveChangesAsync();
        }
        public async Task DeleteDelivery(Guid id)
        {
            var delivery = await _db.Deliveries.FindAsync(id);
            if(delivery != null)
            {
                _db.Deliveries.Remove(delivery);
                await _db.SaveChangesAsync();
            }
        }
    }
}
