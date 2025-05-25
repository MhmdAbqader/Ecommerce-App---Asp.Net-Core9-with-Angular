using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Ecommerce.Api.Errors;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Ecommerce.Api.CustomMiddleware
{
    public class MyCustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MyCustomExceptionMiddleware> _logger;
        private readonly IHostEnvironment _hostDevelopmentMode;

        public MyCustomExceptionMiddleware(RequestDelegate next, ILogger<MyCustomExceptionMiddleware> logger,IHostEnvironment host)
        {
            _next = next;
            _logger = logger;
            _hostDevelopmentMode = host;
        }

        public async Task InvokeAsync(HttpContext context) 
        {
            try
            {
                await _next(context);
                _logger.LogInformation("Success!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"this error come from my custom middleware {ex.Message} !");

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var response = _hostDevelopmentMode.IsDevelopment() ?
                    new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                    :new ApiException((int)HttpStatusCode.InternalServerError);

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);

               
            }

        }
    }
}
