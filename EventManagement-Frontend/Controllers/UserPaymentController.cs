using EventManagement_Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace EventManagement_Frontend.Controllers
{
    public class UserPaymentController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public UserPaymentController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _config = configuration;
        }

        // Show form for payment processing
        [HttpGet]
        public IActionResult Index(int seats)
        {
            int seat = seats;
            int amount = seats * 100;
            string userid = HttpContext.Session.GetString("UserId");
            var modelData = TempData["BookingData"] as string;
            ProcessPaymentViewModel viewModel = new ProcessPaymentViewModel();
            viewModel.TotalAmount = amount;
            viewModel.UserId = HttpContext.Session.GetString("UserId");
            viewModel.EventId = 3;
            //ViewBag.amount = amount;
            //ViewBag.userId = HttpContext.Session.GetString("UserId"); 
            return View(viewModel);
        }

        // Handle form submission
        [HttpPost]
        public async Task<IActionResult> Index(ProcessPaymentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Return with validation errors
            }

            var apiUrl = "https://localhost:7117/api/payments";
            var jsonData = JsonConvert.SerializeObject(model); // Using Newtonsoft.Json here
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // Send data to backend API
            var response = await _httpClient.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();

                // Deserialize using Newtonsoft.Json
                var paymentResult = JsonConvert.DeserializeObject<PaymentResultViewModel>(responseData);

                return RedirectToAction("PaymentResult", paymentResult); // Show result in the next view
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync(); // Get error details
                ModelState.AddModelError(string.Empty, $"Payment processing failed: {errorContent}");
                return View(model);
            }
        }
        public IActionResult PaymentResult(decimal amount)
        {

            var model = new PaymentResultViewModel
            {
                Success = true,
                Message = $"Your total paid amount is {amount}.",
                TotalAmount = amount
            };

            return View(model);
        }


    }
}
