using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using SchoolAttendance_BusinessLayer.Abstract;
using SchoolAttendance_EntityLayer.Concrete;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// çalışmıyor buraya bakılacak 
namespace SchoolAttendance_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAppUserService _appUserService;

        public UserRoleController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IAppUserService appUserService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _appUserService = appUserService;
        }

        [HttpGet("get-user-roles/{userId}")]
        public async Task<IActionResult> GetUserRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var roleNames = roles.ToList();

            return Ok(new
            {
                UserId = userId,
                UserName = user.UserName,
                Roles = roleNames
            });
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] UserRoleModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var roleExist = await _roleManager.RoleExistsAsync(model.RoleName);
            if (!roleExist)
            {
                return BadRequest("Role does not exist.");
            }

            await _appUserService.AssignRoleAsync(user, model.RoleName);
            return Ok($"Role '{model.RoleName}' assigned to user '{user.UserName}'.");
        }

        [HttpPost("remove-role")]
        public async Task<IActionResult> RemoveRole([FromBody] UserRoleModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var roleExist = await _roleManager.RoleExistsAsync(model.RoleName);
            if (!roleExist)
            {
                return BadRequest("Role does not exist.");
            }

            if (!await _userManager.IsInRoleAsync(user, model.RoleName))
            {
                return BadRequest("User is not in this role.");
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
