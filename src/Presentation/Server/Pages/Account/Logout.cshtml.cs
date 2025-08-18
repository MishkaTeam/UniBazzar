using Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Server.Infrastructure.Messages;

namespace Server.Pages.Account
{
	[Microsoft.AspNetCore.Authorization.Authorize]
	public class LogoutModel : BasePageModel
	{
		public LogoutModel() : base()
		{
		}

		public async Task<IActionResult> OnGetAsync()
		{
			// SignOutAsync -> using Microsoft.AspNetCore.Authentication;
			await HttpContext.SignOutAsync();

			return RedirectToPage(pageName: "/Index");
		}
	}
}
