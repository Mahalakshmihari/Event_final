using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EventManagement_Frontend.Models
{
    public class ProfileModel
    {
        [JsonPropertyName("id")]
        [Required(ErrorMessage = "Username is required")]
        public string Id { get; set; }

        [JsonPropertyName("userName")]
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [JsonPropertyName("firstName")]
        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [JsonPropertyName("lastName")]
        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50)]
        public string LastName { get; set; }
        [JsonPropertyName("email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [JsonPropertyName("phoneNumber")]
        [StringLength(15, ErrorMessage = "Phone number cannot exceed 15 characters")]
        public string PhoneNumber { get; set; }
        [JsonPropertyName("address")]
        public string Address { get; set; }
    }
}
