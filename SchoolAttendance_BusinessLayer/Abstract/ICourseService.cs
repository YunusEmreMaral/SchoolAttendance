using SchoolAttendance_EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance_BusinessLayer.Abstract
{
    public interface ICourseService:IGenericService<Course>
    {
        Task<string> GenerateQRCodeForCourseAsync(string courseName);

    }
}
