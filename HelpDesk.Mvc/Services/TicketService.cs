using HelpDesk.Api.Models;

namespace HelpDesk.Mvc.Services
{
    public class TicketService
    {
        private readonly HttpClient _httpClient;

        public TicketService(HttpClient httpClient)
        {
            _httpClient = httpClient;
           
            _httpClient.BaseAddress = new Uri("https://localhost:7042/api/Ticket/");
        }

        public async Task<List<Ticket>> GetAllTicketsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Ticket>>("All") ?? new List<Ticket>();
        }

        public async Task<Ticket?> GetTicketByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Ticket>($"{id}");
        }

        public async Task CreateTicketAsync(Ticket ticket)
        {
            await _httpClient.PostAsJsonAsync("", ticket);
        }

        public async Task UpdateTicketAsync(int id, Ticket ticket)
        {
            await _httpClient.PutAsJsonAsync($"{id}", ticket);
        }

        public async Task DeleteTicketAsync(int id)
        {
            await _httpClient.DeleteAsync($"{id}");
        }

        public async Task<List<Ticket>> GetTicketsByStatusAsync(string status)
        {
            return await _httpClient.GetFromJsonAsync<List<Ticket>>($"Status/{status}") ?? new List<Ticket>();
        }
    }
}