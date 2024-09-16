using SchoolAttendance_EntityLayer.Concrete;
using SchoolAttendance_DataAccessLayer.Abstract;
using SchoolAttendance_DataAccessLayer.Repositories;

namespace SchoolAttendance_DataAccessLayer.Concrete
{
    public class EfUserDal : GenericRepository<ApplicationUser>, IUserDal
    {
        public EfUserDal(ApplicationDbContext context) : base(context)
        {
        }
    }
}
