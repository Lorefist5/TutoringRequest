using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using TutoringRequest.Services.HttpClientServices;
using TutoringRequest.WebUi.Services;

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
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient<AuthenticatedHttpClientService>();
builder.Services.AddSingleton<AuthApiService>();
builder.Services.AddSingleton<RoleApiService>();
builder.Services.AddSingleton<TutorApiService>();
builder.Services.AddSingleton<CourseApiService>();
builder.Services.AddSingleton<MajorApiService>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/Account/Login"; // or wherever your login path is
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.SlidingExpiration = true;
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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
