using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolAttendance_ApiLayer.Models;
using SchoolAttendance_EntityLayer.Concrete;

namespace SchoolAttendance_ApiLayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new ApplicationUser
            {
                UserName = model.SchoolNumber, // Burada SchoolNumber'ı UserName olarak kullanıyoruz
                Email = model.Email,
                Name = model.Name,
                SchoolNumber = model.SchoolNumber
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return Ok(new { Message = "User registered successfully." });
            }

            return BadRequest(result.Errors);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // SchoolNumber'a göre kullanıcıyı buluyoruz
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.SchoolNumber == model.SchoolNumber);

            if (user == null)
            {
                return Unauthorized(new { Message = "Invalid login attempt." });
            }

            // Kullanıcı bulundu, şimdi UserName ile giriş yapalım
            var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                return Ok(new
                {
                    Message = "Login successful.",
                    UserId = user.Id // Kullanıcı ID'sini burada döndürüyoruz
                });
            }

            return Unauthorized(new { Message = "Invalid login attempt." });
        }






        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound(new { Message = "User not found." });

            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                return Ok(new { Message = "Password changed successfully." });
            }

            return BadRequest(result.Errors);
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            // ASP.NET Core Identity oturumu sonlandırma
            await _signInManager.SignOutAsync();

            // Kullanıcıya ait olan cookie oturumunu sonlandırma
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Ok(new { Message = "Logout successful." });
        }


    }
}
