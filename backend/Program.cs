using System.Diagnostics;
using backend;
using backend.interfaces;
using backend.services;
using data;
using Microsoft.EntityFrameworkCore;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHsts(options =>
{
    options.Preload = true;
    options.IncludeSubDomains = true;
    options.MaxAge = TimeSpan.FromDays(60);
    options.ExcludedHosts.Add("example.com");
    options.ExcludedHosts.Add("www.example.com");
});

builder.Services.AddHttpsRedirection(options =>
{
    options.RedirectStatusCode = (int)HttpStatusCode.TemporaryRedirect;
    options.HttpsPort = 7001;
});

// var config = new ConfigurationBuilder()
// .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json")
// .Build();

var config = new ConfigurationBuilder()
.AddJsonFile($"appsettings.json")
.Build();



string bookingsConnectionString = string.Empty;

try
{
    bookingsConnectionString = Environment.GetEnvironmentVariable("ABCGuestHouseConnString");
    if(string.IsNullOrEmpty(bookingsConnectionString))
    {
        bookingsConnectionString = config.GetConnectionString("DefaultConnection");
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

// DI
builder.Services.AddSingleton<IMessageService, MessageService>();

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
