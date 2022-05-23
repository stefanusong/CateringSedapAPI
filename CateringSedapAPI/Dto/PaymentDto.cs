namespace CateringSedapAPI.Dto
{
    public class PaymentDto
    {
        public Guid ReservationID { get; set; }
        public string? PaidStatus { get; set; }
        public string? PaymentMethod { get; set; }
        public int Amount { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}