using System.ComponentModel.DataAnnotations;

namespace EventManagement_Backend.Authentication
{
    public class LoginModel
    {
        [Required(ErrorMessage = "valid user name  is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "valid password  is required")]
        public string Password { get; set; }
    }
}
