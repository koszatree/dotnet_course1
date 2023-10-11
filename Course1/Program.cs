using Course1;
using Course1.Properties.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<FruitOptions>(options =>
{
    options.Name = "watermelon";
});

builder.Services.AddSingleton<IResponseFormatter, HtmlResponseFormatter>();
builder.Services.AddTransient<IResponseFormatter, GuidService>();

var app = builder.Build();

//app.MapGet("/fruit", async(HttpContext context, IOptions<FruitOptions> FruitOptions) => {
//    FruitOptions options = FruitOptions.Value;
//    await context.Response.WriteAsync($"{options.Name}, {options.Color}");
//});

//IResponseFormatter formatter = new TextResponseFormatter();

app.MapGet("/formatter1", async (HttpContext context, IResponseFormatter formatter) =>
{
    await formatter.Format(context, "Formatter 1");
});

app.MapGet("/formatter2", async (HttpContext context, IResponseFormatter formatter) =>
{
    await formatter.Format(context, "Formatter 2");
});

app.UseMiddleware<CustomMiddleware>();
app.UseMiddleware<CustomMiddleware2>();

app.MapGet("/endpoint", CustomEndpoint.Endpoint);

app.UseMiddleware<FruitMiddleware>();

app.MapGet("/", () => "Hello World!");

app.Run();
