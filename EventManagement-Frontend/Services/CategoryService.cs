using EventManagement_Frontend.IService;
using EventManagement_Frontend.Models;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Newtonsoft.Json;
using System.Text;

namespace EventManagement_Frontend.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly List<CategoryModel> _categories = new List<CategoryModel>();
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private string _url = "";

        public CategoryService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
            _url = _config["ApiSettings:BaseUrl"]+"/categories";
            if (string.IsNullOrEmpty(_url))
            {
                throw new ArgumentNullException(nameof(_url), "The base URL for the API cannot be null or empty.");
            }
            _httpClient.BaseAddress = new Uri(_url);

        }
        public async Task<bool> DeleteCategory(int categoryId)
        {
            var response = await _httpClient.DeleteAsync($"{_url}/{categoryId}"); 
            if (response.IsSuccessStatusCode)
            {
                _categories.RemoveAll(c => c.CategoryId == categoryId);
                return true;
            }
            return false;
        }
        public async Task<List<CategoryModel>> GetCategory()
        {
            if (_categories.Any())
            {
                return _categories; 
            }
            var response = await _httpClient.GetAsync($"{_url}");

    
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                var fetchedCategories = JsonConvert.DeserializeObject<List<CategoryModel>>(jsonString);

                if (fetchedCategories != null && fetchedCategories.Any())
                {
                    _categories.AddRange(fetchedCategories);
                }

                return fetchedCategories; 
            }

            return new List<CategoryModel>();
        }
        public async Task<bool> UpdateCategory(int categoryId, CategoryModel updatedCategory)
        {
            var jsonContent = new StringContent(
                           JsonConvert.SerializeObject(updatedCategory),
                           Encoding.UTF8,
                           "application/json");
            string url=_url+"/"+categoryId;
            var response = await _httpClient.PutAsync(url, jsonContent);
            if (response.IsSuccessStatusCode)
            {
                
                return true;
            }
            return false;
        }
        public async Task<bool> CreateCategory(string categoryName)
        {
            CategoryModel category = new CategoryModel()
            {
                CategoryName = categoryName
            };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");
            string url = _url;
            var response = await _httpClient.PostAsync(url, jsonContent);
            if (response.IsSuccessStatusCode)
            {
                string newCategory = await response.Content.ReadAsStringAsync();
                    return true;
               
            }
          
            return false;
        }
        public async Task<CategoryModel> GetCategoryById(int categoryId)
        {
            var response = await _httpClient.GetAsync($"{_url}/{categoryId}");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                var category = JsonConvert.DeserializeObject<CategoryModel>(jsonString);

                return category; 
            }

           
            return null;
        }
    }
}
