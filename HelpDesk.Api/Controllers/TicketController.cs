using HelpDesk.Api.Models;
using HelpDesk.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketRepository _repository;

        public TicketController(ITicketRepository repository)
        {
            _repository = repository;
        }

        // GET: /api/Ticket/All
        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            var tickets = await _repository.GetAllTicketsAsync();
            return Ok(tickets);
        }

        // GET: /api/Ticket/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ticket = await _repository.GetTicketByIdAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return Ok(ticket);
        }

        // POST: /api/Ticket
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Ticket ticket)
        {
            if (ticket == null)
            {
                return BadRequest();
            }

            var newId = await _repository.CreateTicketAsync(ticket);
            return Ok(newId);
        }

        // PUT: /api/Ticket/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Ticket ticket)
        {
            if (ticket == null || id != ticket.Id)
            {
                return BadRequest();
            }

            var existingTicket = await _repository.GetTicketByIdAsync(id);
            if (existingTicket == null)
            {
                return NotFound();
            }

            await _repository.UpdateTicketAsync(ticket);
            return Ok();
        }

        // DELETE: /api/Ticket/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingTicket = await _repository.GetTicketByIdAsync(id);
            if (existingTicket == null)
            {
                return NotFound();
            }

            await _repository.DeleteTicketAsync(id);
            return Ok();
        }

        // GET: /api/Ticket/Status/{status}
        [HttpGet("Status/{status}")]
        public async Task<IActionResult> GetByStatus(string status)
        {
            var tickets = await _repository.GetTicketsByStatusAsync(status);
            return Ok(tickets);
        }
    }
}