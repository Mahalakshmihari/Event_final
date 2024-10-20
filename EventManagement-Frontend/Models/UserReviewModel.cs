using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EventManagement_Frontend.Models
{
    public class UserReviewModel
    {
        public int ReviewId { get; set; }
        [JsonPropertyName("userId")]
        [Required(ErrorMessage = "Event Name is required")]
        public string? UserId { get; set; } = null!;
        [JsonPropertyName("eventId")]
        [Required(ErrorMessage = "Event Name is required")]
        public int? EventId { get; set; }
        [JsonPropertyName("rating")]
        [Required(ErrorMessage = "Event Name is required")]
        public decimal Rating { get; set; }
        [JsonPropertyName("comment")]
        [Required(ErrorMessage = "Event Name is required")]
        public string Comment { get; set; } = null!;
        [JsonPropertyName("reviewDate")]
        [Required(ErrorMessage = "Event Name is required")]
        public DateTime ReviewDate { get; set; }
    }
}
