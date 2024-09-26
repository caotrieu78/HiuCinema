using System;
using System.Collections.Generic;

namespace BookingCinema.Models;

public partial class Movie
{
    public int? MovieId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public int? Duration { get; set; }

    public string? Genre { get; set; }

    public int? Age { get; set; }

    public string? Img { get; set; }

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public virtual ICollection<Showtime> Showtimes { get; set; } = new List<Showtime>();
}
