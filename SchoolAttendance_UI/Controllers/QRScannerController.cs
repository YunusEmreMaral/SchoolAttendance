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
        private readonly HttpClient _httpClient; // API istekleri için HttpClient

        public QRScannerController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }



       



        // Attendance kaydını yapmak için API'ye istek gönderir
        [HttpPost("record")]
        public async Task<IActionResult> RecordAttendance([FromBody] AttendanceDto attendanceDto)
        {
            if (attendanceDto == null)
            {
                return BadRequest("Attendance data cannot be null.");
            }

            // Debug için log ekleyebilirsiniz
            Console.WriteLine($"Received CourseId: {attendanceDto.CourseId}, UserId: {attendanceDto.UserId} , timestam {attendanceDto.Timestamp}");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID not found.");
            }
           
            attendanceDto.UserId = userId;
            attendanceDto.Timestamp = DateTime.Now;

            var jsonContent = new StringContent(JsonSerializer.Serialize(attendanceDto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:7040/api/attendance/record", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return Ok(new { Message = "Attendance recorded successfully." });
            }

            return BadRequest(new { Message = "Failed to record attendance." });
        }

        // DTO sınıfına UserId ekleyin
        public class AttendanceDto
        {
            public int CourseId { get; set; } // QR kodundan alınacak ders ID'si
            public string UserId { get; set; } // Kullanıcı ID'si
            public DateTime Timestamp { get; set; } // Yoklamanın alındığı zaman
        }


        // QR kodundan ders ID'sini çıkarma metodu (örnek)
        private int ExtractCourseIdFromQRCode(string qrCode)
        {
            // QR kodu analiz edilerek ders ID'si çıkar
            // Burada uygun işleme göre düzenleyebilirsiniz
            return int.Parse(qrCode); // Örnek olarak doğrudan parse ediyoruz
        }
        


    }
}
