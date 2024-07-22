using System.ComponentModel.DataAnnotations;

namespace BadmintonEventsWebApp.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Email Address")]
        [Required()]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
