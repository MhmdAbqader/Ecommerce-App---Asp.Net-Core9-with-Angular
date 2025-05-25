namespace Ecommerce.Api.Errors
{
    public class ApiException : BaseCommonResponseError
    {
        public ApiException(int statusCode, string? message = null, string? details = null) : base(statusCode, message)
        {
            Details = details;
        }
        public string Details  { get; set; }
    }
}
