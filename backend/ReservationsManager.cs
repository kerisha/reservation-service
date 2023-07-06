using data;
using data.Models;
using Microsoft.EntityFrameworkCore;

namespace backend 
{
    public static class ReservationsManager
    {
       public static void MapReservations(this WebApplication app)
       {
            app.MapGet("/reservations", async (ReservationsContext context) =>
            {
                var reservations = await context.Reservations.ToListAsync();
                return Results.Ok(reservations);
            });

            app.MapGet("/reservations/{id}", async (ReservationsContext context, int id) =>
            {
                var reservation = await context.Reservations.FindAsync(id);
                if (reservation == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(reservation);
            });

            app.MapPost("/reservations", async (ReservationsContext context, Reservation reservation) =>
            {
                await context.Reservations.AddAsync(reservation);
                await context.SaveChangesAsync();
                return Results.Created($"/reservations/{reservation.Id}", reservation);
            });

            app.MapPut("/reservations/{id}", async (ReservationsContext context, int id, Reservation reservation) =>
            {
                var reservationToUpdate = await context.Reservations.FindAsync(id);
                if (reservationToUpdate == null)
                {
                    return Results.NotFound();
                }
                reservationToUpdate.Status = reservation.Status;
                await context.SaveChangesAsync();
                return Results.Ok(reservationToUpdate);
            });

            app.MapDelete("/reservations/{id}", async (ReservationsContext context, int id) =>
            {
                var reservation = await context.Reservations.FindAsync(id);
                if (reservation == null)
                {
                    return Results.NotFound();
                }
                context.Reservations.Remove(reservation);
                await context.SaveChangesAsync();
                return Results.NoContent();
            });
       }
    }
}