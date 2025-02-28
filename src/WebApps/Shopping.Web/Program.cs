var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

string baseAddress = builder.Configuration["ApiSettings:GatewayAddress"]!;

builder.Services.AddRefitClient<ICatalogService>()
    .ConfigureHttpClient(client =>
    {
        client.BaseAddress = new Uri(baseAddress);
    });
builder.Services.AddRefitClient<IBasketService>()
    .ConfigureHttpClient(client =>
    {
        client.BaseAddress = new Uri(baseAddress);
    });
builder.Services.AddRefitClient<IOrderingService>()
    .ConfigureHttpClient(client =>
    {
        client.BaseAddress = new Uri(baseAddress);
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
