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

// CORS yapılandırması
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

// DbContext ve bağlantı ayarları
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ASP.NET Core Identity yapılandırması
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

// Authentication ayarları
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Giriş yolu
    });

// Swagger yapılandırması
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Controller ve MVC ayarları
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Role ve kullanıcı seeding işlemi (rol ve kullanıcıların otomatik eklenmesi)
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await SeedData.SeedRoles(roleManager);
}

// HTTP request pipeline yapılandırması
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

// HTTPS yönlendirme ve statik dosya kullanımını etkinleştirme
app.UseHttpsRedirection();
app.UseStaticFiles();

// CORS middleware'i ekleniyor
app.UseCors("AllowAllOrigins");

// Routing ayarları
app.UseRouting();

// Kimlik doğrulama ve yetkilendirme
app.UseAuthentication();
app.UseAuthorization();

// API controller route'ları
app.MapControllers();

app.Run();
