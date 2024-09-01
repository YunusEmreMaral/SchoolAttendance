using Microsoft.AspNetCore.Mvc;
using SchoolAttendance_BusinessLayer.Abstract;
using SchoolAttendance_EntityLayer.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolAttendance_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Teacher>>> GetAll()
        {
            var teachers = await _teacherService.TGetListAsync();
            return Ok(teachers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> GetById(int id)
        {
            var teacher = await _teacherService.TGetByIDAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return Ok(teacher);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Teacher teacher)
        {
            if (teacher == null)
            {
                return BadRequest();
            }
            await _teacherService.TAddAsync(teacher);
            return CreatedAtAction(nameof(GetById), new { id = teacher.TeacherId }, teacher);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Teacher teacher)
        {
            if (id != teacher.TeacherId)
            {
                return BadRequest();
            }
            if (teacher == null)
            {
                return NotFound();
            }
            await _teacherService.TUpdateAsync(teacher);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var teacher = await _teacherService.TGetByIDAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            await _teacherService.TDeleteAsync(teacher);
            return NoContent();
        }
    }
}
