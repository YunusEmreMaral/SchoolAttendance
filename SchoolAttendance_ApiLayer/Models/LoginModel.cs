namespace SchoolAttendance_ApiLayer.Models
{
    public class LoginModel
    {
        public string SchoolNumber { get; set; } // Kullanıcı giriş için SchoolNumber'ı kullanıyor
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }

}
