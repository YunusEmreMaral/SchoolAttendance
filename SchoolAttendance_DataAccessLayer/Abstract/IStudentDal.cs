using SchoolAttendance_EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance_DataAccessLayer.Abstract
{
    public interface IStudentDal: IGenericDal<Student>
    {
    }
}
