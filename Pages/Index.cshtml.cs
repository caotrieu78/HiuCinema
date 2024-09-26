using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using BookingCinema.Models; // Thay BookingCinema.Models bằng namespace của DbContext CinemaContext
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingCinema.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly CinemaContext _context; 

        public IndexModel(ILogger<IndexModel> logger, CinemaContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IList<Movie> Movies { get; set; } // Danh sách các phim

        public async Task OnGetAsync()
        {
            // Lấy danh sách phim từ cơ sở dữ liệu và gán vào thuộc tính Movies
            Movies = await _context.Movies.ToListAsync();
        }
    }
}
