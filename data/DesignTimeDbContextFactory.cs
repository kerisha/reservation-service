using data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ReservationsContext>
{
    public ReservationsContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("");
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<ReservationsContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);
            return new ReservationsContext(builder.Options);
        }
}