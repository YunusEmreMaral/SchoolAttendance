using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolAttendance_API;
using SchoolAttendance_BusinessLayer.Abstract;
using SchoolAttendance_BusinessLayer.Concrete;
using SchoolAttendance_BusinessLayer.Container;
using SchoolAttendance_DataAccessLayer.Abstract;
using SchoolAttendance_DataAccessLayer.Concrete;
using SchoolAttendance_DataAccessLayer.EntityFramework;
using SchoolAttendance_EntityLayer.Concrete;

var builder = WebApplication.CreateBuilder(args);

// CORS yap�land�rmas�
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Service Registration (Business Layer ve Data Access Layer servisleri)
builder.Services.AddApplicationServices();

// DbContext ve ba�lant� ayarlar�
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ASP.NET Core Identity yap�land�rmas�
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Authentication ayarlar�
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Giri� yolu
    });

// Swagger yap�land�rmas�
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Controller ve MVC ayarlar�
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Role ve kullan�c� seeding i�lemi (rol ve kullan�c�lar�n otomatik eklenmesi)
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await SeedData.SeedRoles(roleManager);
}

// HTTP request pipeline yap�land�rmas�
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// HTTPS y�nlendirme ve statik dosya kullan�m�n� etkinle�tirme
app.UseHttpsRedirection();
app.UseStaticFiles();

// CORS middleware'i ekleniyor
app.UseCors("AllowAllOrigins");

// Routing ayarlar�
app.UseRouting();

// Kimlik do�rulama ve yetkilendirme
app.UseAuthentication();
app.UseAuthorization();

// API controller route'lar�
app.MapControllers();

app.Run();
