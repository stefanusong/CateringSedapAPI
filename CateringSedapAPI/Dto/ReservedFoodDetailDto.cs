namespace CateringSedapAPI.Dto
{
    public class ReservedFoodDetailDto
    {
        public Guid FoodId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }
}