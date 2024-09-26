namespace BookingCinema.Models
{
    public class MovieDAO
    {
        private CinemaContext _context;

        public MovieDAO(CinemaContext context)
        {
            _context = context;
        }
        public Movie GetTourById(int? id)
        {
            return _context.Movies.FirstOrDefault(u => u.MovieId == id);
        }
        public Movie GetTourDetail(int? id)
        {
            return _context.Movies
                
                .FirstOrDefault(t => t.MovieId == id);
        }
    }
}

