using SchoolAttendance_BusinessLayer.Abstract;
using SchoolAttendance_DataAccessLayer.Abstract;
using SchoolAttendance_EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SchoolAttendance_BusinessLayer.Concrete
{
    public class TeacherManager : ITeacherService
    {
        private readonly ITeacherDal _teacherDal;

        public TeacherManager(ITeacherDal teacherDal)
        {
            _teacherDal = teacherDal;
        }

        public async Task TAddAsync(Teacher t)
        {
            if (t == null)
                throw new ArgumentNullException(nameof(t));

            await _teacherDal.AddAsync(t);
        }

        public async Task TDeleteAsync(Teacher t)
        {
            if (t == null)
                throw new ArgumentNullException(nameof(t));

            await _teacherDal.DeleteAsync(t);
        }

        public async Task<Teacher> TGetByIDAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID must be greater than 0", nameof(id));

            return await _teacherDal.GetByIDAsync(id);
        }

        public async Task<List<Teacher>> TGetListAsync()
        {
            return await _teacherDal.GetListAsync();
        }

        public async Task<List<Teacher>> TGetListByFilterAsync(Expression<Func<Teacher, bool>> filter)
        {
            if (filter == null)
                throw new ArgumentNullException(nameof(filter));

            return await _teacherDal.GetByFilterAsync(filter);
        }

        public async Task TUpdateAsync(Teacher t)
        {
            if (t == null)
                throw new ArgumentNullException(nameof(t));

            await _teacherDal.UpdateAsync(t);
        }
    }
}
