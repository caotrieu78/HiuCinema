using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace BookingCinema.Pages
{
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            await HttpContext.SignOutAsync(); // Đăng xuất người dùng

            TempData["Message"] = "Bạn Đã Đăng Xuất! <i class=\"bi bi-emoji-smile-fill\"></i>";
            return RedirectToPage("/Index");
        }
    }
}
