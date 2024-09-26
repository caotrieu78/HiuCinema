// Add using directives
using BookingCinema.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookingCinema.Pages.Shared
{
    public class SelectSeatModel : PageModel
    {
        private readonly CinemaContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserDAO _userDAO;

        // Constructor injection
        public SelectSeatModel(CinemaContext context, IHttpContextAccessor httpContextAccessor, UserDAO userDAO)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _userDAO = userDAO;
        }

        [BindProperty]
        public Showtime Showtime { get; set; }

        // GET method for loading the page
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                // Redirect unauthenticated users to login page
                return RedirectToPage("/Login");
            }

            if (id == null)
            {
                return NotFound();
            }

            // Fetch the showtime including related entities
            Showtime = await _context.Showtimes
                 .Include(s => s.Theater)
                 .ThenInclude(t => t.Seats)
                 .FirstOrDefaultAsync(s => s.ShowtimeId == id);

            if (Showtime == null)
            {
                return NotFound();
            }

            // Initialize the seats if not already initialized
            if (Showtime.Theater != null && Showtime.Theater.Seats == null)
            {
                Showtime.Theater.Seats = new List<Seat>();
            }

            return Page();
        }


        // POST method for booking seats
        public async Task<IActionResult> OnPostBookSeatsAsync(int? showtimeId, int[] seatIds)
        {
            if (showtimeId == null || seatIds == null || seatIds.Length == 0)
            {
                ModelState.AddModelError("", "Please select at least one seat.");
                return Page();
            }

            Showtime = await _context.Showtimes
                .Include(s => s.Theater)
                .FirstOrDefaultAsync(s => s.ShowtimeId == showtimeId);

            if (Showtime == null)
            {
                return NotFound();
            }

            int userId = GetUserId();

            if (userId == 0)
            {
                return RedirectToPage("/Login", "/Home");
            }

            try
            {
                foreach (var seatId in seatIds)
                {
                    var isSeatBooked = await _context.SeatBookings.AnyAsync(sb => sb.ShowtimeId == showtimeId && sb.SeatId == seatId);
                    if (isSeatBooked)
                    {
                        ModelState.AddModelError("", $"Seat {seatId} is already booked.");
                        return Page();
                    }

                    var seat = await _context.Seats.FirstOrDefaultAsync(s => s.SeatId == seatId);
                    if (seat != null)
                    {
                        seat.IsBooked = true;
                    }

                    var seatBooking = new SeatBooking
                    {
                        ShowtimeId = showtimeId.Value,
                        SeatId = seatId,
                        UserId = userId,
                        BookingDate = DateTime.Now
                    };
                    _context.SeatBookings.Add(seatBooking);
                }

                await _context.SaveChangesAsync();

                return RedirectToPage("/ViewBookings");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while processing your request.");
                return Page();
            }
        }

        // Helper method to get user ID
        private int GetUserId()
        {
            if (_httpContextAccessor.HttpContext.User.Identity is ClaimsIdentity identity && identity.IsAuthenticated)
            {
                var userIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                {
                    return userId;
                }
            }

            return 0;
        }
    }
}
