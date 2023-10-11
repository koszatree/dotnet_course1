using Course1.Properties.Services;

namespace Course1
{
    public class CustomEndpoint
    {
        public static async Task Endpoint(HttpContext context, IResponseFormatter formatter)
        {
            //IResponseFormatter formatter = context.RequestServices.GetRequiredService<IResponseFormatter>();
                
            await formatter.Format(context, "Custom Endpoint");
        }
    }
}
