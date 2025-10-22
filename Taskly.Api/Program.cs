using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Supabase;
using Taskly.Api.Services;
using Taskly.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register SupabaseService
builder.Services.AddSingleton<SupabaseService>();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") // your React dev URL
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

var app = builder.Build();

// Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable CORS
app.UseCors("AllowFrontend");

// Initialize Supabase client
var supa = app.Services.GetRequiredService<SupabaseService>().Client;
await supa.InitializeAsync();

// Helper: extract user ID from header
Guid GetUserId(Microsoft.AspNetCore.Http.HttpRequest req)
{
    var s = req.Headers["x-user-id"].FirstOrDefault();
    return Guid.TryParse(s, out var g) ? g : Guid.Empty;
}

// ---------------- Tasks ----------------
app.MapGet("/api/tasks", async (Microsoft.AspNetCore.Http.HttpRequest req) =>
{
    var userId = GetUserId(req);
    if (userId == Guid.Empty) return Results.Unauthorized();

    var res = await supa.From<TaskItem>().Where(t => t.UserId == userId).Get();
    return Results.Ok(res.Models);
});

app.MapPost("/api/tasks", async (Microsoft.AspNetCore.Http.HttpRequest req, TaskItem task) =>
{
    var userId = GetUserId(req);
    if (userId == Guid.Empty) return Results.Unauthorized();

    task.UserId = userId;
    task.CreatedAt = task.UpdatedAt = DateTime.UtcNow;
    await supa.From<TaskItem>().Insert(task);
    return Results.Ok(task);
});

// ---------------- User Settings ----------------
app.MapGet("/api/settings", async (Microsoft.AspNetCore.Http.HttpRequest req) =>
{
    var userId = GetUserId(req);
    if (userId == Guid.Empty) return Results.Unauthorized();

    var res = await supa.From<UserSettings>().Where(s => s.UserId == userId).Get();
    var settings = res.Models.FirstOrDefault();
    return Results.Ok(settings ?? new UserSettings { UserId = userId, Settings = new Dictionary<string, object>(), UpdatedAt = DateTime.UtcNow });
});

app.MapPost("/api/settings", async (Microsoft.AspNetCore.Http.HttpRequest req, UserSettings incoming) =>
{
    var userId = GetUserId(req);
    if (userId == Guid.Empty) return Results.Unauthorized();

    incoming.UserId = userId;
    incoming.UpdatedAt = DateTime.UtcNow;
    await supa.From<UserSettings>().Upsert(incoming);
    return Results.Ok(incoming);
});

// ---------------- Auth ----------------
app.MapPost("/api/auth/signup", async (Microsoft.AspNetCore.Http.HttpRequest req) =>
{
    var body = await req.ReadFromJsonAsync<Dictionary<string, string>>();
    if (body == null || !body.TryGetValue("email", out var email) || !body.TryGetValue("password", out var password))
        return Results.BadRequest(new { message = "Email and password required" });

    try
    {
        var signUpResult = await supa.Auth.SignUp(email, password);
        if (signUpResult.User != null)
        {
            // UserSettings entry
            await supa.From<UserSettings>().Insert(new UserSettings
            {
                UserId = Guid.Parse(signUpResult.User.Id),
                Settings = new Dictionary<string, object>(),
                UpdatedAt = DateTime.UtcNow
            });

            return Results.Ok(new { user = signUpResult.User.Id, email = signUpResult.User.Email });
        }

        return Results.BadRequest(new { message = "Sign up failed" });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { message = ex.Message });
    }
});

app.MapPost("/api/auth/login", async (Microsoft.AspNetCore.Http.HttpRequest req) =>
{
    var body = await req.ReadFromJsonAsync<Dictionary<string, string>>();
    if (body == null || !body.TryGetValue("email", out var email) || !body.TryGetValue("password", out var password))
        return Results.BadRequest(new { message = "Email and password required" });

    try
    {
        // Sign in
        var session = await supa.Auth.SignInWithPassword(email, password);

        if (session == null || session.User == null)
            return Results.Unauthorized(); // No arguments allowed

        return Results.Ok(new
        {
            user = session.User.Id,
            email = session.User.Email,
            access_token = session.AccessToken
        });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { message = ex.Message });
    }
});




















// ---------------- WeatherForecast ----------------
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}


