using CMS_UI.Dto;
using CMS_UI.Handlers;
using CMS_UI.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CMS_UI.Pages.Admin.Users
{
    public class CreateModel(
        AdminApiHandler _adminApi,
        Session _session
     ) : PageModel
    {



        [BindProperty]
        public Form<UserDto> Form { get; set; } = new Form<UserDto>
        {
            Title = "Create",
            InputName = new List<string> { "Name", "UserName", "Email", "Phone", "ProfileImage", "IsActive", "Role" },
            Buttons = new List<string> { "Create" },
            UserRequest = new(),
            Responses = new()
        };

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
            Form.Responses = await _adminApi.CreateUserAsync(Form.UserRequest);
            Form.Title = "Create";
            Form.InputName = new List<string> { "Name", "UserName", "Email", "Phone", "ProfileImage", "IsActive", "Role" };
            Form.Buttons = new List<string> { "Create" };

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
