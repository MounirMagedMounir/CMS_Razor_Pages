using CMS_UI.Dto;
using CMS_UI.Handlers;
using CMS_UI.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace CMS_UI.Pages.Admin.Users
{
	public class DeleteModel(
		AdminApiHandler _adminApi,
		Session _session
	 ) : PageModel
	{


		[BindProperty]
		public Form<UserDto> Form { get; set; } = new Form<UserDto>
		{
			Title = "Delete",
			InputName = new List<string> { "Name", "UserName", "Email", "Phone", "Password", "ProfileImage", "IsActive", "Role" },
			Buttons = new List<string> { "Delete" },
			UserRequest = new(),
			Responses = new()
		};

		public static string Id { get; set; }

		public string Alert { get; set; }

		public ActionResult? OnGet(string id)
		{
			Id = id;

			if (_session.Get("Token") is null)
			{
				return RedirectToPage("/Index");
			}

			if (_session.Get("Alert") is not null)
				this.Alert = _session.Get("Alert");

			Form.Responses = _adminApi.GetUserByIdAsync(Id);

			if (!Form.Responses.ContainsKey("Success"))
			{
				if (Form.Responses.ContainsKey("Alert")) Alert = Form.Responses["Alert"].ToString();

				return Page();
			}
			Form.UserRequest = JsonSerializer.Deserialize<UserDto>(Form.Responses["Success"].ToString());

			return null;
		}

		public async Task<ActionResult> OnPostAsync()
		{

			Form.Responses = await _adminApi.DeleteUserAsync(Id);

			Form.Title = "Delete";
			Form.InputName = new List<string> { "Name", "UserName", "Email", "Phone", "Password", "ProfileImage", "IsActive", "Role" };
			Form.Buttons = new List<string> { "Delete" };

			if (!Form.Responses.ContainsKey("Success"))
			{
				if (Form.Responses.ContainsKey("Alert")) Alert = Form.Responses["Alert"].ToString();

				return Page();
			}

			return RedirectToPage("/Admin/Users/Index");

		}

		public ActionResult OnPostDismissAlert()
		{
			_session.Remove("Alert");
			return RedirectToPage();
		}
	}
}

