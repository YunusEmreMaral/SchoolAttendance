using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using SchoolAttendance_BusinessLayer.Abstract;
using SchoolAttendance_EntityLayer.Concrete;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SchoolAttendance_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = _roleManager.Roles;
            return Ok(await roles.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] string roleName)
        {
            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (roleExist)
            {
                return BadRequest("Role already exists.");
            }

            var role = new IdentityRole(roleName);
            await _roleManager.CreateAsync(role);
            return Ok(role);
        }

        [HttpPut("{roleName}")]
        public async Task<IActionResult> UpdateRole(string roleName, [FromBody] string newRoleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                return NotFound("Role not found.");
            }

            role.Name = newRoleName;
            await _roleManager.UpdateAsync(role);
            return Ok(role);
        }

        [HttpDelete("{roleName}")]
        public async Task<IActionResult> DeleteRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                return NotFound("Role not found.");
            }

            await _roleManager.DeleteAsync(role);
            return NoContent();
        }
    }
}
