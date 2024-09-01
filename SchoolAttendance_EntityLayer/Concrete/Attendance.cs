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
        public int StudentId { get; set; } // FK
        public DateTime Timestamp { get; set; } // Yoklamanın alındığı zaman
        public Course Course { get; set; } // İlişki
        public Student Student { get; set; } // İlişki
    }
}
