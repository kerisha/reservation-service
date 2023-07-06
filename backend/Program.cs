using System.Diagnostics;
using backend;
using data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder()
.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json")
.Build();

string bookingsConnectionString = string.Empty;

try
{
    bookingsConnectionString = config.GetConnectionString("DefaultConnection");
    if(string.IsNullOrEmpty(bookingsConnectionString))
    {
        bookingsConnectionString = Environment.GetEnvironmentVariable("ShopDbConnection");
    }
}
catch(Exception ex)
{
    Trace.WriteLine($"Exception occurred: {ex}");
}

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var qat = Environment.GetEnvironmentVariable("QAT");

if (string.IsNullOrEmpty(qat))
{
    try
    {
        // Register Db Dependency
        builder
            .Services
            .AddDbContext<ReservationsContext>(opt => opt.UseSqlServer(bookingsConnectionString));
        Console.WriteLine("Successful connection to the database");
    }
    catch (Exception ex)
    {
        Trace.WriteLine($"An exception occured while setting up Db Context: {ex}");
        Console.WriteLine($"An exception occured while setting up Db Context: {ex}");
    }
}

// Add a health check!!
builder.Services.AddHealthChecks();

var app = builder.Build();

app.MapHealthChecks("/health");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapReservations();

app.Run();

public partial class Program { }
