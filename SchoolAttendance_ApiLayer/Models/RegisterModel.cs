﻿namespace SchoolAttendance_ApiLayer.Models
{
    public class RegisterModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string SchoolNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

}
