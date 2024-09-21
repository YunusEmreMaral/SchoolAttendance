using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolAttendance_BusinessLayer.Abstract;
using SchoolAttendance_EntityLayer.Concrete;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Kimlik doğrulaması gerektir

    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AttendanceController(UserManager<ApplicationUser> userManager, IAttendanceService attendanceService)
        {
            _userManager = userManager;
            _attendanceService = attendanceService;
        }

        [HttpPost("record")]
        public async Task<IActionResult> RecordAttendance([FromBody] AttendanceDto attendanceDto)
        {
            if (attendanceDto == null)
                return BadRequest("Attendance data cannot be null.");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return NotFound("User not found.");

            // Attendance nesnesini oluşturun
            var attendance = new Attendance
            {
                CourseId = attendanceDto.CourseId,
                StudentId = userId,
                Timestamp = DateTime.Now
            };

            // Asenkron olarak ekleme işlemi
            await _attendanceService.TAddAsync(attendance);

            return Ok(new { Message = "Attendance recorded successfully." });
        }

        // DTO (Data Transfer Object) sınıfı
        public class AttendanceDto
        {
            public int CourseId { get; set; } // QR kodundan alınacak ders ID'si
            public string UserId { get; set; } // Kullanıcı ID'si
            public DateTime Timestamp { get; set; } // Yoklamanın alındığı zaman
        }


        [HttpGet]
        public async Task<ActionResult<List<Attendance>>> GetAll()
        {
            var attendances = await _attendanceService.TGetListAsync();
            return Ok(attendances);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Attendance>> GetById(int id)
        {
            var attendance = await _attendanceService.TGetByIDAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }
            return Ok(attendance);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Attendance attendance)
        {
            if (attendance == null)
            {
                return BadRequest();
            }
            await _attendanceService.TAddAsync(attendance);
            return CreatedAtAction(nameof(GetById), new { id = attendance.AttendanceId }, attendance);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Attendance attendance)
        {
            if (id != attendance.AttendanceId)
            {
                return BadRequest();
            }
            if (attendance == null)
            {
                return NotFound();
            }
            await _attendanceService.TUpdateAsync(attendance);
            return NoContent();
        }
        public class AttendanceDto2
        {
            public int AttendanceId { get; set; }
            public DateTime Timestamp { get; set; }
            public string StudentId { get; set; }
            public string CourseName { get; set; }
            public string SchoolNumber { get; set; } // SchoolNumber'ı ekledik
        }

        // API metodunuzda dönüş tipi olarak DTO kullanabilirsiniz
        [HttpGet("{id}/attendances")]
        public async Task<ActionResult<List<AttendanceDto2>>> GetAttendancesByCourse(int id)
        {
            try
            {
                var attendances = await _attendanceService.GetAttendancesByCourseIdAsync(id);
                if (attendances == null || !attendances.Any())
                {
                    return NotFound("No attendances found for this course.");
                }

                // DTO'ya dönüştürme
                var attendanceDtos = attendances.Select(a => new AttendanceDto2
                {
                    AttendanceId = a.AttendanceId,
                    Timestamp = a.Timestamp,
                    StudentId = a.StudentId,
                    CourseName = a.Course?.CourseName, // Güvenli erişim
                    SchoolNumber = a.Student?.SchoolNumber // Güvenli erişim
                }).ToList();

                return Ok(attendanceDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }





        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var attendance = await _attendanceService.TGetByIDAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }
            await _attendanceService.TDeleteAsync(attendance);
            return NoContent();
        }
    }
}
