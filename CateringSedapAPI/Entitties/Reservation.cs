using System.ComponentModel.DataAnnotations.Schema;

namespace CateringSedapAPI.Entitties
{
    public class Reservation
    {
        public Guid Id { get; set; }
        [ForeignKey("User")]
        public Guid CustomerId { get; set; }
        public ReservationStatus Status { get; set; }
        public DateTime ReservationDate { get; set; }
        public List<Food>? Foods { get; set; }
        public string? Address { get; set; }
    }


    public enum ReservationStatus
    {
        Received = 1,
        Processed,
        Accepted,
        WaitingForPayment,
        Rejected,
        OnDelivery,
        Delivered
    }
}
