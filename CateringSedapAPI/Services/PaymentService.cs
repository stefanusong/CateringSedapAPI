using CateringSedapAPI.Dto;
using CateringSedapAPI.Entitties;
using CateringSedapAPI.Repositories;

namespace CateringSedapAPI.Services
{
    public interface IPaymentService
    {
        Task<List<Payment>> GetPayments();
        Task<Payment?> GetPayment(Guid id);
        Task<String> CreatePayment(PaymentDto paymentDetail);
        Task<bool> UpdatePaymentStatus(Guid id, Payment.PaymentStatus status);
        Task<bool> CancelPayment(Guid id);
    }

    public class PaymentService : IPaymentService
    {
        public readonly IPaymentRepository _paymentRepository;
        public readonly IUserRepository _userRepository;


        public PaymentService(IPaymentRepository paymentRepository, IUserRepository userRepository)
        {
            _paymentRepository = paymentRepository;
            _userRepository = userRepository;
        }

        public async Task<List<Payment>> GetPayments()
        {
            var payment = await _paymentRepository.GetPayments();
            return payment;
        }

        public async Task<Payment?> GetPayment(Guid id)
        {
            var payment = await _paymentRepository.GetPayment(id);
            return payment;
        }

        public async Task<string> CreatePayment(PaymentDto paymentDetail)
        {
            try
            {
                Payment newPayment = new Payment
                {
                    ReservationId = paymentDetail.ReservationID,
                    Status = Payment.PaymentStatus.AwaitingForPayment,
                    Amount = paymentDetail.Amount
                };
                var paymentId = await _paymentRepository.CreatePayment(newPayment);
                return paymentId.ToString();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> UpdatePaymentStatus(Guid id, Payment.PaymentStatus status)
        {
            try
            {
                var payment = await _paymentRepository.GetPayment(id);
                if(payment == null)
                {
                    throw new Exception("Payment not found");
                }
                else 
                {
                    if(status == Payment.PaymentStatus.Expired || status == Payment.PaymentStatus.Cancelled)
                    {
                        await CancelPayment(id);
                        return true;
                    }
                    else
                    {
                        payment.Status = status;
                        await _paymentRepository.UpdatePayment(payment);

                        return true;
                    }
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> CancelPayment(Guid id)
        {
            try
            {
                await _paymentRepository.CancelPayment(id);
                return true;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}