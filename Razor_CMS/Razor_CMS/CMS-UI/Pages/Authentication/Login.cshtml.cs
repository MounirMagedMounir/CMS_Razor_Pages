using CMS_UI.Dto;
using CMS_UI.Handlers;
using CMS_UI.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CMS_UI.Pages.Authentication
{
	public class LoginModel(
		ApiClientHandler _apiClientHandler,
		Session _session
	 ) : PageModel
	{

		[BindProperty]
		public Form<LoginDto> Form { get; set; } = new Form<LoginDto>
		{
			Title = "Log in",
			InputName = new List<string> { "Email", "Password" },
			Buttons = new List<string> { "Log in" },
			UserRequest = new(),
			Responses = new()
		};

		public string Alert { get; set; }


		public ActionResult? OnGet()
		{
			if (_session.Get("Token") is not null)
			{
				return RedirectToPage("/Index");
			}

			if (_session.Get("Alert") is not null)
				this.Alert = _session.Get("Alert");

			return null;
		}

		public async Task<ActionResult> OnPostAsync()
		{
			Form.Responses = await _apiClientHandler.LogInAsync(Form.UserRequest);

			Form.Title = "Log in";
			Form.InputName = new List<string> { "Email", "Password" };
			Form.Buttons = new List<string> { "Log in" };


			if (!Form.Responses.ContainsKey("Success"))
			{
				if (Form.Responses.TryGetValue("Alert", out var alert))
					Alert = alert.ToString();

				return Page();
			}

			_session.Set("Token", Form.Responses["Token"].ToString());
			_session.Set("RefreshToken", Form.Responses["RefreshToken"].ToString());

			return RedirectToPage("/Index");

		}

		public ActionResult OnPostDismissAlert()
		{
			_session.Remove("Alert");
			return RedirectToPage();
		}
	}
}
