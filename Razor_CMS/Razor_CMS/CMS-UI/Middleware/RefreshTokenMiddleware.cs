using CMS_UI.Handlers;
using CMS_UI.Utilities;
using System.IdentityModel.Tokens.Jwt;

public class RefreshTokenMiddleware
{
	private readonly RequestDelegate _next;
	private readonly IServiceProvider _serviceProvider;

	public RefreshTokenMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
	{
		_next = next;
		_serviceProvider = serviceProvider;
	}

	public async Task InvokeAsync(HttpContext context)
	{

		using (var scope = _serviceProvider.CreateScope())
		{
			var _apiClientHandler = scope.ServiceProvider.GetRequiredService<ApiClientHandler>();
			var _session = scope.ServiceProvider.GetRequiredService<Session>();

			var token = _session.Get("Token");
			var refreshToken = _session.Get("RefreshToken");

			if (token != null && refreshToken != null)
			{
				var handler = new JwtSecurityTokenHandler();
				var jwtToken = handler.ReadJwtToken(token);

				if (jwtToken.ValidTo < DateTime.UtcNow)
				{
					var result = await _apiClientHandler.RefreshTokenAsync(refreshToken, token);

					if (result.ContainsKey("Success") && result.TryGetValue("Token", out object newToken))
					{
						_session.Set("Token", newToken.ToString());

						if (result.TryGetValue("RefreshToken", out object newRefreshToken))
						{
							_session.Set("RefreshToken", newRefreshToken.ToString());
						}
					}
					else if (result.ContainsKey("Alert"))
					{
						_session.Remove("Token");
						_session.Remove("RefreshToken");
						_session.Set("Alert", result["Alert"].ToString());

						if (context.Request.Path.StartsWithSegments("/Admin"))
						{
							context.Response.Redirect("/Authentication/Login");
						}
						return;
					}
				}
			}
			else if (context.Request.Path.StartsWithSegments("/Admin"))
			{
				_session.Remove("Token");
				_session.Remove("RefreshToken");
				context.Response.Redirect("/Authentication/Login");
				return;
			}
		}


		await _next(context);
	}

}
