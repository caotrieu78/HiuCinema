using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookingCinema.Pages.Checkout
{
	public class SuccessModel : PageModel
	{
		public void OnGet()
		{
		}

		[Authorize]
		public IActionResult OnGetPaymentSuccess()
		{
			return RedirectToPage("Success");
		}
	}
}
