using CateringSedapAPI.Entitties;

namespace CateringSedapAPI.Dto
{
    public class ReservationDetailDto
    {
        public Guid ReservationId { get; set; }
        public Guid CustomerId { get; set; }
        public ReservationStatus Status { get; set; }
        public DateTime ReservationDate { get; set; }
        public string? Address { get; set; }
        public List<ReservedFoodDetailDto>? Foods { get; set; }
    };
}