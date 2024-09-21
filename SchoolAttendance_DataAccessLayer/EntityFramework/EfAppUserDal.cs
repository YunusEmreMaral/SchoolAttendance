using Microsoft.EntityFrameworkCore;
using SchoolAttendance_DataAccessLayer.Abstract.SchoolAttendance_DataAccessLayer.Abstract;
using SchoolAttendance_DataAccessLayer.Concrete;
using SchoolAttendance_DataAccessLayer.Repositories;
using SchoolAttendance_EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance_DataAccessLayer.EntityFramework
{
    public class EfAppUserDal : GenericRepository<ApplicationUser>, IAppUserDal
    {
        
        public EfAppUserDal(ApplicationDbContext context) : base(context)
        {
        }
        

    }
}
