namespace WebApi.Exceptions
{
    public class ApiErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = default!;
        public string TraceId { get; set; } = default!;
        public object? Errors { get; set; } // validation purpose only 
    }
}
