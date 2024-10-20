using EventManagement_Frontend.IService;
using EventManagement_Frontend.Models;
using Newtonsoft.Json;

namespace EventManagement_Frontend.Services
{
    public class ProfileService : IProfileService
    {
        private readonly List<ProfileModel> _profile = new List<ProfileModel>();
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private string _url = "";

        public ProfileService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
            _url = _config["ApiSettings:BaseUrl"] + "/user";
            if (string.IsNullOrEmpty(_url))
            {
                throw new ArgumentNullException(nameof(_url), "The base URL for the API cannot be null or empty.");
            }
            _httpClient.BaseAddress = new Uri(_url);

        }
        public async Task<List<ProfileModel>> GetAllProfile()
        {
            if (_profile.Any())
            {
                return _profile;
            }
            var response = await _httpClient.GetAsync($"{_url}");


            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                var fetchedProfile = JsonConvert.DeserializeObject<List<ProfileModel>>(jsonString);

                if (fetchedProfile != null && fetchedProfile.Any())
                {
                    _profile.AddRange(fetchedProfile);
                }

                return fetchedProfile;
            }

            return new List<ProfileModel>();
        }
    }
}
