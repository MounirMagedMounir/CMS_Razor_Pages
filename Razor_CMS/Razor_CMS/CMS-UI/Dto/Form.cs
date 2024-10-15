namespace CMS_UI.Dto
{
	public class Form<TModel>
	{
		public TModel UserRequest { get; set; }
		public Dictionary<string, object> Responses { get; set; } = new();
		public List<string> InputName { get; set; } = new();
		public List<string> Buttons { get; set; } = new();
		public string Title { get; set; } = "";

	}
}
