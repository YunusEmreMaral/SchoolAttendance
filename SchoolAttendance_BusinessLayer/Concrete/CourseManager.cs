﻿using SchoolAttendance_BusinessLayer.Abstract;
using SchoolAttendance_DataAccessLayer.Abstract;
using SchoolAttendance_EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SchoolAttendance_BusinessLayer.Concrete
{
    public class CourseManager : ICourseService
    {
        private readonly ICourseDal _courseDal;

        public CourseManager(ICourseDal courseDal)
        {
            _courseDal = courseDal;
        }

        public async Task TAddAsync(Course t)
        {
            if (t == null)
                throw new ArgumentNullException(nameof(t));

            await _courseDal.AddAsync(t);
        }

        public async Task TDeleteAsync(Course t)
        {
            if (t == null)
                throw new ArgumentNullException(nameof(t));

            await _courseDal.DeleteAsync(t);
        }

        public async Task<Course> TGetByIDAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID must be greater than 0", nameof(id));

            return await _courseDal.GetByIDAsync(id);
        }

        public async Task<List<Course>> TGetListAsync()
        {
            return await _courseDal.GetListAsync();
        }

        public async Task<List<Course>> TGetListByFilterAsync(Expression<Func<Course, bool>> filter)
        {
            if (filter == null)
                throw new ArgumentNullException(nameof(filter));

            return await _courseDal.GetByFilterAsync(filter);
        }

        public async Task TUpdateAsync(Course t)
        {
            if (t == null)
                throw new ArgumentNullException(nameof(t));

            await _courseDal.UpdateAsync(t);
        }
    }
}
