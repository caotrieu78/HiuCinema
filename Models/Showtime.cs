using System;
using System.Collections.Generic;

namespace BookingCinema.Models;

public partial class Showtime
{
    public int ShowtimeId { get; set; }

    public int? MovieId { get; set; }

    public int? TheaterId { get; set; }

    public DateTime ShowDateTime { get; set; }

    public virtual Movie? Movie { get; set; }

    public virtual ICollection<SeatBooking> SeatBookings { get; set; } = new List<SeatBooking>();

    public virtual Theater? Theater { get; set; }
}
