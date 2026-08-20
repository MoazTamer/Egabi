using System.Net;
using System.Text.Json;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Application.Common.Exceptions;

namespace StudentManagementSystem.API.Middleware
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
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = exception switch
            {
                ValidationException validationException => new ErrorResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Validation failed",
                    Errors = validationException.Errors
                        .Select(e => e.ErrorMessage)
                        .ToList()
                },

                NotFoundException notFoundException => new ErrorResponse
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = notFoundException.Message,
                    Errors = new List<string>()
                },

                ConflictException conflictException => new ErrorResponse
                {
                    StatusCode = (int)HttpStatusCode.Conflict,
                    Message = conflictException.Message,
                    Errors = new List<string>()
                },

                DbUpdateException dbUpdateException => new ErrorResponse
                {
                    StatusCode = (int)HttpStatusCode.Conflict,
                    Message = "The operation could not be completed because it conflicts with existing data.",
                    Errors = new List<string> { dbUpdateException.InnerException?.Message ?? dbUpdateException.Message }
                },

                _ => new ErrorResponse
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = "An unexpected error occurred. Please try again later.",
                    Errors = new List<string>()
                }
            };

            if (response.StatusCode >= 500)
            {
                _logger.LogError(exception, "Unhandled exception occurred: {Message}", exception.Message);
            }
            else
            {
                _logger.LogWarning("Request failed with {StatusCode}: {Message}", response.StatusCode, response.Message);
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = response.StatusCode;

            var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response, jsonOptions));
        }
    }
}
