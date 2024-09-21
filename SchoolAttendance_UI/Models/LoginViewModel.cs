using System.ComponentModel.DataAnnotations;

namespace SchoolAttendance_UI.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "School Number is required")]
        [Display(Name = "School Number")]
        public string SchoolNumber { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }

}
