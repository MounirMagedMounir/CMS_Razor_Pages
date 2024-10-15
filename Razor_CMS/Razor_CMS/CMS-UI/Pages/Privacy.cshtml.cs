using CMS_UI.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CMS_UI.Pages
{
    public class PrivacyModel(Session _session) : PageModel
    {
        public string Alert { get; set; }

        public void OnGet()
        {
            if (_session.Get("Alert") is not null)
                this.Alert = _session.Get("Alert");

        }
        public ActionResult OnPostDismissAlert()
        {
            _session.Remove("Alert");
            return RedirectToPage();
        }
    }

}
