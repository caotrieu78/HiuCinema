using System;
using System.Security.Claims;
using System.Threading.Tasks;
using BookingCinema.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookingCinema.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly CinemaContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DetailsModel(CinemaContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public Movie Movie { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Movie = await _context.Movies
                .Include(t => t.Ratings)
                .ThenInclude(r => r.User)
                .FirstOrDefaultAsync(m => m.MovieId == id);

            if (Movie == null)
            {
                return NotFound();
            }

            return Page();
        }


        public async Task<IActionResult> OnPostAsync(int movieId, int ratingValue, string comment)
        {
            if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                // Người dùng chưa đăng nhập, chuyển hướng đến trang đăng nhập
                return RedirectToAction("Login");
            }

            // Kiểm tra xem movieId có tồn tại trong bảng Movies không
          
            // Lấy thông tin người dùng từ Claims
            var userId = GetUserId();

            // Tạo một đối tượng Rating mới
            var rating = new Rating
            {
                MovieId = movieId,
                UserId = userId,
                RatingValue = ratingValue,
                Comment = comment,
                RatingDate = DateTime.Now
            };

            // Thêm đánh giá vào cơ sở dữ liệu
            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Details", new { id = movieId });
        }


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
