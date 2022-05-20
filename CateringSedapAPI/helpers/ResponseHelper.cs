using CateringSedapAPI.Dto;
using Newtonsoft.Json;

namespace CateringSedapAPI.Helpers
{
    public interface IResponseHelper
    {
        string GetSuccessResponse(string message, dynamic data);
        string GetErrorResponse(string message);
    }
    public class ResponseHelper : IResponseHelper
    {
        public string GetSuccessResponse(string message, dynamic data)
        {
            return JsonConvert.SerializeObject(new ResponseDto
            {
                Success = true,
                Message = message,
                Data = data
            }, Formatting.Indented);
        }

        public string GetErrorResponse(string message)
        {
            return JsonConvert.SerializeObject(new ResponseDto
            {
                Success = false,
                Message = message,
                Data = null
            }, Formatting.Indented);
        }
    }
}