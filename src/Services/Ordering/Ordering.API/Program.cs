var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
     .AddApplicationServices(builder.Configuration)
     .AddInfrastructureServices(builder.Configuration)
     .AddApiServices(builder.Configuration);

var app = builder.Build();

// configure the HTTP request pipeline.
app.UseApiServices();

if (app.Environment.IsDevelopment())
{
    await app.InitializeDatabaseAsync();
}

app.Run();
