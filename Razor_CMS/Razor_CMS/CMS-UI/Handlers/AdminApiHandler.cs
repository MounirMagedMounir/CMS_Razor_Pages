using CMS_UI.FilterModels;
using CMS_UI.Dto;
using CMS_UI.Utilities;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CMS_UI.Handlers
{
	public class AdminApiHandler(HttpClient _httpClient,
		IHttpContextAccessor _httpContextAccessor,
		Session _session)
	{
		// Users API Handler
		public async Task<Dictionary<string, object>> CreateUserAsync(UserDto UserRequest)
		{

			UserRequest.name ??= "";
			UserRequest.email ??= "";
			UserRequest.phone ??= "";
			UserRequest.userName ??= "";
			UserRequest.password ??= "";
			UserRequest.profileImage ??= "";
			UserRequest.role ??= "";


			var jsonContent = new StringContent(
							 JsonSerializer.Serialize(UserRequest),
														 System.Text.Encoding.UTF8,
														 "application/json");

			_httpClient.DefaultRequestHeaders.Clear();
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _session.Get("Token").ToString().Trim());

			var response = await _httpClient.PostAsync("https://localhost:7113/Admin/CreateUser", jsonContent);

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

		public async Task<Dictionary<string, object>> UpdateUserAsync(UserDto UserRequest)
		{

			UserRequest.name ??= "";
			UserRequest.email ??= "";
			UserRequest.phone ??= "";
			UserRequest.userName ??= "";
			UserRequest.password ??= "";
			UserRequest.profileImage ??= "";
			UserRequest.role ??= "";


			var jsonContent = new StringContent(
							 JsonSerializer.Serialize(UserRequest),
														 System.Text.Encoding.UTF8,
														 "application/json");
			_httpClient.DefaultRequestHeaders.Clear();
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _session.Get("Token").ToString().Trim());

			var response = await _httpClient.PutAsync("https://localhost:7113/Admin/UpdateUser", jsonContent);

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

		public async Task<Dictionary<string, object>> DeleteUserAsync(string UserId)
		{

			_httpClient.DefaultRequestHeaders.Clear();
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _session.Get("Token").ToString().Trim());

			var response = await _httpClient.DeleteAsync($"https://localhost:7113/Admin/DeleteUser?UserId={UserId}");

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

		public async Task<Dictionary<string, object>> DeleteUsersAsync(List<string> userIds)
		{
			var jsonContent = new StringContent(
				JsonSerializer.Serialize(userIds),
				System.Text.Encoding.UTF8,
				"application/json");

			var request = new HttpRequestMessage(HttpMethod.Delete, "https://localhost:7113/Admin/DeleteUsers")
			{
				Content = jsonContent
			};

			request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _session.Get("Token").ToString().Trim());


			var response = await _httpClient.SendAsync(request);


			response.EnsureSuccessStatusCode();


			var jsonResponse = await response.Content.ReadAsStringAsync();
			Console.WriteLine("Raw JSON response: " + jsonResponse);

			var generalResponses = JsonSerializer.Deserialize<List<GeneralRespons>>(jsonResponse);

			Dictionary<string, object> responses = new();
			foreach (var pair in generalResponses)
			{
				responses[pair.Key] = pair.Value;
			}
			return responses;
		}

		public Dictionary<string, object> GetUsersAsync(UsersFilter? parameter, int? skip, int? take, string? sortBy, string? sortOrder)
		{
			var jsonContent = new StringContent(
						 JsonSerializer.Serialize(parameter),
													 System.Text.Encoding.UTF8,
													 "application/json");

			_httpClient.DefaultRequestHeaders.Clear();
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _session.Get("Token").ToString().Trim());

			var response = _httpClient.PostAsync($"https://localhost:7113/Admin/GetUsers?skiped={skip}&take={take}&sortBy={sortBy}&sortOrder={sortOrder}", jsonContent);

			var jsonResponse = response.Result.Content.ReadAsStringAsync();

			Console.WriteLine("Raw JSON response: " + jsonResponse.Result);


			var generalResponses = JsonSerializer.Deserialize<List<GeneralRespons>>(jsonResponse.Result);


			Dictionary<string, object> Responses = new();

			foreach (var pair in generalResponses)
			{

				Responses[pair.Key] = pair.Value;
			}

			return Responses;

		}

		public Dictionary<string, object> GetUserByIdAsync(string UserId)
		{

			_httpClient.DefaultRequestHeaders.Clear();
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _session.Get("Token").ToString().Trim());

			var response = _httpClient.GetAsync($"https://localhost:7113/Admin/GetUserById?UserId={UserId}");

			var jsonResponse = response.Result.Content.ReadAsStringAsync();

			Console.WriteLine("Raw JSON response: " + jsonResponse.Result);


			var generalResponses = JsonSerializer.Deserialize<List<GeneralRespons>>(jsonResponse.Result);


			Dictionary<string, object> Responses = new();

			foreach (var pair in generalResponses)
			{

				Responses[pair.Key] = pair.Value;
			}

			return Responses;

		}

		// Roles API Handler
		public async Task<Dictionary<string, object>> CreateRoleAsync(RoleDto RoleRequest)
		{

			RoleRequest.name ??= "";



			var jsonContent = new StringContent(
							 JsonSerializer.Serialize(RoleRequest),
														 System.Text.Encoding.UTF8,
														 "application/json");

			_httpClient.DefaultRequestHeaders.Clear();
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _session.Get("Token").ToString().Trim());

			var response = await _httpClient.PostAsync("https://localhost:7113/Admin/CreateRole", jsonContent);

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

		public async Task<Dictionary<string, object>> UpdateRoleAsync(RoleDto UserRequest)
		{

			UserRequest.name ??= "";



			var jsonContent = new StringContent(
							 JsonSerializer.Serialize(UserRequest),
														 System.Text.Encoding.UTF8,
														 "application/json");
			_httpClient.DefaultRequestHeaders.Clear();
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _session.Get("Token").ToString().Trim());

			var response = await _httpClient.PutAsync("https://localhost:7113/Admin/UpdateRole", jsonContent);

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

		public async Task<Dictionary<string, object>> DeleteRoleAsync(string RoleId)
		{

			_httpClient.DefaultRequestHeaders.Clear();
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _session.Get("Token").ToString().Trim());

			var response = await _httpClient.DeleteAsync($"https://localhost:7113/Admin/DeleteRoleById?RoleId={RoleId}");

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

		public async Task<Dictionary<string, object>> DeleteRolesAsync(List<string> RoleIds)
		{
			var jsonContent = new StringContent(
				JsonSerializer.Serialize(RoleIds),
				System.Text.Encoding.UTF8,
				"application/json");

			var request = new HttpRequestMessage(HttpMethod.Delete, "https://localhost:7113/Admin/DeleteRolesById")
			{
				Content = jsonContent
			};

			request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _session.Get("Token").ToString().Trim());


			var response = await _httpClient.SendAsync(request);


			response.EnsureSuccessStatusCode();


			var jsonResponse = await response.Content.ReadAsStringAsync();
			Console.WriteLine("Raw JSON response: " + jsonResponse);

			var generalResponses = JsonSerializer.Deserialize<List<GeneralRespons>>(jsonResponse);

			Dictionary<string, object> responses = new();
			foreach (var pair in generalResponses)
			{
				responses[pair.Key] = pair.Value;
			}
			return responses;
		}

		public Dictionary<string, object> GetRolesAsync(RolesFilter? parameter, int? skip, int? take, string? sortBy, string? sortOrder)
		{
			var jsonContent = new StringContent(
						 JsonSerializer.Serialize(parameter),
													 System.Text.Encoding.UTF8,
													 "application/json");

			_httpClient.DefaultRequestHeaders.Clear();
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _session.Get("Token").ToString().Trim());

			var response = _httpClient.PostAsync($"https://localhost:7113/Admin/GetRoles?skip={skip}&take={take}&sortBy={sortBy}&sortOrder={sortOrder}", jsonContent);

			var jsonResponse = response.Result.Content.ReadAsStringAsync();

			Console.WriteLine("Raw JSON response: " + jsonResponse.Result);


			var generalResponses = JsonSerializer.Deserialize<List<GeneralRespons>>(jsonResponse.Result);


			Dictionary<string, object> Responses = new();

			foreach (var pair in generalResponses)
			{

				Responses[pair.Key] = pair.Value;
			}

			return Responses;

		}

		public Dictionary<string, object> GetRoleByIdAsync(string RoleId)
		{

			_httpClient.DefaultRequestHeaders.Clear();
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _session.Get("Token").ToString().Trim());

			var response = _httpClient.GetAsync($"https://localhost:7113/Admin/GetRoleById?RoleId={RoleId}");

			var jsonResponse = response.Result.Content.ReadAsStringAsync();

			Console.WriteLine("Raw JSON response: " + jsonResponse.Result);


			var generalResponses = JsonSerializer.Deserialize<List<GeneralRespons>>(jsonResponse.Result);


			Dictionary<string, object> Responses = new();

			foreach (var pair in generalResponses)
			{

				Responses[pair.Key] = pair.Value;
			}

			return Responses;

		}


	}
}