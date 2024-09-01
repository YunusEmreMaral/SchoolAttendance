using Microsoft.AspNetCore.Mvc;
using SchoolAttendance_BusinessLayer.Abstract;
using SchoolAttendance_EntityLayer.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolAttendance_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Course>>> GetAll()
        {
            var courses = await _courseService.TGetListAsync();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetById(int id)
        {
            var course = await _courseService.TGetByIDAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Course course)
        {
            if (course == null)
            {
                return BadRequest();
            }
            await _courseService.TAddAsync(course);
            return CreatedAtAction(nameof(GetById), new { id = course.CourseId }, course);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Course course)
        {
            if (id != course.CourseId)
            {
                return BadRequest();
            }
            if (course == null)
            {
                return NotFound();
            }
            await _courseService.TUpdateAsync(course);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var course = await _courseService.TGetByIDAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            await _courseService.TDeleteAsync(course);
            return NoContent();
        }
    }
}
