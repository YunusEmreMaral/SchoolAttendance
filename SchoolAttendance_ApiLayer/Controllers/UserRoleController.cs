using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using SchoolAttendance_EntityLayer.Concrete;

namespace SchoolAttendance_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRoleController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // Get roles for a specific user
        [HttpGet("get-user-roles/{userId}")]
        public async Task<IActionResult> GetUserRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var roles = await _userManager.GetRolesAsync(user);
            return Ok(new { user.UserName, Roles = roles });
        }

        // Assign role to user
        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] UserRoleModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            if (!await _roleManager.RoleExistsAsync(model.RoleName))
            {
                return BadRequest("Role does not exist.");
            }

            await _userManager.AddToRoleAsync(user, model.RoleName);
            return Ok($"Role '{model.RoleName}' assigned to user '{user.UserName}'.");
        }

        // Remove role from user
        [HttpPost("remove-role")]
        public async Task<IActionResult> RemoveRole([FromBody] UserRoleModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            if (!await _userManager.IsInRoleAsync(user, model.RoleName))
            {
                return BadRequest("User does not have this role.");
            }

            await _userManager.RemoveFromRoleAsync(user, model.RoleName);
            return Ok($"Role '{model.RoleName}' removed from user '{user.UserName}'.");
        }

        public class UserRoleModel
        {
            public string UserId { get; set; }
            public string RoleName { get; set; }
        }
    }
}
