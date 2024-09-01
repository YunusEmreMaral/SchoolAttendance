using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance_EntityLayer.Concrete
{
    public class Teacher
    {
        public int TeacherId { get; set; } // PK
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<Course> Courses { get; set; }

    }
}
