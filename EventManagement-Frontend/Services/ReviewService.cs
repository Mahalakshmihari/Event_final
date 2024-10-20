using EventManagement_Backend.Models;
using EventManagement_Frontend.IService;
using EventManagement_Frontend.Models;
using Newtonsoft.Json;

using System.Text.Json;

namespace EventManagement_Frontend.Services
{
    public class ReviewService : IReviewService
    {
        private readonly List<ReviewModel> _reviews = new List<ReviewModel>();
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private string _url = "";

        public ReviewService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
            _url = $"{_config["ApiSettings:BaseUrl"].TrimEnd('/')}/Reviews";
        }

        public async Task<List<ReviewModel>> GetAllReviews()
        {
        
            var response = await _httpClient.GetAsync(_url);
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var reviews = System.Text.Json.JsonSerializer.Deserialize<List<ReviewModel>>(jsonData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return reviews ?? new List<ReviewModel>();
            }

            return new List<ReviewModel>(); 
        }
    }
}
