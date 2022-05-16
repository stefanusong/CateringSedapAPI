using System.ComponentModel.DataAnnotations.Schema;

namespace CateringSedapAPI.Entitties
{
    public class Delivery
    {
        public Guid Id { get; set; }
        [ForeignKey("Reservation")]
        public Guid ReservationId { get; set; }
        [ForeignKey("User")]
        public Guid DriverId { get; set; }

    }
}
