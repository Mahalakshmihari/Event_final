using EventManagement_Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace EventManagement_Frontend.Controllers
{
    public class UserReviewController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        public UserReviewController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _config = configuration;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var apiUrl = _config["ApiSettings:BaseUrl"] + "/Reviews";
                var response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var review = JsonSerializer.Deserialize<List<UserReviewModel>>(content);
                    return View(review);
                }

                // Return an empty list in case of failure, to avoid NullReferenceException
                return View(new List<UserReviewModel>());
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }


        // GET: Reviews/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reviews/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReviewId,UserId,EventId,Rating,Comment,ReviewDate")] UserReviewModel review)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var apiUrl = _config["ApiSettings:BaseUrl"] + "/Reviews";
                    var json = JsonSerializer.Serialize(review);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await _httpClient.PostAsync(apiUrl, content);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        ModelState.AddModelError(string.Empty, $"Error creating review: {errorContent}");
                    }
                }
                return View(review);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        /*  // GET: Reviews/Delete/5
          public async Task<IActionResult> Delete(int? id)
          {
              if (id == null)
              {
                  return NotFound();
              }

              var apiUrl = _config["ApiSettings:BaseUrl"] + $"/Reviews/{id}";
              var response = await _httpClient.GetAsync(apiUrl);
              if (response.IsSuccessStatusCode)
              {
                  var content = await response.Content.ReadAsStringAsync();
                  var review = JsonSerializer.Deserialize<ReviewModel>(content);
                  if (review == null)
                  {
                      return NotFound();
                  }
                  return View(review);
              }
              return NotFound();
          }

          // POST: Reviews/Delete/5
          [HttpDelete, ActionName("Delete")]
          [ValidateAntiForgeryToken]
          public async Task<IActionResult> DeleteConfirmed(int id)
          {
              var response = await _httpClient.DeleteAsync($"https://localhost:7117/api/Reviews/{id}");
              response.EnsureSuccessStatusCode();
              var _reviews = _reviewss.FirstOrDefault(c => c.ReviewId == id);
              if (_reviews != null)
              {
                  _reviewss.Remove(_reviews);
              }
              if (response.IsSuccessStatusCode)
              {
                  return RedirectToAction(nameof(Index));
              }
              return View();


              *//* var apiUrl = _config["ApiSettings:BaseUrl"] + $"/Reviews/{id}";
               var response = await _httpClient.DeleteAsync(apiUrl);
               if (response.IsSuccessStatusCode)
               {
                   return RedirectToAction(nameof(Index));

               }
               return View();*//*
          }*/
    }
}
