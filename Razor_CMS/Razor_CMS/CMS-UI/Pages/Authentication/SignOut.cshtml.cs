using CMS_UI.Handlers;
using CMS_UI.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CMS_UI.Pages.Authentication
{
	public class SignOutModel(
		ApiClientHandler _apiClientHandler,
		Session _session
	 ) : PageModel
	{
		public Dictionary<string, object> Responses { get; set; } = new();
		public async Task<ActionResult> OnGet()
		{
			if (_session.Get("Token") is null)
			{
				Console.WriteLine("Token is null  ");
				return RedirectToPage("/Index");
			}

			Responses = await _apiClientHandler.SignOutAsync(_session.Get("Token"));


			if (!Responses.ContainsKey("Success"))
			{
				if (Responses.ContainsKey("Alert")) _session.Set("Alert", Responses["Alert"].ToString());

				return RedirectToPage("/Index");
			}

			Console.WriteLine("Success ");


			_session.Remove("Token");
			_session.Remove("RefreshToken");
			return RedirectToPage("/Authentication/LogIn");
		}
		public ActionResult OnPostDismissAlert()
		{
			_session.Remove("Alert");
			return RedirectToPage();
		}
	}
}
