namespace WebApi.Interfaces
{
    public interface IExceptionLogger
    {
        Task LogAsync(Exception exception, HttpContext context, int statusCode);
    }
}
