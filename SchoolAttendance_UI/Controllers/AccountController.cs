using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SchoolAttendance_UI.Models;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance_UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;

        public AccountController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: Login Page
        public IActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var jsonContent = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:7040/api/account/login", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var loginResponse = JsonConvert.DeserializeObject<LoginResponseModel>(jsonResponse);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, loginResponse.UserId),
                    new Claim(ClaimTypes.Name, model.SchoolNumber),
                    new Claim(ClaimTypes.Role, loginResponse.Role) // Rolü claim'e ekle
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                // Rol kontrolü ve yönlendirme
                if (loginResponse.Role == "Teacher")
                {
                    return RedirectToAction("Create", "Lesson"); // Öğretmen sayfasına yönlendir
                }
                else if (loginResponse.Role == "Student")
                {
                    return RedirectToAction("RecordAttendance", "QRScanner"); // Öğrenci sayfasına yönlendir
                }
                else
                {
                    return RedirectToAction("Index", "Home"); // Varsayılan yönlendirme
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }
        }

        public class LoginResponseModel
        {
            public string UserId { get; set; }
            public string Role { get; set; } // Kullanıcı rolü
        }

        // GET: Register Page
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Create the request to the API
            var jsonContent = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Send the request to the API
            var response = await _httpClient.PostAsync("https://localhost:7040/api/account/register", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Login");
            }
            else
            {
                // Handle failed registration
                ModelState.AddModelError("", "Registration failed.");
                return View(model);
            }
        }

        // GET: Change Password Page
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Create the request to the API
            var jsonContent = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Send the request to the API
            var response = await _httpClient.PostAsync("https://localhost:7040/api/account/change-password", content);

            if (response.IsSuccessStatusCode)
            {
                // Handle successful password change
                return RedirectToAction("Login");
            }
            else
            {
                // Handle failed password change
                ModelState.AddModelError("", "Password change failed.");
                return View(model);
            }
        }

        // POST: Logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // API'ye Logout isteği gönder
            var response = await _httpClient.PostAsync("https://localhost:7040/api/account/logout", null);

            // Kullanıcıdan çıkış yap
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Login"); // Başarılı çıkış sonrası login sayfasına yönlendir
            }

            return View("Error"); // Hata durumu
        }
    }
}
