using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance_EntityLayer.Concrete
{
    public class Course
    {
        public int CourseId { get; set; } // PK
        public string CourseName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime Date { get; set; }
        public string QRCode { get; set; } // QR kodu veri

        public string TeacherId { get; set; } // FK (IdentityUser'dan gelen)
        public ApplicationUser Teacher { get; set; } // İlişki (A   pplicationUser ile)

        public ICollection<Attendance> Attendances { get; set; } // Derse ait yoklamalar
    }


}
