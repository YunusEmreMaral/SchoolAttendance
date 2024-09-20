using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using SchoolAttendance_UI.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Authentication configuration
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Giriþ yolu
        options.LogoutPath = "/Account/Logout"; // Çýkýþ yolu
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Cookie'nin süresi
        options.SlidingExpiration = true; // Süre dolduðunda yeniden süre uzatma
    });



builder.Services.AddHttpClient<QRController>();
builder.Services.AddHttpClient<AccountController>();
builder.Services.AddHttpClient<QRScannerController>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication(); // Kimlik doðrulamasý ekleniyor
app.UseAuthorization();  // Yetkilendirme ekleniyor

// MVC için varsayýlan rota ayarý
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
