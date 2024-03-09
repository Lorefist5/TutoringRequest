using Microsoft.AspNetCore.Authentication.Cookies;
using TutoringRequest.Services.HttpClientServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<HttpClient>(prov =>
{
    var apiBaseUrl = builder.Configuration["ApiSettings:ApiBaseUrl"] ?? "https://localhost:7249/api/";
    HttpClient client = new HttpClient();
    client.BaseAddress = new Uri(apiBaseUrl);
    return client;
});
builder.Services.AddTransient<AuthApiService>();
builder.Services.AddTransient<RoleApiService>();
builder.Services.AddTransient<TutorApiService>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.Cookie.Name = "LoginCookie";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // Adjust as needed
    
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
