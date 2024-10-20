
using EventManagement_Frontend.IService;
using EventManagement_Frontend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EventManagement_Frontend.Controllers
{
    public class TicketController : Controller
    {
        
        [HttpGet]
        public IActionResult Create(int maxSeats, string seatIds)
        {
            TempData.Keep();
            //return View(new TicketModel() { NumberOfTickets= maxSeats, SeatId=Convert.ToIn    t32(seatIds)}); 
            TicketModel ticket = new TicketModel() { NumberOfTickets=maxSeats,SeatId = Convert.ToInt32(seatIds) };
            return View(ticket);
        }

        // POST: Ticket/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TicketModel model)
        {
            TempData.Keep();
            if (ModelState.IsValid)
            {

                model.BookingID = new System.Random().Next(1, 1000);
                model.TotalAmount = model.NumberOfTickets * 100;
                TempData["amount"] = model.TotalAmount;
                HttpContext.Session.SetString("amount",model.TotalAmount.ToString());
                //TicketModel newModel = await _ticketService.AddTicket(model);
                // Store model data in TempData
                TempData["BookingData"] = JsonConvert.SerializeObject(model);

                // Redirect to the confirmation page
                //return RedirectToAction("Con")
                return RedirectToAction("Index","UserPayment");
            }
            //ViewBag.AvailableSeats = GetAvailableSeats(); 
            return View(model);
        }

        public IActionResult Confirmation()
        {
            // Retrieve model data from TempData
            var modelData = TempData["BookingData"] as string;
            TempData.Keep();
            if (!string.IsNullOrEmpty(modelData))
            {
                var model = JsonConvert.DeserializeObject<TicketModel>(modelData);
                return RedirectToAction("Index", "UserPayment");
                return View(model); // Pass the model to the view
            }

            return RedirectToAction("Index","PayementResults");
        }

        private List<int> GetAvailableSeats()
        {
            // Simulate available seats (e.g., from a database)
            var availableSeats = new List<int>();
            for (int i = 1; i <= 40; i++)
            {
                availableSeats.Add(i);
            }
            return availableSeats; // Return list of 40 seats
        }
    }
}
