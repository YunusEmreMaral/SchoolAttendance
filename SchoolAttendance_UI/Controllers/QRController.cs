using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace SchoolAttendance_UI.Controllers
{
    public class QRController : Controller
    {
        private readonly HttpClient _httpClient;

        public QRController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // ID'ye göre QR kodu getirir
        [HttpGet]
        public async Task<IActionResult> GetQRCode(int id)
        {
            // API'deki QR kodu getiren endpoint
            var apiUrl = $"https://localhost:7040/api/course/{id}/qrcode";

            // API'den QR kodunu çek
            var response = await _httpClient.GetAsync(apiUrl);

            if (!response.IsSuccessStatusCode)
            {
                // Hata durumu
                return NotFound("QR kodu bulunamadı.");
            }

            // API'den dönen QR kodu verisini string olarak al
            var qrCode = await response.Content.ReadAsStringAsync();

            // Bu QR kodunu View'e gönder
            ViewBag.QRCode = qrCode;

            return View();
        }
    }
}
