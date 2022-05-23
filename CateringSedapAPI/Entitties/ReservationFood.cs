using System.ComponentModel.DataAnnotations.Schema;

namespace CateringSedapAPI.Entitties
{
    public class ReservationFood
    {
        public Guid Id { get; set; }
        [ForeignKey("Reservation")]
        public Guid ReservationId { get; set; }
        [ForeignKey("Food")]
        public Guid FoodId { get; set; }
        public int Quantity { get; set; }
    }
}