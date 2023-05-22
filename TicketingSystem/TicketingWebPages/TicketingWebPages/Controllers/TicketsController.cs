using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Newtonsoft.Json;
using TicketingSystem.DAL.Entities;

namespace TicketingWebPages.Controllers
{
    public class TicketsController : Controller
    {
        private readonly IHttpClientFactory _httpClient;

        public TicketsController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> GetTicket(Guid? id)
        {
            try
            {
                var url = String.Format("https://localhost:7255/api/Tickets/ScanTicket/{0}", id);
                var json = await _httpClient.CreateClient().GetStringAsync(url);
                Ticket ticket = JsonConvert.DeserializeObject<Ticket>(json);

                if (ticket.isUsed == false)
                {
                    return RedirectToAction("Edit");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return RedirectToAction("TicketNotFound");
            }
        }

        [HttpGet]
        public async Task<IActionResult> TicketNotFound()
        {
            return View();
        }



        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            try
            {
                var url = String.Format("https://localhost:7255/api/Tickets/ScanTicket/{0}", id);
                var json = await _httpClient.CreateClient().GetStringAsync(url);
                Ticket ticket = JsonConvert.DeserializeObject<Ticket>(json);
                return View(ticket);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, Ticket ticket)
        {
            try
            {
                var url = String.Format("https://localhost:7255/api/Tickets/EditTicket/{0}", id);
                await _httpClient.CreateClient().PutAsJsonAsync(url, ticket);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }

        }
    }
}
