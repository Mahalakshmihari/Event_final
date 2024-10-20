using EventManagement_Frontend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace EventManagement_Frontend.Controllers
{
    public class AdminController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        public AdminController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _config = configuration;
        }
        //[Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
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
                    var userid = tokenResponse["userid"];
                    var username = tokenResponse["username"];
                    TempData["Token"] = token;
                    TempData["SuccessMessage"] = "Login success!";
                    TempData["userid"] = userid;

                    // Store user information in session
                    //HttpContext.Session.SetString("UserName", login.Username);
                    //HttpContext.Session.SetString("UserId", userid);




                    string str = JsonConvert.SerializeObject(new ModelMaza
                    {
                        UserId = userid

                    });
                    HttpContext.Session.SetString("str", str);

                    Console.WriteLine(HttpContext.Session.GetString("UserId"));
                    //var user = LoginModel(login);
                    //if (user != null)
                    //{
                    // Pass username to the view model
                    var userName = new LoginModel { Username = login.Username };
                    return RedirectToAction("Index", "Admin", userName);
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

    }
}
