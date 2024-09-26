using System;
using System.Collections.Generic;

namespace BookingCinema.Models;

public partial class Rating
{
    public int RatingId { get; set; }

    public int? MovieId { get; set; }

    public int? UserId { get; set; }

    public int RatingValue { get; set; }

    public string? Comment { get; set; }

    public DateTime? RatingDate { get; set; }

    public virtual Movie? Movie { get; set; }

    public virtual User? User { get; set; }
}
