using Microsoft.Extensions.DependencyInjection;
using SchoolAttendance_BusinessLayer.Abstract;
using SchoolAttendance_BusinessLayer.Concrete;
using SchoolAttendance_BusinessLayer.Container;
using SchoolAttendance_DataAccessLayer.Abstract;
using SchoolAttendance_DataAccessLayer.Abstract.SchoolAttendance_DataAccessLayer.Abstract;
using SchoolAttendance_DataAccessLayer.Concrete;
using SchoolAttendance_DataAccessLayer.EntityFramework;
using SchoolAttendance_EntityLayer.Concrete;

namespace SchoolAttendance_API
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            // Registering Business Layer Services
            services.AddScoped<IAttendanceService, AttendanceManager>();
            services.AddScoped<ICourseService, CourseManager>();
            services.AddScoped<IQRCodeGeneratorService, QRCodeGeneratorService>();
            services.AddScoped<IAppUserService, AppUserService>();

            // Registering Data Access Layer Services
            services.AddScoped<IAttendanceDal, EfAttendanceDal>();
            services.AddScoped<ICourseDal, EfCourseDal>();
            services.AddScoped<IAppUserDal, EfAppUserDal>();
        }
    }
}
