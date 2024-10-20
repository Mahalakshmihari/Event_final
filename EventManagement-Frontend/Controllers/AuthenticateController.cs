using EventManagement_Frontend.Models;
using EventManagement_Frontend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Template;
using Newtonsoft.Json;
using System.Text;

namespace EventManagement_Frontend.Controllers
{
    public class AuthenticateController : Controller
    {
            private readonly HttpClient _httpClient;
            private readonly IConfiguration _config;
            public AuthenticateController(HttpClient httpClient, IConfiguration configuration)
            {
                _httpClient = httpClient;
                _config = configuration;
            }
            public IActionResult Index()
            {
                var token = TempData["Token"] as string;
                ViewBag.Token = token;
                return View();
            }

            [HttpGet]
            public IActionResult Login()
            {
                return View();
            }

            [HttpPost]
            public async Task<IActionResult> Login(LoginModel login)
            {
                if (ModelState.IsValid)
                {
                    var json = JsonConvert.SerializeObject(login);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await _httpClient.PostAsync($"{_config["ApiSettings:AuthURL"]}login", content);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        var tokenResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString);
                        var token = tokenResponse["token"];
                    var userid=tokenResponse["userid"];
                    var username=tokenResponse["username"];
                        TempData["Token"] = token;
                        TempData["SuccessMessage"] = "Login success!";
                    TempData["userid"] = userid;

                    // Store user information in session
                    HttpContext.Session.SetString("UserName", login.Username);
                    HttpContext.Session.SetString("UserId", userid);
                    string str = JsonConvert.SerializeObject(new ModelMaza {
                     UserId = userid
                    
                    });
                    HttpContext.Session.SetString("str", str);

                    Console.WriteLine(HttpContext.Session.GetString("UserId"));
                    //var user = LoginModel(login);
                    //if (user != null)
                    //{
                        // Pass username to the view model
                        var userName = new LoginModel { Username = login.Username };
                        return RedirectToAction("HomePage", "Home", userName);
                    //}
                }
                    else
                    {
                        TempData["ErrorMessage"] = "Login Failed";
                        ModelState.AddModelError("", "Login failed");
                        return View();
                    }

                }
                else
                {
                    {
                        TempData["ErrorMessage"] = "Login failed";
                        return View();
                    }
                }
            }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // Clear the session data
            HttpContext.Session.Clear();
            TempData["LogoutMessage"] = "Logout successfully.";
            return RedirectToAction("EvenAppPage", "Home");
        }


        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(UserRegisteration userRegisteration)
        {
            // Check if the model is valid
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid form data";
                return View(userRegisteration);  // Return the view with the current model so that data persists
            }

            // Convert the model to JSON
            var json = JsonConvert.SerializeObject(userRegisteration);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //try
            //{
            //    var response = await _httpClient.PostAsync($"{_config["ApiSettings:AuthURL"]}register", content);

            //    if (response.IsSuccessStatusCode)
            //    {
            //        TempData["SuccessMessage"] = "User Added Successfully";
            //        // Redirect to the Login page after successful registration
            //        return RedirectToAction("HomePage", "Home");
            //    }
            //    else
            //    {
            //        var errorContent = await response.Content.ReadAsStringAsync();
            //        TempData["ErrorMessage"] = "Registration failed. Server responded with an error.";
            //        ModelState.AddModelError("", $"Registration failed: {errorContent}");
            //        return View("Registeration");
            //    }
            //}
            try
            {
                var response = await _httpClient.PostAsync($"{_config["ApiSettings:AuthURL"]}register", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "User Added Successfully";


                    try
                    {
                        // Your logic to save the user details
                        var notificationService = new NotificationServices();
                        var result = notificationService.Notification(userRegisteration.Email, userRegisteration.Email);

                        // You can log the result if needed, or show it to the user
                        TempData["NotificationResult"] = result;

                        // Redirect to the Login page after successful registration
                        return RedirectToAction("Login", "Authenticate");
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }

                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    TempData["ErrorMessage"] = "Registration failed. Server responded with an error.";
                    ModelState.AddModelError("", $"Registration failed: {errorContent}");
                    return View("Registeration");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while processing your request.";
                ModelState.AddModelError("", ex.Message);
                return View("Registeration");
            }
        }


    }
}
