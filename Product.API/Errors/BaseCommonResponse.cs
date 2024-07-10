
namespace Product.API.Errors
{
    public class BaseCommonResponse
    {

        public BaseCommonResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? DefaultMessageForStatusCode(statusCode);
        }

        private string DefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad Request",
                401 => "Not Authorize",
                404 => "Resource Not Found",
                500 => "Server Error",
                _ => null,
            };
        }

        public int StatusCode { get; }
        public string Message { get; }
    }
}
