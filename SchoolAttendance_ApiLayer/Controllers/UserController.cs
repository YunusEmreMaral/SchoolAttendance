using Microsoft.AspNetCore.Mvc;
using SchoolAttendance_BusinessLayer.Abstract;
using SchoolAttendance_EntityLayer.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolAttendance_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAppUserService _userService;

        public UserController(IAppUserService userService)
        {
            _userService = userService;
        }

        // Get all users
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.TGetListAsync();
            return Ok(users);
        }

        // Get user by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id) // ID'yi int olarak değiştirin
        {
            var user = await _userService.TGetByIDAsync(id); // ID'yi int olarak gönderin
            if (user == null)
            {
                return NotFound("User not found.");
            }
            return Ok(user);
        }

        // Create a new user
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] ApplicationUser user, [FromQuery] string password)
        {
            if (user == null || string.IsNullOrEmpty(password))
            {
                return BadRequest("Invalid data.");
            }

            await _userService.CreateUserAsync(user, password);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        // Update user
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] ApplicationUser user)
        {
            // Eğer user.Id bir string ise, bunu int'e dönüştürmeye çalışın
            if (id != int.Parse(user.Id))
            {
                return BadRequest("User ID mismatch.");
            }

            await _userService.TUpdateAsync(user);
            return NoContent();
        }

        // Delete user
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id) // ID'yi int olarak değiştirin
        {
            var user = await _userService.TGetByIDAsyncString(id); // ID'yi int olarak gönderin
            if (user == null)
            {
                return NotFound("User not found.");
            }

            await _userService.TDeleteAsync(user);
            return NoContent();
        }
    }
}
