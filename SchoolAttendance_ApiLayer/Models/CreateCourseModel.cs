namespace SchoolAttendance_ApiLayer.Models
{
    public class CreateCourseModel
    {
        public string CourseName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime Date { get; set; }
        public string TeacherId { get; set; }
    }

}
