using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EventManagement_Frontend.Models
{
    public class EventModel
    {
        [JsonPropertyName("eventId")]
        public int EventId { get; set; }
        [JsonPropertyName("categoryId")]
        [Required(ErrorMessage = "Category Id is required")]
        //[StringLength(50)]
        public int CategoryId { get; set; }
        [JsonPropertyName("eventName")]
        [Required(ErrorMessage = "Event Name is required")]
        [StringLength(100)]
        public string EventName { get; set; }
        [JsonPropertyName("description")]
        [Required(ErrorMessage = "Description is required")]
        [StringLength(1000)]
        public string Description { get; set; }
        [JsonPropertyName("startDate")]
        [Required(ErrorMessage = "Start Date is required")]
        public DateTime StartDate { get; set; }
        [JsonPropertyName("endDate")]
        [Required(ErrorMessage = "End Date is required")]
        public DateTime EndDate { get; set; }
        [JsonPropertyName("location")]
        [Required(ErrorMessage = "Location is required")]
        [StringLength(200)]
        public string Location { get; set; }
        [JsonPropertyName("price")]
        [Required(ErrorMessage = "Price is required")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [JsonPropertyName("imageUrl")]
        [Required(ErrorMessage = "Image URL is required")]
        [StringLength(255)]
        public string ImageURL { get; set; }
        [JsonPropertyName("rating")]
        public double Rating { get; set; }
        [JsonPropertyName("totalTickets")]
        [Required(ErrorMessage = "Total Tickets is required")]
        public int TotalTickets { get; set; }
        [JsonPropertyName("availableTickets")]
        public int AvailableTickets { get; set; }
    }
}
