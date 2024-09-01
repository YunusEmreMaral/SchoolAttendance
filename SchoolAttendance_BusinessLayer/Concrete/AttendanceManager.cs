using SchoolAttendance_BusinessLayer.Abstract;
using SchoolAttendance_DataAccessLayer.Abstract;
using SchoolAttendance_EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SchoolAttendance_BusinessLayer.Concrete
{
    public class AttendanceManager : IAttendanceService
    {
        private readonly IAttendanceDal _attendanceDal;

        public AttendanceManager(IAttendanceDal attendanceDal)
        {
            _attendanceDal = attendanceDal;
        }

        public async Task TAddAsync(Attendance t)
        {
            if (t == null)
                throw new ArgumentNullException(nameof(t));

            await _attendanceDal.AddAsync(t);
        }

        public async Task TDeleteAsync(Attendance t)
        {
            if (t == null)
                throw new ArgumentNullException(nameof(t));

            await _attendanceDal.DeleteAsync(t);
        }

        public async Task<Attendance> TGetByIDAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID must be greater than 0", nameof(id));

            return await _attendanceDal.GetByIDAsync(id);
        }

        public async Task<List<Attendance>> TGetListAsync()
        {
            return await _attendanceDal.GetListAsync();
        }

        public async Task<List<Attendance>> TGetListByFilterAsync(Expression<Func<Attendance, bool>> filter)
        {
            if (filter == null)
                throw new ArgumentNullException(nameof(filter));

            return await _attendanceDal.GetByFilterAsync(filter);
        }

        public async Task TUpdateAsync(Attendance t)
        {
            if (t == null)
                throw new ArgumentNullException(nameof(t));

            await _attendanceDal.UpdateAsync(t);
        }
    }
}
