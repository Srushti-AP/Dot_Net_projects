using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Text.Json;
using ProductApi.Exceptions;
using ProductApi.Models;

namespace ProductApi.Middlewares
{
    public class ErrorHandellingMiddleware
    {
        private readonly RequestDelegate _next;
          private readonly ILogger<ErrorHandellingMiddleware> _logger;

           public ErrorHandellingMiddleware(
            RequestDelegate next,
            ILogger<ErrorHandellingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex,ex.Message);
                await HandleExceptionAsync(context,ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context,Exception exception)
        {
              int statusCode = (int)HttpStatusCode.InternalServerError;
            string message = "An unexpected error occurred";

             if (exception is AppException appException)
            {
                statusCode = appException.StatusCode;
                message = appException.Message;
            }

            var response = new ErrorResponse
            {
                 StatusCode = statusCode,
                Message = message,
                Path = context.Request.Path,
                Timestamp = DateTime.UtcNow
            };
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response)
            );
        }

    }
}