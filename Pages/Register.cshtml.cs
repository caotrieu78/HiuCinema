using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookingCinema.Models;
using System.Text.RegularExpressions;

namespace BookingCinema.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly UserDAO _userDAO;
        public string Message { get; set; }
        public RegisterModel(UserDAO userDAO)
        {
            _userDAO = userDAO;
        }

        [BindProperty]
        public User User { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            // Kiểm tra xem người dùng đã tồn tại hay chưa
            if (_userDAO.GetUserByEmail(User.Email) != null)
            {
                ModelState.AddModelError("User.Email", "Email Đã Có Rồi");
                return Page();
            }

            // Kiểm tra xem username có để trống hay không
            if (string.IsNullOrWhiteSpace(User.Username))
            {
                ModelState.AddModelError("User.Username", "Username is required");
                return Page();
            }

            // Kiểm tra định dạng email
            if (!IsValidEmail(User.Email))
            {
                ModelState.AddModelError("User.Email", "Invalid email format");
                return Page();
            }
        

            // Kiểm tra và gán giá trị mặc định cho IsAdmin và Role
            User.Role = User.Role ?? "khách";

            // Thêm người dùng mới vào cơ sở dữ liệu
            _userDAO.AddUser(User);
            Message = "Đăng Ký Thành Công! <i class=\"bi bi-emoji-smile-fill\"></i>";
            return RedirectToPage("/Login", new { message = Message });
        }

        private bool IsValidEmail(string email)
        {
            // Sử dụng regular expression để kiểm tra định dạng email
            string pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            return Regex.IsMatch(email, pattern);
        }
    }
}
