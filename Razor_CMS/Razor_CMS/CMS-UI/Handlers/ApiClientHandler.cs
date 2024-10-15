using CMS_UI.Dto;
using System.Text.Json;

namespace CMS_UI.Handlers
{
	public class ApiClientHandler
	{
		private readonly HttpClient _httpClient;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public ApiClientHandler(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
			_httpClient = httpClient;
		}

		public async Task<Dictionary<string, object>> RegisterAsync(RegisterDto UserRequest)
		{

			UserRequest.Name ??= "";
			UserRequest.Email ??= "";
			UserRequest.Phone ??= "";
			UserRequest.UserName ??= "";
			UserRequest.Password ??= "";
			UserRequest.ConfirmPassword ??= "";

			var jsonContent = new StringContent(
							 JsonSerializer.Serialize(UserRequest),
														 System.Text.Encoding.UTF8,
														 "application/json");

			var response = await _httpClient.PostAsync("https://localhost:7113/Authentication/register", jsonContent);

			var jsonResponse = await response.Content.ReadAsStringAsync();

			Console.WriteLine("Raw JSON response: " + jsonResponse);

			var generalResponses = JsonSerializer.Deserialize<List<GeneralRespons>>(jsonResponse);


			Dictionary<string, object> Responses = new();

			foreach (var pair in generalResponses)
			{

				Responses[pair.Key] = pair.Value;
			}

			return Responses;

		}

		public async Task<Dictionary<string, object>> EmailVerificationAsync(VerificationEmailDto UserRequest)
		{

			UserRequest.Email ??= "";
			UserRequest.VerificationCode ??= "";
			var jsonContent = new StringContent(
									 JsonSerializer.Serialize(UserRequest),
																 System.Text.Encoding.UTF8,
																 "application/json");

			var response = await _httpClient.PostAsync("https://localhost:7113/Authentication/EmailVerification", jsonContent);

			var jsonResponse = await response.Content.ReadAsStringAsync();

			Console.WriteLine("Raw JSON response: " + jsonResponse);

			var generalResponses = JsonSerializer.Deserialize<List<GeneralRespons>>(jsonResponse);


			Dictionary<string, object> Responses = new();

			foreach (var pair in generalResponses)
			{

				Responses[pair.Key] = pair.Value;
			}

			return Responses;
		}

		public async Task<Dictionary<string, object>> LogInAsync(LoginDto UserRequest)
		{

			UserRequest.Email ??= "";
			UserRequest.Password ??= "";

			var jsonContent = new StringContent(
									 JsonSerializer.Serialize(UserRequest),
																 System.Text.Encoding.UTF8,
																 "application/json");
			var userAgent = _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"];

			_httpClient.DefaultRequestHeaders.Add("User-Agent", userAgent.ToString());

			var response = await _httpClient.PostAsync("https://localhost:7113/Authentication/login", jsonContent);


			var jsonResponse = await response.Content.ReadAsStringAsync();

			Console.WriteLine("Raw JSON response: " + jsonResponse);

			var generalResponses = JsonSerializer.Deserialize<List<GeneralRespons>>(jsonResponse);



			Dictionary<string, object> Responses = new();

			foreach (var pair in generalResponses)
			{

				Responses[pair.Key] = pair.Value;
			}

			return Responses;
		}

		public async Task<Dictionary<string, object>> SignOutAsync(string UserRequest)
		{

			Console.WriteLine("Token: " + UserRequest);


			_httpClient.DefaultRequestHeaders.Clear();
			_httpClient.DefaultRequestHeaders.Add("Authorization", UserRequest.ToString());

			var response = await _httpClient.PostAsync($"https://localhost:7113/Authentication/SignOut", null);


			var jsonResponse = await response.Content.ReadAsStringAsync();

			Console.WriteLine("Raw JSON response: " + jsonResponse);

			var generalResponses = JsonSerializer.Deserialize<List<GeneralRespons>>(jsonResponse);


			Dictionary<string, object> Responses = new();

			foreach (var pair in generalResponses)
			{

				Responses[pair.Key] = pair.Value;
			}

			return Responses;
		}

		public async Task<Dictionary<string, object>> RefreshTokenAsync(string refreshToken, string Token)
		{


			var jsonContent = new StringContent(
									 JsonSerializer.Serialize(refreshToken),
																 System.Text.Encoding.UTF8,
																 "application/json");

			_httpClient.DefaultRequestHeaders.Clear();
			_httpClient.DefaultRequestHeaders.Add("Authorization", Token.ToString());

			var response = await _httpClient.PostAsync("https://localhost:7113/Authentication/RefreshToken", jsonContent);


			var jsonResponse = await response.Content.ReadAsStringAsync();

			Console.WriteLine("Raw JSON response: " + jsonResponse);

			var generalResponses = JsonSerializer.Deserialize<List<GeneralRespons>>(jsonResponse);

			Dictionary<string, object> Responses = new();

			foreach (var pair in generalResponses)
			{

				Responses[pair.Key] = pair.Value;
			}

			return Responses;

		}


	}
}
