using SchoolAttendance_EntityLayer.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolAttendance_BusinessLayer.Abstract
{
    public interface IUserService
    {
        Task CreateUserAsync(ApplicationUser user, string password);
        Task<ApplicationUser> GetUserByIdAsync(string id);
        Task<List<ApplicationUser>> GetAllUsersAsync();
        Task UpdateUserAsync(ApplicationUser user);
        Task DeleteUserAsync(string id);
    }
}
