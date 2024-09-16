using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance_EntityLayer.Concrete
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string SchoolNumber { get; set; } // Hem öğretmen hem öğrenci için kullanılabilir.
        public ICollection<Attendance> Attendances { get; set; }
        public ICollection<Course> Courses { get; set; } // Öğretmen için ders ilişkisi
    }
}
