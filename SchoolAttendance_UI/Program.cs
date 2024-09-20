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
        options.LoginPath = "/Account/Login"; // Giri� yolu
        options.LogoutPath = "/Account/Logout"; // ��k�� yolu
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Cookie'nin s�resi
        options.SlidingExpiration = true; // S�re doldu�unda yeniden s�re uzatma
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

app.UseAuthentication(); // Kimlik do�rulamas� ekleniyor
app.UseAuthorization();  // Yetkilendirme ekleniyor

// MVC i�in varsay�lan rota ayar�
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
