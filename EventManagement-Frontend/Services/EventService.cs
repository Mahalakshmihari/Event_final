using EventManagement_Backend.Models;
using EventManagement_Frontend.IService;
using EventManagement_Frontend.Models;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;

namespace EventManagement_Frontend.Services
{
    public class EventService : IEventService
    {
        private readonly List<EventModel> _events = new List<EventModel>();
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private string _url = "";

        public EventService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
            _url = _config["ApiSettings:BaseUrl"] + "/events";
            if (string.IsNullOrEmpty(_url))
            {
                throw new ArgumentNullException(nameof(_url), "The base URL for the API cannot be null or empty.");
            }
            _httpClient.BaseAddress = new Uri(_url);

        }
        public async Task<bool> CreateEvents(EventModel eventObj)
        {
            EventModel eventItem = new EventModel()
            {
                EventName = eventObj.EventName,
                Description = eventObj.Description,
                CategoryId = eventObj.CategoryId,
                StartDate = eventObj.StartDate,
                EndDate = eventObj.EndDate,
                Price = eventObj.Price,
                TotalTickets = eventObj.TotalTickets,
                Location = eventObj.Location,
                AvailableTickets = eventObj.AvailableTickets,
                ImageURL = eventObj.ImageURL
            };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(eventItem), Encoding.UTF8, "application/json");
            string url = _url;
            var response = await _httpClient.PostAsync(url, jsonContent);
            if (response.IsSuccessStatusCode)
            {
                string newCategory = await response.Content.ReadAsStringAsync();
                return true;

            }

            return false;
        }
        public async Task<EventModel> GetEventsById(int eventId)
        {

            var response = await _httpClient.GetAsync($"{_url}/{eventId}");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var eventModel = JsonConvert.DeserializeObject<EventModel>(jsonString);
                return eventModel;
            }
            return null;

        }

        public async Task<bool> DeleteEvents(int eventId)
        {
            var response = await _httpClient.DeleteAsync($"{_url}/{eventId}");
            if (response.IsSuccessStatusCode)
            {
                _events.RemoveAll(c => c.CategoryId == eventId);
                return true;
            }
            return false;
        }

        public async Task<List<EventModel>> GetEvents()
        {
            if (_events.Any())
            {
                return _events;
            }
            var response = await _httpClient.GetAsync($"{_url}");


            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                var fetchedCategories = JsonConvert.DeserializeObject<List<EventModel>>(jsonString);

                if (fetchedCategories != null && fetchedCategories.Any())
                {
                    _events.AddRange(fetchedCategories);
                }

                return fetchedCategories;
            }

            return new List<EventModel>();
        }

       

        public async Task<bool> UpdateEvents(int eventId, EventModel updatedEvent)
        {
            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(updatedEvent),
                Encoding.UTF8,
                "application/json");
            string url = _url + "/" + eventId;
            var response = await _httpClient.PutAsync(url, jsonContent);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
