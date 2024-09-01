using SchoolAttendance_BusinessLayer.Abstract;
using SchoolAttendance_DataAccessLayer.Abstract;
using SchoolAttendance_EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SchoolAttendance_BusinessLayer.Concrete
{
    public class StudentManager : IStudentService
    {
        private readonly IStudentDal _studentDal;

        public StudentManager(IStudentDal studentDal)
        {
            _studentDal = studentDal;
        }

        public async Task TAddAsync(Student t)
        {
            if (t == null)
                throw new ArgumentNullException(nameof(t));

            await _studentDal.AddAsync(t);
        }

        public async Task TDeleteAsync(Student t)
        {
            if (t == null)
                throw new ArgumentNullException(nameof(t));

            await _studentDal.DeleteAsync(t);
        }

        public async Task<Student> TGetByIDAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID must be greater than 0", nameof(id));

            return await _studentDal.GetByIDAsync(id);
        }

        public async Task<List<Student>> TGetListAsync()
        {
            return await _studentDal.GetListAsync();
        }

        public async Task<List<Student>> TGetListByFilterAsync(Expression<Func<Student, bool>> filter)
        {
            if (filter == null)
                throw new ArgumentNullException(nameof(filter));

            return await _studentDal.GetByFilterAsync(filter);
        }

        public async Task TUpdateAsync(Student t)
        {
            if (t == null)
                throw new ArgumentNullException(nameof(t));

            await _studentDal.UpdateAsync(t);
        }
    }
}
