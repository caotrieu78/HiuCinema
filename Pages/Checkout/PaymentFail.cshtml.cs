using BookingCinema.Models;
using BookingCinema.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace BookingCinema.Pages.Checkout
{
	public class PaymentFailModel : PageModel
	{
		private readonly CinemaContext _context;
		private readonly IVnPayService _vnPayservice;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public PaymentFailModel(IVnPayService vnPayservice, IHttpContextAccessor httpContextAccessor, CinemaContext context)
		{
			_vnPayservice = vnPayservice;
			_httpContextAccessor = httpContextAccessor;
			_context = context;
		}

		public void OnGet()
		{
		}

		public IActionResult OnGetPaymentFail()
		{
			return Page();
		}

		
	}
}
