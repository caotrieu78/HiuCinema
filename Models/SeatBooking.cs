using System;
using System.Collections.Generic;
namespace BookingCinema.Models;
public partial class SeatBooking
{
    public int BookingId { get; set; }

    public int? ShowtimeId { get; set; }

    public int? SeatId { get; set; }

    public int? UserId { get; set; }

    public DateTime BookingDate { get; set; }

    public virtual Seat? Seat { get; set; }

    public virtual Showtime? Showtime { get; set; }

    public virtual User? User { get; set; }
}
