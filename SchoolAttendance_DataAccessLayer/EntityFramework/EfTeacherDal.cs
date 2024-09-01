using SchoolAttendance_DataAccessLayer.Abstract;
using SchoolAttendance_DataAccessLayer.Concrete;
using SchoolAttendance_DataAccessLayer.Repositories;
using SchoolAttendance_EntityLayer.Concrete;

namespace SchoolAttendance_DataAccessLayer.EntityFramework
{
    public class EfTeacherDal : GenericRepository<Teacher>, ITeacherDal
    {
        public EfTeacherDal(ApplicationDbContext context) : base(context)
        {
        }
    }
}
