using AspNetWeek1.Api.Services;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();


builder.Services.AddSingleton<CustomerService>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild",
    "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast(
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();

    return forecast;
});

app.MapGet("/health", () => new { status = "OK" });

app.MapGet("/env", (IWebHostEnvironment env) =>
{
    return new
    {
        Environment = env.EnvironmentName
    };
});

app.MapGet("/config", (IConfiguration config) =>
{
    return new
    {
        AppName = config["AppSettings:AppName"],
        BaseUrl = config["AppSettings:BaseUrl"]
    };
});

app.MapGet("/", () => "API is running");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}