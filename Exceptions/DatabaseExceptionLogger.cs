using System;
using WebApi.Data;
using WebApi.Interfaces;
using WebApi.Models.Entities;

namespace WebApi.Exceptions
{
    public class DatabaseExceptionAuditLogger : IExceptionLogger
    {
        private readonly ApplicationDbContext _db;

        public DatabaseExceptionAuditLogger(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task LogAsync(Exception exception, HttpContext context, int statusCode)
        {
            var log = new ExceptionLog
            {
                Id = Guid.NewGuid(),
                Message = exception.Message,
                ExceptionType = exception.GetType().FullName!,
                StackTrace = exception.StackTrace ?? string.Empty,
                InnerException = exception.InnerException?.ToString(),
                StatusCode = statusCode,
                Path = context.Request.Path,
                Method = context.Request.Method,
                TraceId = context.TraceIdentifier,
                UserId = context.User?.Identity?.Name,
                OccurredAtUtc = DateTime.UtcNow
            };

            _db.ExceptionLogs.Add(log);
            await _db.SaveChangesAsync();
        }
    }


}
