using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolAttendance_BusinessLayer.Abstract;
using SchoolAttendance_DataAccessLayer.Abstract.SchoolAttendance_DataAccessLayer.Abstract;
using SchoolAttendance_EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SchoolAttendance_BusinessLayer.Concrete
{
    public class AppUserService : IAppUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAppUserDal _appUserDal;

        public AppUserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IAppUserDal appUserDal)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _appUserDal = appUserDal;
        }


        public async Task CreateUserAsync(ApplicationUser user, string password)
        {
            await _userManager.CreateAsync(user, password);
        }

        public async Task AssignRoleAsync(ApplicationUser user, string role)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }

            await _userManager.AddToRoleAsync(user, role);
        }

        public async Task TAddAsync(ApplicationUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            await CreateUserAsync(user, user.PasswordHash); // PasswordHash'in doğru şekilde ayarlandığından emin olun
        }

        public async Task TDeleteAsync(ApplicationUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            await _appUserDal.DeleteAsync(user); // Use the repository's delete method
        }

        public async Task TUpdateAsync(ApplicationUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            await _userManager.UpdateAsync(user);
        }

        public async Task<ApplicationUser> TGetByIDAsync(int id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public async Task<List<ApplicationUser>> TGetListAsync()
        {
            return await _userManager.Users.ToListAsync(); // UserManager'dan kullanıcıları listele
        }

        public async Task<List<ApplicationUser>> TGetListByFilterAsync(Expression<Func<ApplicationUser, bool>> filter)
        {
            return await _userManager.Users.Where(filter).ToListAsync();
        }
        public async Task<ApplicationUser> TGetByIDAsyncString(string id)
        {
            return await _appUserDal.GetByIdAsyncString(id);
        }


    }
}
