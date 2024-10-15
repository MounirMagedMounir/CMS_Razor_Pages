using CMS_UI.Handlers;
using CMS_UI.Infrastructure;
using CMS_UI.Utilities;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorPages();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();



builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7113");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddHttpClient<ApiClientHandler>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7113");
});

builder.Services.AddHttpClient("AdminApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7113");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddHttpClient<AdminApiHandler>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7113");
});




builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ApiClientHandler>();
builder.Services.AddScoped<AdminApiHandler>();
builder.Services.AddScoped<Session>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(15);
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.Path = "/";
    options.Cookie.Domain = "localhost";
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();
app.UseMiddleware<RefreshTokenMiddleware>();
//app.UseExceptionHandler();
app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
