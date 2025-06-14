using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Vehicheck.Infrastructure.Exceptions;

namespace Vehicheck.Infrastructure.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }
        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = new ErrorResponse();

            switch (exception)
            {
                case EntityNotFoundException ex:
                    response = CreateErrorResponse(ex.Message, HttpStatusCode.NotFound, $"Entity: {ex.EntityName}, ID: {ex.EntityId}");
                    break;

                default:
                    response = CreateErrorResponse("An internal server error occurred", HttpStatusCode.InternalServerError, "Please contact support");
                    break;
            }

            context.Response.StatusCode = response.StatusCode;

            var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(jsonResponse);
        }

        private static ErrorResponse CreateErrorResponse(string message, HttpStatusCode statusCode, string details)
        {
            return new ErrorResponse
            {
                Message = message,
                StatusCode = (int)statusCode,
                Details = details,
                Timestamp = DateTime.UtcNow
            };
        }
    }

    public class ErrorResponse
    {
        public string Message { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public string Details { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public IDictionary<string, string[]>? ValidationErrors { get; set; }
    }
}
