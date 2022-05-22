using CateringSedapAPI.Dto;

namespace CateringSedapAPI.Helpers
{
    public interface IResponseHelper
    {
        ResponseDto GetSuccessResponse(string message, dynamic data);
        ResponseDto GetErrorResponse(string message);
    }
    public class ResponseHelper : IResponseHelper
    {
        public ResponseDto GetSuccessResponse(string message, dynamic data)
        {
            return new ResponseDto
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public ResponseDto GetErrorResponse(string message)
        {
            return new ResponseDto
            {
                Success = false,
                Message = message,
                Data = null
            };
        }
    }
}