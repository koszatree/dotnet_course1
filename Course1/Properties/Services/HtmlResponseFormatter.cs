namespace Course1.Properties.Services
{
    public class HtmlResponseFormatter : IResponseFormatter
    {
        private int _responseCounter = 0;
        public async Task Format(HttpContext context, string content)
        {
            context.Response.ContentType = "text/html";
            await context.Response.WriteAsync($"Formatted Respose {++_responseCounter}\n <h2>{content}</h2>");
        }
    }
}
