﻿using Microsoft.Extensions.DependencyInjection;
using SchoolAttendance_BusinessLayer.Abstract;
using SchoolAttendance_BusinessLayer.Concrete;
using SchoolAttendance_DataAccessLayer.Abstract;
using SchoolAttendance_DataAccessLayer.EntityFramework;

namespace SchoolAttendance_API
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            // Registering Business Layer Services
            services.AddScoped<IAttendanceService, AttendanceManager>();
            services.AddScoped<ICourseService, CourseManager>();
            services.AddScoped<IStudentService, StudentManager>();
            services.AddScoped<ITeacherService, TeacherManager>();

            // Registering Data Access Layer Services
            services.AddScoped<IAttendanceDal, EfAttendanceDal>();
            services.AddScoped<ICourseDal, EfCourseDal>();
            services.AddScoped<IStudentDal, EfStudentDal>();
            services.AddScoped<ITeacherDal, EfTeacherDal>();
        }
    }
}
