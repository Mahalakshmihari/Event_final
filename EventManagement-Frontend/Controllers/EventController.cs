using EventManagement_Backend.Models;
using EventManagement_Frontend.IService;
using EventManagement_Frontend.Models;
using EventManagement_Frontend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace EventManagement_Frontend.Controllers
{
    public class EventController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IEventService _eventService;
     
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        string _url = "";
        public EventController(HttpClient httpClient, IConfiguration configuration, IEventService eventService)
        {
            _eventService = eventService;
            _httpClient = httpClient;
            _config = configuration;
            _url= _config["ApiSettings:BaseUrl"] + "/events";
        }
        
        public async Task<IActionResult> AdminIndex()
        {
            var response = await _httpClient.GetAsync(_url);
            TempData.Keep();
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var events = JsonSerializer.Deserialize<List<EventModel>>(content);
                return View(events);
            }


            return View(new List<EventModel>());
        }
        public async Task<IActionResult> Index()
        {
            var events = await _eventService.GetEvents();
            return View(events);

            //var response = await _httpClient.GetAsync(_url);
            //TempData.Keep();
            //if (response.IsSuccessStatusCode)
            //{
            //    var content = await response.Content.ReadAsStringAsync();
            //    var events =JsonSerializer.Deserialize<List<EventModel>>(content);
            //    return View(events);
            //}


            //return View(new List<EventModel>());
        }

        // GET: Events/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apiUrl = _config["ApiSettings:BaseUrl"] + $"/events/{id}";
            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var eventDetails = System.Text.Json.JsonSerializer.Deserialize<EventModel>(content);

                if (eventDetails == null)
                {
                    return NotFound();
                }

                return View(eventDetails);
            }

            return NotFound(); // Handle error accordingly
        }

        // GET: Events/Create
      
        public IActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( EventModel eventItem)
        {
            if (ModelState.IsValid)
            {

                var result = await _eventService.CreateEvents(eventItem);
                if (result)
                {
                    return RedirectToAction(nameof(AdminIndex)); // Redirect to Index on success
                }
                ModelState.AddModelError("", "Error creating event Please try again."); // Show error
            }
            return View(eventItem);
        }

        // GET: Events/Edit/5
        [HttpPut]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apiUrl = _config["ApiSettings:BaseUrl"] + $"/events/{id}";
            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var eventDetails = JsonSerializer.Deserialize<EventModel>(content);

                if (eventDetails == null)
                {
                    return NotFound();
                }

                return View(eventDetails);
            }

            return NotFound();
        }

        // POST: Events/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventID,EventName,Description,Date,Location,Price")] EventModel @event)
        {
            if (id != @event.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var apiUrl = _config["ApiSettings:BaseUrl"] + $"/events/{id}";
                var jsonData = JsonSerializer.Serialize(@event);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(@event); 
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apiUrl = _config["ApiSettings:BaseUrl"] + $"/events/{id}";
            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var eventDetails = JsonSerializer.Deserialize<EventModel>(content);

                if (eventDetails == null)
                {
                    return NotFound();
                }

                return View(eventDetails);
            }

            return NotFound(); // Handle error accordingly
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var apiUrl = _config["ApiSettings:BaseUrl"] + $"/events/{id}";
            var response = await _httpClient.DeleteAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return NotFound(); // Handle error accordingly
        }
    }
}
