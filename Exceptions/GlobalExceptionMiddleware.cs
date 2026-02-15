using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApi.Interfaces;

namespace WebApi.Exceptions
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;
        private readonly IServiceScopeFactory _scopeFactory;// 🔥 NEW

        public GlobalExceptionMiddleware(
            RequestDelegate next,
            ILogger<GlobalExceptionMiddleware> logger,
            IHostEnvironment env,
             IServiceScopeFactory scopeFactory) // 🔥 NEW
        {
            _next = next;
            _logger = logger;
            _env = env;
            _scopeFactory = scopeFactory;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var statusCode = MapStatusCode(ex);

                _logger.LogError(ex, "Unhandled exception");

                // 🔥 Persist FULL exception safely

                using var scope = _scopeFactory.CreateScope();
                var exceptionLogger =
                    scope.ServiceProvider.GetRequiredService<IExceptionLogger>();
                try
                {
                    await exceptionLogger.LogAsync(ex, context, statusCode);
                }
                catch (Exception logEx)
                {
                    _logger.LogCritical(logEx, "Failed to persist exception");
                }

                await HandleExceptionAsync(context, ex, statusCode);
            }
        }

        private async Task HandleExceptionAsync(
            HttpContext context,
            Exception exception,
            int statusCode)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = statusCode;

            var errorResponse = new ApiErrorResponse
            {
                StatusCode = statusCode,
                TraceId = context.TraceIdentifier
            };

            switch (exception)
            {
                case NotFoundException ex:
                    errorResponse.Message = ex.Message;
                    break;

                case ValidationException ex:
                    errorResponse.Message = "Invalid request data";
                    errorResponse.Errors = ex.Errors;
                    break;

                case BusinessRuleException ex:
                    errorResponse.Message = ex.Message;
                    break;

                case UnauthorizedAccessException:
                    errorResponse.Message = "Unauthorized access";
                    break;

                default:
                    errorResponse.Message = "Something went wrong. Please try again later.";
                    break;
            }

            if (_env.IsDevelopment())
            {
                errorResponse.Errors ??= exception.Message;
            }

            await response.WriteAsJsonAsync(errorResponse);
        }

        private static int MapStatusCode(Exception exception) =>
            exception switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                ValidationException => StatusCodes.Status400BadRequest,
                BusinessRuleException => StatusCodes.Status409Conflict,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                DbUpdateException => StatusCodes.Status500InternalServerError,
                SqlException => StatusCodes.Status500InternalServerError,
                _ => StatusCodes.Status500InternalServerError
            };
    }

}
