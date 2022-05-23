using CateringSedapAPI.Context;
using CateringSedapAPI.Entitties;
using Microsoft.EntityFrameworkCore;

namespace CateringSedapAPI.Repositories
{
    public interface IPaymentRepository
    {
        Task<List<Payment>> GetPayments();
        Task<Payment?> GetPayment(Guid id);
        Task<string> CreatePayment(Payment payment);
        Task UpdatePayment(Payment payment);
        Task CancelPayment(Guid id);
    }

    public class PaymentRepository : IPaymentRepository
    {
        public readonly ApplicationContext _db;

        public PaymentRepository(ApplicationContext context)
        {
            _db = context;
        }

        public async Task<List<Payment>> GetPayments()
        {
            return await _db.Payments.ToListAsync();
        }

        public async Task<Payment?> GetPayment(Guid id)
        {
            return await _db.Payments.FindAsync(id);
        }

        public async Task<string> CreatePayment(Payment payment)
        {
            _db.Payments.Add(payment);
            await _db.SaveChangesAsync();
            return payment.Id.ToString();
        }

        public async Task UpdatePayment(Payment payment)
        {
            _db.Payments.Update(payment);
            await _db.SaveChangesAsync();
        }

        public async Task CancelPayment(Guid id)
        {
            var payment = await _db.Payments.FindAsync(id);
            if (payment != null)
            {
                _db.Payments.Remove(payment);
                await _db.SaveChangesAsync();
            }
        }
    }
}
}
