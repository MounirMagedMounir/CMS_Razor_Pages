using CMS_UI.Dto;
using CMS_UI.Handlers;
using CMS_UI.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace CMS_UI.Pages.Admin.Roles
{
	public class EditModel(
		AdminApiHandler _adminApi,
		Session _session
	 ) : PageModel
	{

		public static string Id { get; set; }

		public string Alert { get; set; }

		[BindProperty]
		public Form<RoleDto> Form { get; set; } = new Form<RoleDto>
		{
			Title = "Edit",
			InputName = new List<string> { "Name", "Permissions" },
			Buttons = new List<string> { "Edit" },
			UserRequest = new(),
			Responses = new()
		};

		[BindProperty]
		public List<string> Permissions { get; set; }
		public ActionResult? OnGet(string id)
		{
			Id = id;

			if (_session.Get("Token") is null)
			{
				return RedirectToPage("/Index");
			}

			if (_session.Get("Alert") is not null)
				this.Alert = _session.Get("Alert");

			Form.Responses = _adminApi.GetRoleByIdAsync(Id);

			if (!Form.Responses.ContainsKey("Success"))
			{
				if (Form.Responses.ContainsKey("Alert"))
					Alert = Form.Responses["Alert"].ToString();

				return Page();
			}

			Form.UserRequest = JsonSerializer.Deserialize<RoleDto>(Form.Responses["Success"].ToString());

			Form.UserRequest.permissions.Add(new PremissionDto());

			return null;
		}

		public async Task<ActionResult> OnPostAsync()
		{

			// Parse the permissions input and map to RoleDto
			// Ensure permissions is initialized
			if (Form.UserRequest.permissions == null)
			{
				Form.UserRequest.permissions = new List<PremissionDto>();
			}

			foreach (var permission in Permissions)
			{
				Console.WriteLine("Permissions: " + permission);
				// Now it is safe to use .Any() as permissions is guaranteed to be initialized
				if (!Form.UserRequest.permissions.Any(p => p.name == permission) && permission != null)
				{
					Form.UserRequest.permissions.Add(new PremissionDto { name = permission });
				}
			}


			// Print the UserRequest for debugging
			Console.WriteLine("UserRequest: " + JsonSerializer.Serialize(Form.UserRequest));



			Form.UserRequest.id = Id;
			Form.Responses = await _adminApi.UpdateRoleAsync(Form.UserRequest);

			Form.Title = "Edit";
			Form.InputName = new List<string> { "Name", "Permissions" };
			Form.Buttons = new List<string> { "Edit" };

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

