using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance_DataAccessLayer.Abstract
{
    using SchoolAttendance_EntityLayer.Concrete;

    namespace SchoolAttendance_DataAccessLayer.Abstract
    {
        public interface IAppUserDal : IGenericDal<ApplicationUser>
        {
            Task<ApplicationUser> GetByIdAsyncString(string id);
        }
    }

}
