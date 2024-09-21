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

            // API'ye istek gönderiyoruz
            var response = await _httpClient.PostAsync("https://localhost:7040/api/account/login", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var loginResponse = JsonConvert.DeserializeObject<LoginResponseModel>(jsonResponse);

                // Kullanıcı ID'sini loginResponse üzerinden alarak Claim'leri oluşturun
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, loginResponse.UserId), // Kullanıcı ID'si
                    new Claim(ClaimTypes.Name, model.SchoolNumber) // Kullanıcı adı (SchoolNumber)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Kullanıcıyı oturum açma işlemi yap
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                // Yönlendirme
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Giriş başarısız ise hata mesajı gösterilir
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }
        }

        public class LoginResponseModel
        {
            public string Message { get; set; }
            public string UserId { get; set; } // Kullanıcı ID'si eklendi
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
    }
}
