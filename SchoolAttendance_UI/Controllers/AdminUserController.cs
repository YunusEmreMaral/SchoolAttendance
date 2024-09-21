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

        // Get roles for a specific user and display form to assign role
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

        // Assign a new role to the user
        [HttpPost]
        public async Task<IActionResult> AssignRole(UserRoleAssignmentViewModel model)
        {
            var jsonContent = JsonSerializer.Serialize(model);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:7040/api/UserRole/assign-role", content);
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
