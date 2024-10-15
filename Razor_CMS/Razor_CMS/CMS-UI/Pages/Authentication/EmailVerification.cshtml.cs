using CMS_UI.Dto;
using CMS_UI.Handlers;
using CMS_UI.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CMS_UI.Pages.Authentication
{
	public class EmailVerificationModel(
		ApiClientHandler _apiClientHandler,
		Session _session
	 )
		 : PageModel
	{
		[BindProperty]
		public string VerificationCode { get; set; }


		public static VerificationEmailDto Verification { get; set; } = new();

		public string Alert { get; set; }

		public Dictionary<string, object> Responses { get; set; } = new();


		public ActionResult? OnGet(string Email)
		{
			if (_session.Get("Token") is not null)
			{
				return RedirectToPage("/Index");
			}
			Verification.Email = Email;

			return null;
		}


		public async Task<ActionResult> OnPostAsync()
		{
			Verification.VerificationCode = VerificationCode;


			Responses = await _apiClientHandler.EmailVerificationAsync(Verification);


			if (!Responses.ContainsKey("Success"))
			{

				if (Responses.ContainsKey("Alert")) Alert = Responses["Alert"].ToString();
				return Page();
			}


			return RedirectToPage("Login");
		}

		public ActionResult OnPostDismissAlert()
		{
			_session.Remove("Alert");
			return RedirectToPage();
		}
	}
}
