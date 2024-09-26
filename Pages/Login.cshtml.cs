using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Collections.Generic;
using System.Security.Claims;
using BookingCinema.Models;

namespace BookingCinema.Pages
{
    public class LoginModel : PageModel
    {
        private readonly UserDAO _userDAO;

        public LoginModel(UserDAO userDAO)
        {
            _userDAO = userDAO;
        }
        public string Message { get; set; }
        [BindProperty]
        public User User { get; set; }
        public IActionResult OnGet(string message)
        {
            Message = message;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            User existingUser = _userDAO.GetUserByEmail(User.Email);

            if (existingUser == null)
            {
                ModelState.AddModelError("LoginError", "Không Đúng Mật Khẩu Hoặc Email");
                return Page();
            }

            if (existingUser.Password != User.Password)
            {
                ModelState.AddModelError("LoginError", "Nhập Sai Mật Khẩu");
                return Page();
            }

            // If email and password are correct, proceed with authentication
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, existingUser.UserId.ToString()),
        new Claim(ClaimTypes.Name, existingUser.FullName)
    };

            if (existingUser.Role == "Admin")
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)));
                return RedirectToAction("Index", "Admin");
            }

            await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)));
            TempData["Message"] = "Đăng Nhập Thành Công! <i class=\"bi bi-emoji-smile-fill\"></i>";
            return RedirectToPage("/Index");
        }

    }
}
