using System.ComponentModel.DataAnnotations;

namespace EventManagement_Backend.Authentication
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "valid username  is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "valid email  is required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "valid password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "valid Firstname  is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "valid Firstname  is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "valid Phone Numberis required")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "valid Firstname  is required")]
        public string Address { get; set; }
    }
}
