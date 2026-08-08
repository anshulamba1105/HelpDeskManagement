using HelpDesk.Api.Controllers;
using HelpDesk.Api.Models;
using HelpDesk.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HelpDesk.Tests
{
    public class TicketControllerTests
    {
        private readonly Mock<ITicketRepository> _mockRepo;
        private readonly TicketController _controller;

        public TicketControllerTests()
        {
            // Initialize the mock repository and inject it into the controller
            _mockRepo = new Mock<ITicketRepository>();
            _controller = new TicketController(_mockRepo.Object);
        }

        // 1. GetAllTickets_ReturnsOkResult_WhenTicketsExist
        [Fact]
        public async Task GetAllTickets_ReturnsOkResult_WhenTicketsExist()
        {
            // Arrange
            var mockTickets = new List<Ticket>
            {
                new Ticket { Id = 1, Title = "Login Issue", Status = "Open" },
                new Ticket { Id = 2, Title = "Hardware Failure", Status = "Closed" }
            };
            _mockRepo.Setup(repo => repo.GetAllTicketsAsync()).ReturnsAsync(mockTickets);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnTickets = Assert.IsType<List<Ticket>>(okResult.Value);
            Assert.Equal(2, returnTickets.Count);
        }

        // 2. GetTicketById_ReturnsOkResult_WhenTicketExists
        [Fact]
        public async Task GetTicketById_ReturnsOkResult_WhenTicketExists()
        {
            // Arrange
            var mockTicket = new Ticket { Id = 1, Title = "Login Issue" };
            _mockRepo.Setup(repo => repo.GetTicketByIdAsync(1)).ReturnsAsync(mockTicket);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnTicket = Assert.IsType<Ticket>(okResult.Value);
            Assert.Equal(1, returnTicket.Id);
        }

        // 3. GetTicketById_ReturnsNotFound_WhenTicketDoesNotExist
        [Fact]
        public async Task GetTicketById_ReturnsNotFound_WhenTicketDoesNotExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetTicketByIdAsync(99)).ReturnsAsync((Ticket)null);

            // Act
            var result = await _controller.GetById(99);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        // 4. CreateTicket_ReturnsOkResult_WhenTicketIsCreatedSuccessfully
        [Fact]
        public async Task CreateTicket_ReturnsOkResult_WhenTicketIsCreatedSuccessfully()
        {
            // Arrange
            var newTicket = new Ticket { Title = "Network Down", Status = "Open" };
            _mockRepo.Setup(repo => repo.CreateTicketAsync(newTicket)).ReturnsAsync(1);

            // Act
            var result = await _controller.Create(newTicket);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(1, okResult.Value);
        }

        // 5. CreateTicket_ReturnsBadRequest_WhenTicketIsNull
        [Fact]
        public async Task CreateTicket_ReturnsBadRequest_WhenTicketIsNull()
        {
            // Arrange
            Ticket nullTicket = null;

            // Act
            var result = await _controller.Create(nullTicket);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        // 6. GetTicketsByStatus_ReturnsOkResult_WhenMatchingTicketsExist
        [Fact]
        public async Task GetTicketsByStatus_ReturnsOkResult_WhenMatchingTicketsExist()
        {
            // Arrange
            var status = "Open";
            var mockTickets = new List<Ticket>
            {
                new Ticket { Id = 1, Title = "Login Issue", Status = "Open" }
            };
            _mockRepo.Setup(repo => repo.GetTicketsByStatusAsync(status)).ReturnsAsync(mockTickets);

            // Act
            var result = await _controller.GetByStatus(status);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnTickets = Assert.IsType<List<Ticket>>(okResult.Value);
            Assert.Single(returnTickets);
            Assert.Equal(status, returnTickets[0].Status);
        }
    }
}
