using E_commerce.API.Errors;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace E_commerce.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger,
            IHostEnvironment env
            )
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
                _logger.LogInformation("success");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"This Error come from Exception Middleware {ex.Message} !");
                
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                
                var response = _env.IsDevelopment() ? 
                        new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                      : new ApiException((int)HttpStatusCode.InternalServerError);
                
                var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, options);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
