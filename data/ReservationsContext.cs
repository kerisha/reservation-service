using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace data;
public class ReservationsContext : IdentityDbContext
{
    private readonly IConfiguration _configuration;
    public DbSet<Models.Reservation> Reservations { get; set; } = default!;
    public DbSet<Models.User> Users { get; set; } = default!;

    public ReservationsContext(DbContextOptions<ReservationsContext> options) : base(options)
    {
    }

}
