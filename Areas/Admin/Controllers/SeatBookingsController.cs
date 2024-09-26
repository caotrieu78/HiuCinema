using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookingCinema.Models;

namespace BookingCinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SeatBookingsController : Controller
    {
        private readonly CinemaContext _context;

        public SeatBookingsController(CinemaContext context)
        {
            _context = context;
        }

        // GET: Admin/SeatBookings
        public async Task<IActionResult> Index()
        {
            var cinemaContext = _context.SeatBookings.Include(s => s.Seat).Include(s => s.Showtime).Include(s => s.User);
            return View(await cinemaContext.ToListAsync());
        }

        // GET: Admin/SeatBookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SeatBookings == null)
            {
                return NotFound();
            }

            var seatBooking = await _context.SeatBookings
                .Include(s => s.Seat)
                .Include(s => s.Showtime)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.BookingId == id);
            if (seatBooking == null)
            {
                return NotFound();
            }

            return View(seatBooking);
        }

        // GET: Admin/SeatBookings/Create
        public IActionResult Create()
        {
            ViewData["SeatId"] = new SelectList(_context.Seats, "SeatId", "SeatId");
            ViewData["ShowtimeId"] = new SelectList(_context.Showtimes, "ShowtimeId", "ShowtimeId");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: Admin/SeatBookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingId,ShowtimeId,SeatId,UserId,BookingDate")] SeatBooking seatBooking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(seatBooking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SeatId"] = new SelectList(_context.Seats, "SeatId", "SeatId", seatBooking.SeatId);
            ViewData["ShowtimeId"] = new SelectList(_context.Showtimes, "ShowtimeId", "ShowtimeId", seatBooking.ShowtimeId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", seatBooking.UserId);
            return View(seatBooking);
        }

        // GET: Admin/SeatBookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SeatBookings == null)
            {
                return NotFound();
            }

            var seatBooking = await _context.SeatBookings.FindAsync(id);
            if (seatBooking == null)
            {
                return NotFound();
            }
            ViewData["SeatId"] = new SelectList(_context.Seats, "SeatId", "SeatId", seatBooking.SeatId);
            ViewData["ShowtimeId"] = new SelectList(_context.Showtimes, "ShowtimeId", "ShowtimeId", seatBooking.ShowtimeId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", seatBooking.UserId);
            return View(seatBooking);
        }

        // POST: Admin/SeatBookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingId,ShowtimeId,SeatId,UserId,BookingDate")] SeatBooking seatBooking)
        {
            if (id != seatBooking.BookingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seatBooking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeatBookingExists(seatBooking.BookingId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SeatId"] = new SelectList(_context.Seats, "SeatId", "SeatId", seatBooking.SeatId);
            ViewData["ShowtimeId"] = new SelectList(_context.Showtimes, "ShowtimeId", "ShowtimeId", seatBooking.ShowtimeId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", seatBooking.UserId);
            return View(seatBooking);
        }

        // GET: Admin/SeatBookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SeatBookings == null)
            {
                return NotFound();
            }

            var seatBooking = await _context.SeatBookings
                .Include(s => s.Seat)
                .Include(s => s.Showtime)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.BookingId == id);
            if (seatBooking == null)
            {
                return NotFound();
            }

            return View(seatBooking);
        }

        // POST: Admin/SeatBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SeatBookings == null)
            {
                return Problem("Entity set 'CinemaContext.SeatBookings' is null.");
            }

            var seatBooking = await _context.SeatBookings.FindAsync(id);
            if (seatBooking == null)
            {
                return NotFound();
            }

            // Update the seat's IsBooked status to 0
            var seat = await _context.Seats.FindAsync(seatBooking.SeatId);
            if (seat != null)
            {
                seat.IsBooked = false;
            }

            // Remove the booking
            _context.SeatBookings.Remove(seatBooking);

            // Save changes
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        private bool SeatBookingExists(int id)
        {
          return (_context.SeatBookings?.Any(e => e.BookingId == id)).GetValueOrDefault();
        }
    }
}
