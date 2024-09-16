using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance_EntityLayer.Concrete
{
    public class Attendance
    {
        public int AttendanceId { get; set; } // PK
        public int CourseId { get; set; } // FK
        public string StudentId { get; set; } // FK (IdentityUser'dan gelen)
        public DateTime Timestamp { get; set; } // Yoklamanın alındığı zaman

        public Course Course { get; set; } // İlişki
        public ApplicationUser Student { get; set; } // İlişki (ApplicationUser ile)
    }

}
