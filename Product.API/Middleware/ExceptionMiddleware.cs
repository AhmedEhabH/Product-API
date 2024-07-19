using Microsoft.Extensions.Hosting;
using Product.API.Errors;
using System.Net;
using System.Text.Json;

namespace Product.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _host;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment host)
        {
            _next = next;
            _logger = logger;
            _host = host;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
                _logger.LogInformation("Success");
                _logger.LogInformation("Pass Middleware");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"This Error come From exception Middleware \n {ex.Message} !");

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var response = _host.IsDevelopment()
                    ? new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                    : new ApiException((int)HttpStatusCode.InternalServerError);

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, options);
                await context.Response.WriteAsync(json);

            }
        }
    }
}
