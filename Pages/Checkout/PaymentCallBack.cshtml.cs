using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookingCinema.Services;
using BookingCinema.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;// Add the appropriate namespace for your DbContext

namespace BookingCinema.Pages.Checkout
{
    public class PaymentCallBackModel : PageModel
    {
        private readonly IVnPayService _vnPayService;
        private readonly CinemaContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PaymentCallBackModel(IVnPayService vnPayService, CinemaContext context, IHttpContextAccessor httpContextAccessor)
        {
            _vnPayService = vnPayService;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult OnGet()
        {
            try
            {
                var response = _vnPayService.PaymentExecute(Request.Query);

                if (response == null || response.VnPayResponseCode != "00")
                {
                    TempData["Message"] = $"Lỗi thanh toán VN Pay: {response?.VnPayResponseCode ?? "Không có mã phản hồi từ VN Pay"}";
                    return RedirectToPage("/Checkout/PaymentFail");
                }

                // Save the order to the database here

                // Delete booked seats after successful payment
                int userId = GetUserId(); // Assuming you have a method to get the user ID
                var bookedSeats = _context.SeatBookings.Where(sb => sb.UserId == userId);
                _context.SeatBookings.RemoveRange(bookedSeats);
                _context.SaveChanges();

                TempData["Message"] = "Thanh toán VNPay thành công";
                return RedirectToPage("/Checkout/Success");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                TempData["Message"] = "Có lỗi xảy ra khi xử lý thanh toán";
                return RedirectToPage("/Checkout/PaymentFail");
            }
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
