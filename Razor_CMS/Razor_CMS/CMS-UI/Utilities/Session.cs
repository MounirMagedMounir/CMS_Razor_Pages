namespace CMS_UI.Utilities
{
	public class Session
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public Session(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		private HttpContext HttpContext => _httpContextAccessor.HttpContext;

		public void Set(string key, string value)
		{
			HttpContext.Session.SetString(key, value);

		}

		public string Get(string key)
		{
			return HttpContext.Session.GetString(key);
		}

		public void Remove(string key)
		{

			HttpContext.Session.Remove(key);
		}
	}
}
