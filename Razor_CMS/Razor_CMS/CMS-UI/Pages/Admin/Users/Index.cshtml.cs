using CMS_UI.FilterModels;
using CMS_UI.Dto;
using CMS_UI.Handlers;
using CMS_UI.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Reflection.Metadata;

namespace CMS_UI.Pages.Admin.Users
{
    public class IndexModel(Session _session,
        AdminApiHandler _adminApi,
        IHttpContextAccessor _httpContext) : PageModel
    {
        [BindProperty]
        public List<UserDto> Users { get; set; } = new();

        public Dictionary<string, object> Responses { get; set; } = new();

        [TempData]
        public List<string> SelectedIds { get; set; }

        public string Alert { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public int Take { get; set; } = 3;

        [BindProperty(SupportsGet = true)]
        public string SortBy { get; set; } = "name";

        [BindProperty(SupportsGet = true)]
        public string SortOrder { get; set; } = "asc";

        [BindProperty(SupportsGet = true)]
        public UsersFilter FilterParams { get; set; } = new();

        public IActionResult OnGet()
        {
            if (_session.Get("Token") is null)
                return RedirectToPage("/Index");

            Alert = _session.Get("Alert");

            if (!_httpContext.HttpContext.Request.QueryString.HasValue)
                return RedirectToPage("/Admin/Users/Index", new { PageNumber, Take, SortBy, SortOrder });

            SetViewData();

            Responses = _adminApi.GetUsersAsync(FilterParams, PageNumber, Take, SortBy, SortOrder);

            if (Responses.TryGetValue("Alert", out var alert))
            {
                Alert = alert.ToString();
                ViewData["Count"] = 1;
                ViewData["Take"] = 3;
                return Page();
            }

            Users = JsonSerializer.Deserialize<List<UserDto>>(Responses["Success"].ToString());

            int.TryParse(Responses["Count"].ToString(), out int count);
            ViewData["Count"] = count;

            int.TryParse(Responses["Take"].ToString(), out int take);
            ViewData["Take"] = take;

            SetUserDetails();

            return null;
        }

        public async Task<IActionResult> OnPostDeleteSelectedAsync([FromForm] string selectedIdsJson)
        {
            Console.WriteLine("delete start");

            var selectedIds = JsonSerializer.Deserialize<List<string>>(selectedIdsJson);

            var response = await _adminApi.DeleteUsersAsync(selectedIds);

            Console.WriteLine("deleted " + response.ToString());

            if (!response.ContainsKey("Success") && response.TryGetValue("Alert", out var alert))
                Alert = alert.ToString();



            return RedirectToPage();
        }

        public IActionResult OnPostDismissAlert()
        {
            _session.Remove("Alert");
            return RedirectToPage();
        }

        private void SetViewData()
        {
            var parameterNames = typeof(UsersFilter).GetProperties().Select(p => p.Name).ToList();
            ViewData["Count"] = 1;
            ViewData["Take"] = 3;
            ViewData["parameter"] = parameterNames;
            ViewData["sortBy"] = SortBy;
            ViewData["sortOrder"] = SortOrder;
            ViewData["PageNumber"] = PageNumber;
            ViewData["Headers"] = new List<string>
            {
                "ProfileImage", "Name", "UserName", "Email", "Phone", "Password", "IsActive", "Role", "CreatedDate", "LastUpdatedDate", "CreatedById", "LastUpdatedById"
            };
            ViewData["Buttons"] = new List<string> { "Edit", "Delete" };


        }

        private void SetUserDetails()
        {
            foreach (var user in Users)
            {
                user.createdbyId = GetUserById(user.createdbyId);
                user.lastUpdatedbyId = GetUserById(user.lastUpdatedbyId);
            }

        }

        private string GetUserById(string userId)
        {
            Responses = _adminApi.GetUserByIdAsync(userId);
            var user = JsonSerializer.Deserialize<UserDto>(Responses["Success"].ToString());
            return user?.name;
        }
    }

}