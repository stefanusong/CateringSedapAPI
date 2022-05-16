using System.ComponentModel.DataAnnotations.Schema;

namespace CateringSedapAPI.Entitties
{
    public class Payment
    {
        public Guid Id { get; set; }
        [ForeignKey("Reservation")]
        public Guid ReservationId { get; set; }
        public PaymentStatus Status { get; set; }
        public int Amount { get; set; }
        public DateTime PaymentDate { get; set; }

        public enum PaymentStatus
        {
            AwaitingForPayment = 1,
            Paid,
            Cancelled,
            Expired
        }
    }
}
