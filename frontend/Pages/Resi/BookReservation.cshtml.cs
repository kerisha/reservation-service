using data;
using data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace frontend.Pages
{
    public class BookReservationModel : PageModel
    {
        private ReservationsContext _context;

        public BookReservationModel(ReservationsContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            ViewData["Message"] = "Book resi page";
        }


        //[BindProperty]
        //public string Username { get; set; }
        [BindProperty]
        public DateTime StartDate { get; set; }
        [BindProperty]
        public DateTime EndDate { get; set; }

        [Authorize]
        public IActionResult OnPost()
        {
            var username = User.Identity.Name;
            var users = _context.Users.ToList();
            var userId = _context.Users.FirstOrDefault(u => u.Name == username).Id;
            var reservation = new data.Models.Reservation { Status = data.Models.ReservationStatus.Confirmed, UserId = userId, Start = DateTime.Parse(Request.Form["start-date"]), End = DateTime.Parse(Request.Form["end-date"]) };
            //_context.Reservations.Add(reservation);
            //_context.SaveChanges();


            // API Call
            var client = new HttpClient();
            var backend_resi_api = Environment.GetEnvironmentVariable("BACKEND_RESI_API");


            client.BaseAddress = new Uri(backend_resi_api);
            var response = client.PostAsJsonAsync("/reservations", reservation).Result;
            if (response.IsSuccessStatusCode)
            {
                return new RedirectToPageResult("/Index");
            }
            else
            {
                return new RedirectToPageResult("/Error");
            }
        }
    }
}
