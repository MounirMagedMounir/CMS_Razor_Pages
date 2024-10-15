using CMS_UI.Dto;
using CMS_UI.Handlers;
using CMS_UI.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CMS_UI.Pages.Authentication
{
	public class RegisterModel(
		ApiClientHandler _apiClientHandler,
		Session _session
	 ) : PageModel
	{



		[BindProperty]
		public Form<RegisterDto> Form { get; set; } = new Form<RegisterDto>
		{
			Title = "Register",
			InputName = new List<string> { "Name", "UserName", "Email", "Phone", "Password", "ConfirmPassword" },
			Buttons = new List<string> { "Register" },
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

			Form.Responses = await _apiClientHandler.RegisterAsync(Form.UserRequest);

			Form.Title = "Register";
			Form.InputName = new List<string> { "Name", "UserName", "Email", "Phone", "Password", "ConfirmPassword" };
			Form.Buttons = new List<string> { "Register" };


			if (!Form.Responses.ContainsKey("Success"))
			{
				if (Form.Responses.ContainsKey("Alert")) Alert = Form.Responses["Alert"].ToString();

				return Page();
			}

			return RedirectToPage("EmailVerification", new { Form.UserRequest.Email });

		}
		public ActionResult OnPostDismissAlert()
		{
			_session.Remove("Alert");
			return RedirectToPage();
		}
	}
}
