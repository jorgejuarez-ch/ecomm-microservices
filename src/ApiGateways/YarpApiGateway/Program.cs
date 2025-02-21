using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
builder.Services.AddRateLimiter(rateLimiterOptions =>
    {
        rateLimiterOptions.AddFixedWindowLimiter("fixed-10-5", options =>
        {
            options.Window = TimeSpan.FromSeconds(10);
            options.PermitLimit = 5;
        });
    });

var app = builder.Build();

// configure the HTTP request pipeline.
app.UseRateLimiter();
app.MapReverseProxy();

app.Run();
