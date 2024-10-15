using CMS_UI.Dto;
using CMS_UI.Handlers;
using CMS_UI.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace CMS_UI.Pages.Admin.Roles
{
	public class CreateModel(
		AdminApiHandler _adminApi,
		Session _session
	 ) : PageModel
	{



		[BindProperty]
		public Form<RoleDto> Form { get; set; } = new Form<RoleDto>
		{
			Title = "Create",
			InputName = new List<string> { "Name", "Permissions" },
			Buttons = new List<string> { "Create" },
			UserRequest = new(),
			Responses = new()
		};

		[BindProperty]
		public string Permissions { get; set; } = string.Empty;

		public string Alert { get; set; }

		public ActionResult? OnGet()
		{
			if (_session.Get("Token") is null)
			{
				return RedirectToPage("/Index");
			}

			if (_session.Get("Alert") is not null)
			{
				this.Alert = _session.Get("Alert");

			}

			return null;
		}

		public async Task<ActionResult> OnPostAsync()
		{
			Console.WriteLine("Permissions: " + Permissions);
			// Parse the permissions input and map to RoleDto
			if (!string.IsNullOrWhiteSpace(Permissions))
			{
				var permissionNames = Permissions.Split(',', StringSplitOptions.RemoveEmptyEntries);
				Form.UserRequest.permissions = permissionNames
					.Select(p => new PremissionDto { name = p.Trim() })
					.ToList();
			}

	
			Form.Responses = await _adminApi.CreateRoleAsync(Form.UserRequest);
			Form.Title = "Create";
			Form.InputName = new List<string> { "Name", "Permissions" };
			Form.Buttons = new List<string> { "Create" };

			if (!Form.Responses.ContainsKey("Success"))
			{
				if (Form.Responses.ContainsKey("Alert")) Alert = Form.Responses["Alert"].ToString();

				return Page();
			}

			return RedirectToPage("/Admin/Roles/Index");

		}

		public ActionResult OnPostDismissAlert()
		{
			_session.Remove("Alert");
			return RedirectToPage();
		}
	}
}
