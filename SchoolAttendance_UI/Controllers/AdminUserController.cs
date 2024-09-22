using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using SchoolAttendance_UI.Models;

namespace SchoolAttendance_UI.Controllers
{
    public class AdminUserController : Controller
    {
        private readonly HttpClient _httpClient;

        public AdminUserController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IActionResult> AssignRole(string userId)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7040/api/UserRole/get-user-roles/{userId}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var userRoles = JsonSerializer.Deserialize<UserRoleAssignmentViewModel>(jsonData);
                return View(userRoles);
            }
            return View("Error");
        }

        // Post: Assign a new role to the user
        [HttpPost]
        public async Task<IActionResult> AssignRole(UserRoleAssignmentViewModel model)
        {
            // Sadece UserId ve RoleToAssign (RoleName) gönderilecek
            var userRoleModel = new UserRoleModel
            {
                UserId = model.UserId,
                RoleName = model.RoleToAssign
            };

            var jsonContent = JsonSerializer.Serialize(userRoleModel);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:7040/api/UserRole/assign-role", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View("Error");
        }
        public class UserRoleModel
        {
            public string UserId { get; set; }
            public string RoleName { get; set; }
        }

        // List all users
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("https://localhost:7040/api/User");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var users = JsonSerializer.Deserialize<List<UserViewModel>>(jsonData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (users == null)
                {
                    ViewBag.ErrorMessage = "Kullanıcı verileri null döndü.";
                    return View(new List<UserViewModel>());
                }

                return View(users);
            }

            ViewBag.ErrorMessage = "Kullanıcıları yüklerken bir hata oluştu.";
            return View(new List<UserViewModel>());
        }

        // Delete a user by ID
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7040/api/User/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Error");
        }

        
        // Add a new user
        [HttpPost]
        public async Task<IActionResult> AddUser(UserViewModel user, string password)
        {
            var request = new
            {
                User = user,
                Password = password
            };
            var jsonContent = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:7040/api/User", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Error");
        }
    }
}
