namespace CateringSedapAPI.Dto
{
    public class ResponseDto
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public dynamic? Data { get; set; }
    }
}