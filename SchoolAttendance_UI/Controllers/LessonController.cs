using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SchoolAttendance_UI.Models;
using System.Security.Claims;
using System.Text;

namespace SchoolAttendance_UI.Controllers
{
    public class LessonController : Controller
    {
        private readonly HttpClient _httpClient;

        public LessonController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET Create page
        public IActionResult Create()
        {
            var teacherId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.TeacherId = teacherId;
            return View();
        }

        // POST Create course
        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseModel model)
        {
            
            if (!ModelState.IsValid)
            {
                return View(model); // Validation failed, return view with model to show errors
            }


            var jsonContent = JsonConvert.SerializeObject(model);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:7040/api/course", httpContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List", "Lesson"); // Redirect to list page after success
            }
            else
            {
                ModelState.AddModelError("", "There was an error creating the course.");
                return View(model); // Return view with error
            }
        }

        // 2. Sayfa: Dersler
        public async Task<IActionResult> List()
        {
            // API'dan authenticated öğretmenin derslerini çek
            var teacherId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _httpClient.GetAsync($"https://localhost:7040/api/course/teacher-courses");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var courses = JsonConvert.DeserializeObject<List<CourseViewModel>>(jsonString);
                return View(courses);
            }

            // Eğer dersler bulunamazsa ya da API çağrısı başarısız olursa, boş bir model döndür
            return View(new List<CourseViewModel>());
        }

        // 3. Sayfa: QR Kod Göster
        public async Task<IActionResult> ShowQRCode(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7040/api/course/{id}/qrcode");

            if (response.IsSuccessStatusCode)
            {
                var qrCode = await response.Content.ReadAsStringAsync();
                ViewBag.QRCode = qrCode;
                return View();
            }

            return NotFound(); // Eğer QR kod alınamazsa
        }



        // 4. Sayfa: Yoklamalar
        public async Task<IActionResult> Attendance(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7040/api/attendance/{id}/attendances");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var attendances = JsonConvert.DeserializeObject<List<AttendanceViewModel2>>(jsonString);
                return View(attendances); // Burada List<AttendanceViewModel2> döndürdüğünüzden emin olun
            }

            return View(new List<AttendanceViewModel2>()); // Burayı da değiştirin
        }



    }
}
