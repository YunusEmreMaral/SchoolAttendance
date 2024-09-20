using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SchoolAttendance_UI.Controllers
{
    [Authorize]
    public class QRScannerController : Controller
    {
        private readonly HttpClient _httpClient;

        public QRScannerController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET Metodu
        public IActionResult RecordAttendance()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized("Kullanıcı oturum açmadı.");
            }

            // Gerekli verileri ViewBag ile taşıyın
            ViewBag.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RecordAttendance(AttendanceDto attendanceDto)
        {
            if (attendanceDto == null)
            {
                return BadRequest("Attendance data cannot be null.");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID not found.");
            }

            attendanceDto.UserId = userId;
            attendanceDto.Timestamp = DateTime.Now;

            var jsonContent = JsonSerializer.Serialize(attendanceDto);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:7040/api/Attendance/record", content);

            if (response.IsSuccessStatusCode)
            {
                return Ok(new { Message = "Attendance recorded successfully." });
            }

            var errorMessage = await response.Content.ReadAsStringAsync();
            return BadRequest(new { Message = "Failed to record attendance.", Details = errorMessage });
        }

        public class AttendanceDto
        {
            public int CourseId { get; set; }
            public string UserId { get; set; }
            public DateTime Timestamp { get; set; }
        }
    }
}
