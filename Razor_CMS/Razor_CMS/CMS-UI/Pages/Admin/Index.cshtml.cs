using CMS_UI.Handlers;
using CMS_UI.Utilities;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CMS_UI.Pages.Admin
{
	public class IndexModel(Session _session,
		AdminApiHandler _adminApi) : PageModel
	{

		public Dictionary<string, object> Responses { get; set; } = new();

		public void OnGet()
		{

		}
	}
}
