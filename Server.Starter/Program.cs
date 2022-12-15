using InMemorySampleDatabase;
using Microsoft.EntityFrameworkCore;
using Server.Starter.Components.BzTableTest;
using Server.Starter.Data;
using Server.Starter.Data.Northwind;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Register services
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddScoped<IBzTableDataNorthwindProvider, BzTableDataNorthwindProvider>();

// DbContext
builder.Services.AddDbContext<NorthwindContext>(options => options.UseInMemoryDatabase(databaseName: "Northwind"));

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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<NorthwindContext>();
Configuration.Seed(dbContext);

app.Run();
