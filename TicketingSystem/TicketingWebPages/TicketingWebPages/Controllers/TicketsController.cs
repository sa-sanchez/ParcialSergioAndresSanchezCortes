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
            var url = String.Format("https://localhost:7255/api/Tickets/ScanTicket/{0}", id);
            var json = await _httpClient.CreateClient().GetStringAsync(url);
            Ticket scanning = JsonConvert.DeserializeObject<Ticket>(json);
            return View(scanning);
        }
    }
}
