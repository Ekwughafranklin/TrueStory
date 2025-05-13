using Microsoft.AspNetCore.Diagnostics;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using TrueStory.Domain.Dtos;
using TrueStory.Domain.Errors;

namespace TrueStory.Api.Middlewares;

public static class GlobalExceptionMiddleware
{
    public static void UseGlobalExceptionMiddleware(this WebApplication app)
    {
        app.UseExceptionHandler(appBuilder =>
        {
            appBuilder.Run(async context =>
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

                if (exceptionHandlerFeature != null)
                {
                    var exception = exceptionHandlerFeature.Error; // Get the exception
                    var logger = app.Services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(exception, "Unhandled exception occurred.");

                    int statusCode;
                    var message = $"Sorry! An unexpected error occurred.";
                    var errors = new List<ValidationErrorDto>();

                    switch (exception)
                    {
                        case InvalidRequestException requestException:
                            statusCode = requestException?.StatusCode ?? 400;
                            message = exception.Message;
                            errors = requestException?.Errors.ToList();
                            break;
                        case ValidationException validationException:
                            statusCode = StatusCodes.Status400BadRequest;
                            message = "Validation errors occurred.";
                            break;
                        case ArgumentNullException:
                            statusCode = StatusCodes.Status400BadRequest;
                            message = "A required argument was null.";
                            break;
                        case UnauthorizedAccessException:
                            statusCode = StatusCodes.Status401Unauthorized;
                            message = "You are not authorized to perform this action.";
                            break;
                        case KeyNotFoundException:
                            statusCode = StatusCodes.Status404NotFound;
                            message = "The requested resource was not found.";
                            break;
                        default:
                            statusCode = StatusCodes.Status500InternalServerError;
                            message = $"An unexpected error occurred.";
                            break;
                    }

                    context.Response.StatusCode = statusCode;

                    var response = new ErrorResponseDto(false, message, errors);

                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                }
            });
        });
    }
}
