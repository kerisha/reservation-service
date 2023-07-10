namespace data.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } 
    public string? Email { get; set; } 
    public string? Password { get; set; } 
    public string? Role { get; set; } 
    public string? Token { get; set; } 
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public List<Reservation>? Reservations { get; set; } 

    public User()
    {
    }
}