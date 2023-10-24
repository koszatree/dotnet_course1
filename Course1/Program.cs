using Course1;
using Course1.Properties.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.IsEssential = true;
});

builder.Services.Configure<FruitOptions>(options =>
{
    options.Name = "watermelon";
});

builder.Services.AddSingleton<IResponseFormatter, HtmlResponseFormatter>();
builder.Services.AddScoped<IResponseFormatter, GuidService>();

var app = builder.Build();

app.UseSession();

app.MapGet("/formatter1", async (HttpContext context, IResponseFormatter formatter) =>
{
    await formatter.Format(context, "Formatter 1");
});

app.MapGet("/formatter2", async (HttpContext context, IResponseFormatter formatter) =>
{
    await formatter.Format(context, "Formatter 2");
});

app.UseMiddleware<CustomMiddleware>();

app.MapGet("/endpoint", CustomEndpoint.Endpoint);

app.UseMiddleware<FruitMiddleware>();

app.MapGet("/config", async (HttpContext context, IConfiguration config) =>
{
    string defaultDebug = config["Logging:LogLevel:Default"];
    await context.Response.WriteAsync(defaultDebug);

    string environment = config["ASPNETCORE_ENVIRONMENT"];
    await context.Response.WriteAsync(environment);

    if (app.Environment.IsDevelopment())
    {
        await context.Response.WriteAsync("\nisDevelopment");
    }
});

app.MapGet("/cookie", async context =>
{
    int counter = int.Parse(context.Request.Cookies["counter"] ?? "0") + 1;

    context.Response.Cookies.Append(
            "counter",
            counter.ToString(),
            new CookieOptions
            {
                MaxAge = TimeSpan.FromMinutes(30)
            }
        );

    await context.Response.WriteAsync($"Cookie: {counter}");
});

app.MapGet("/clear", context =>
{
    context.Response.Cookies.Delete("counter");
    context.Response.Redirect("/");
    return Task.CompletedTask;
});

app.MapGet("/session", async context =>
{
    int sessionCounter = (context.Session.GetInt32("counter") ?? 0) + 1;

    context.Session.SetInt32("counter", sessionCounter);

    await context.Session.CommitAsync();

    await context.Response.WriteAsync($"Session: {sessionCounter}");
});

app.MapGet("/", () => "Hello World!");

app.Run();
