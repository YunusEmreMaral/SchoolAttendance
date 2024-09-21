using SchoolAttendance_EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance_BusinessLayer.Abstract
{
    public interface IAppUserService : IGenericService<ApplicationUser>
    {
        Task CreateUserAsync(ApplicationUser user, string password);
        Task AssignRoleAsync(ApplicationUser user, string role);
        Task<ApplicationUser> TGetByIDAsyncString(string id); // Yeni metot

    }
}
