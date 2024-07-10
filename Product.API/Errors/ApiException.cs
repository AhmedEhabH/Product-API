namespace Product.API.Errors
{
    public class ApiException : BaseCommonResponse
    {
        private readonly int _statusCode;
        private readonly string _message;
        private readonly string _details;

        public ApiException(int statusCode, string message = null, string details = null) : base(statusCode, message)
        {
            _statusCode = statusCode;
            _message = message;
            _details = details;
        }
    }
}
