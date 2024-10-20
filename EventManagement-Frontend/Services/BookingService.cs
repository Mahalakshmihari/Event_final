using EventManagement_Frontend.IService;
using EventManagement_Frontend.Models;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using System.Text;
using Newtonsoft.Json;

namespace EventManagement_Frontend.Services
{
    public class BookingService : IBookingService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private ILogger<BookingService> _logger;
        private readonly string _url;
        public BookingService(HttpClient httpClient, IConfiguration config, ILogger<BookingService> logger)
        {
            _httpClient = httpClient;
            _config = config;
            _logger = logger;
            //_url = $"{_config["ApiSettings:BaseUrl"].TrimEnd('/')}/Bookings";
            _url = _config["ApiSettings:BaseUrl"] + "/Booking";
        }

        // Fetch all bookings
        public async Task<List<BookingModel>> GetAllBookings()
        {
            var response = await _httpClient.GetAsync(_url);
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var bookings = System.Text.Json.JsonSerializer.Deserialize<List<BookingModel>>(jsonData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return bookings ?? new List<BookingModel>();
            }

            return new List<BookingModel>(); // Return an empty list if something goes wrong
        }
        public async Task<BookingModel> GetBookingDetails(int bookingId)
        {
            var response = await _httpClient.GetAsync($"{_url}/{bookingId}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var booking = System.Text.Json.JsonSerializer.Deserialize<BookingModel>(jsonData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return booking;
            }

            return null; // Return null if the booking is not found
        }



        public async Task<bool> AddBook(BookingModel bookingModel)
        {
            BookingModel booking = new BookingModel()
            {
               
            };
            //var jsonContent= JsonSerializer.Serialize<BookingModel>()
            var jsonContent = new StringContent(JsonConvert.SerializeObject(booking), Encoding.UTF8, "application/json");
            string url = _url;
            var response = await _httpClient.PostAsync(url, jsonContent);
            if (response.IsSuccessStatusCode)
            {
                string newBooking = await response.Content.ReadAsStringAsync();
               
                return true;
             
            }           
            return false;
        }
    }
}
