using SchoolAttendance_DataAccessLayer.Abstract;
using SchoolAttendance_DataAccessLayer.Concrete;
using SchoolAttendance_DataAccessLayer.Repositories;
using SchoolAttendance_EntityLayer.Concrete;

namespace SchoolAttendance_DataAccessLayer.EntityFramework
{
    public class EfStudentDal : GenericRepository<Student>, IStudentDal
    {
        public EfStudentDal(ApplicationDbContext context) : base(context)
        {
        }
    }
}
