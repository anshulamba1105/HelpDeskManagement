using HelpDesk.Api.Models;
using HelpDesk.Mvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Mvc.Controllers
{
    public class TicketController : Controller
    {
        private readonly TicketService _ticketService;

        public TicketController(TicketService ticketService)
        {
            _ticketService = ticketService;
        }

        // 1. Dashboard
        public async Task<IActionResult> Dashboard()
        {
            var tickets = await _ticketService.GetAllTicketsAsync();

            // Calculate dashboard metrics
            ViewBag.TotalTickets = tickets.Count;
            ViewBag.OpenTickets = tickets.Count(t => t.Status == "Open");
            ViewBag.ClosedTickets = tickets.Count(t => t.Status == "Closed");

            return View();
        }

        // 2. View All Tickets & Filter by Status
        public async Task<IActionResult> Index(string statusFilter)
        {
            List<Ticket> tickets;

            if (string.IsNullOrEmpty(statusFilter))
            {
                tickets = await _ticketService.GetAllTicketsAsync();
            }
            else
            {
                tickets = await _ticketService.GetTicketsByStatusAsync(statusFilter);
            }

            // Keep the selected filter in the ViewBag to persist the dropdown state
            ViewBag.CurrentFilter = statusFilter;
            return View(tickets);
        }

        // 3. View Ticket Details
        public async Task<IActionResult> Details(int id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket == null) return NotFound();

            return View(ticket);
        }

        // 4. Raise New Ticket (GET)
        public IActionResult Create()
        {
            return View();
        }

        // 4. Raise New Ticket (POST)
        [HttpPost]
        public async Task<IActionResult> Create(Ticket ticket)
        {
            // Status is hardcoded to "Open" as per assignment requirements
            ticket.Status = "Open";

            await _ticketService.CreateTicketAsync(ticket);
            return RedirectToAction(nameof(Index));
        }

        // 5. Edit Ticket (GET)
        public async Task<IActionResult> Edit(int id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket == null) return NotFound();

            return View(ticket);
        }

        // 5. Edit Ticket (POST)
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Ticket ticket)
        {
            if (id != ticket.Id) return BadRequest();

            await _ticketService.UpdateTicketAsync(id, ticket);
            return RedirectToAction(nameof(Index));
        }

        // 6. Delete Ticket (GET)
        public async Task<IActionResult> Delete(int id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket == null) return NotFound();

            return View(ticket);
        }

        // 6. Delete Ticket (POST)
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _ticketService.DeleteTicketAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}