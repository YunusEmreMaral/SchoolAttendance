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

// CORS yapýlandýrmasý
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

// DbContext ve baðlantý ayarlarý
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ASP.NET Core Identity yapýlandýrmasý
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

// Authentication ayarlarý
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Giriþ yolu
    });

// Swagger yapýlandýrmasý
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Controller ve MVC ayarlarý
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Role ve kullanýcý seeding iþlemi (rol ve kullanýcýlarýn otomatik eklenmesi)
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await SeedData.SeedRoles(roleManager);
}

// HTTP request pipeline yapýlandýrmasý
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

// HTTPS yönlendirme ve statik dosya kullanýmýný etkinleþtirme
app.UseHttpsRedirection();
app.UseStaticFiles();

// CORS middleware'i ekleniyor
app.UseCors("AllowAllOrigins");

// Routing ayarlarý
app.UseRouting();

// Kimlik doðrulama ve yetkilendirme
app.UseAuthentication();
app.UseAuthorization();

// API controller route'larý
app.MapControllers();

app.Run();
