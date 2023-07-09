namespace data.Models;

public class Reservation
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public ReservationStatus Status { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }

    public Reservation()
    {
    }
}

public enum ReservationStatus
{
    Pending,
    Confirmed,
    Canceled,
    Completed
}
