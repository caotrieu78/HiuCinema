using System;
using System.Collections.Generic;

namespace BookingCinema.Models;

public partial class Seat
{
    public int SeatId { get; set; }

    public int? TheaterId { get; set; }

    public string? NRow { get; set; }

    public int? NColumn { get; set; }

    public decimal? Price { get; set; }

    public bool IsBooked { get; set; }

    public virtual ICollection<SeatBooking> SeatBookings { get; set; } = new List<SeatBooking>();

    public virtual Theater? Theater { get; set; }

  
}
