using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Net;
using TicketingSystem.DAL;
using TicketingSystem.DAL.Entities;

namespace TicketingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly DataBaseContext _context;

        public TicketsController(DataBaseContext context)
        {
            _context = context;
        }

        [HttpGet, ActionName("Get")]
        [Route("ScanTicket/{id}")]

        public async Task<ActionResult<Ticket>> ScanTicket(Guid? id)
        {
            var ticket = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == id);

            if (ticket == null)
            {
                return NotFound("Invalid Ticket");
            }
            return Ok(ticket);
        }

        [HttpPut, ActionName("Edit")]
        [Route("EditTicket/{id}")]
        public async Task<ActionResult<Ticket>> EditTicket(Guid? id, Ticket ticket)
        {
            try
            {
                if (id != ticket.Id) return NotFound("Ticket not found");

                ticket.UseDate = DateTime.Now;
                ticket.isUsed = true;

                _context.Tickets.Update(ticket);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    return Conflict("Ticket update error");
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

            return Ok(ticket);
        }
    }

}
