
namespace Ecommerce.Api.Errors
{
    public class BaseCommonResponseError
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public BaseCommonResponseError(int statusCode, string? message=null)
        {
            StatusCode = statusCode;
            Message = message ?? DefaultMessageError(statusCode);
        }

        private string DefaultMessageError(int statusCode)
        {
            return statusCode switch
            {
                400 => "Error, Bad Request",
                401 => "Error, Not Authorized",
                404 => "Error, Resource Not Found",
                500 => "Server Error",
                _ => ""
            };
        }

 
    }
}
