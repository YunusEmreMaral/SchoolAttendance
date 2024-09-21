using SchoolAttendance_DataAccessLayer.Abstract;
using SchoolAttendance_DataAccessLayer.Concrete;
using SchoolAttendance_DataAccessLayer.Repositories;
using SchoolAttendance_EntityLayer.Concrete;

namespace SchoolAttendance_DataAccessLayer.EntityFramework
{
    public class EfCourseDal : GenericRepository<Course>, ICourseDal
    {
        public EfCourseDal(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Course>> GetByTeacherIdAsync(string teacherId)
        {
            return await GetByFilterAsync(c => c.TeacherId == teacherId);
        }

    }
}
