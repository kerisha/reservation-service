using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using data;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;

namespace frontend.Pages;

[Authorize]
public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private ReservationsContext _context;
    private bool userMapped = false;

    public IndexModel(ILogger<IndexModel> logger, ReservationsContext context)
    {
        _logger = logger;
        _context = context;
    }

    public void OnGet()
    {
        if (!userMapped)
        {
            try
            {
                bool userExists = _context.Users.Any(u => u.Name == User.Identity.Name);
                if (!userExists)
                {
                    var user = new data.Models.User { Name = User.Identity.Name };
                    _context.Users.Add(user);
                    _context.SaveChanges(); 
                }
                userMapped = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occured: {ex}");
            }
        }
        ViewData["LeUser"] = User.Identity.Name;

        ViewData["Tester"] = "Test test";
            var reservations = _context.Reservations.ToList();
            ViewData["Reservations"] = reservations;
    }
  
}
