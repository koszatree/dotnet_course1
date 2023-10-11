namespace Course1.Properties.Services
{
    public interface IResponseFormatter
    {
        Task Format(HttpContext context, string content);
    }
}
