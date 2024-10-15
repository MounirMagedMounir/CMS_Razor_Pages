using System.Text.Json.Serialization;

namespace CMS_UI.Dto
{
	public class GeneralRespons
	{
		[JsonPropertyName("key")]
		public string? Key { get; set; }

		[JsonPropertyName("value")]
		public object Value { get; set; }
	}
}
