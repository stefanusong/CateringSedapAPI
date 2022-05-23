namespace CateringSedapAPI.Dto
{
    public class ReservationDto
    {
        public List<ReservedFoodDto>? Foods { get; set; }
        public string? Address { get; set; }
    }
}