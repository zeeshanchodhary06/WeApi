namespace WebApi.Models.Entities
{
    public class ExceptionLog
    {
        public Guid Id { get; set; }

        public string Message { get; set; } = default!;
        public string ExceptionType { get; set; } = default!;
        public string StackTrace { get; set; } = default!;
        public string? InnerException { get; set; }

        public int StatusCode { get; set; }
        public string Path { get; set; } = default!;
        public string Method { get; set; } = default!;

        public string TraceId { get; set; } = default!;
        public string? UserId { get; set; }

        public DateTime OccurredAtUtc { get; set; }
    }
}
