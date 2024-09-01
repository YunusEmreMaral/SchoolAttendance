using Microsoft.AspNetCore.Mvc;
using SchoolAttendance_BusinessLayer.Abstract;
using SchoolAttendance_EntityLayer.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolAttendance_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
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
