using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using OrderApi.Controllers;
using OrderApi.DTOs;
using OrderApi.Services.Interfaces;

namespace OrderApi.Tests
{
    public class OrdersControllerTests
    {
        [Fact]
        public async Task GetAll_ReturnsOkWithList()
        {
            var mockService = new Mock<IOrderService>();
            var list = new List<OrderResponseDto>
            {
                new OrderResponseDto { Id = Guid.NewGuid(), CustomerName = "Alice", ProductName = "Item", Quantity = 1, TotalAmount = 10m, CreatedAt = DateTime.UtcNow }
            };
            mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(list);

            var controller = new OrdersController(mockService.Object);
            var result = await controller.GetAll();

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Same(list, ok.Value);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenNull()
        {
            var mockService = new Mock<IOrderService>();
            mockService.Setup(s => s.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((OrderResponseDto?)null);

            var controller = new OrdersController(mockService.Object);
            var result = await controller.GetById(Guid.NewGuid());

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsOkWithCreatedDto()
        {
            var mockService = new Mock<IOrderService>();
            var request = new OrderRequestDto { CustomerId = Guid.NewGuid(), ProductId = Guid.NewGuid(), Quantity = 2 };
            var response = new OrderResponseDto { Id = Guid.NewGuid(), CustomerName = "Bob", ProductName = "Widget", Quantity = 2, TotalAmount = 20m, CreatedAt = DateTime.UtcNow };
            mockService.Setup(s => s.CreateAsync(request)).ReturnsAsync(response);

            var controller = new OrdersController(mockService.Object);
            var result = await controller.Create(request);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Same(response, ok.Value);
        }

        [Fact]
        public async Task Create_ReturnsNotFound_WhenServiceThrowsInvalidOperationException()
        {
            var mockService = new Mock<IOrderService>();
            var request = new OrderRequestDto { CustomerId = Guid.NewGuid(), ProductId = Guid.NewGuid(), Quantity = 1 };
            mockService.Setup(s => s.CreateAsync(It.IsAny<OrderRequestDto>())).ThrowsAsync(new InvalidOperationException("Product not found"));

            var controller = new OrdersController(mockService.Object);
            var result = await controller.Create(request);

            var notFound = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Contains("Product not found", notFound.Value?.ToString() ?? string.Empty);
        }

        [Fact]
        public async Task Create_ReturnsBadRequest_WhenServiceThrowsArgumentException()
        {
            var mockService = new Mock<IOrderService>();
            var request = new OrderRequestDto { CustomerId = Guid.NewGuid(), ProductId = Guid.NewGuid(), Quantity = -1 };
            mockService.Setup(s => s.CreateAsync(It.IsAny<OrderRequestDto>())).ThrowsAsync(new ArgumentException("Invalid quantity"));

            var controller = new OrdersController(mockService.Object);
            var result = await controller.Create(request);

            var bad = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Contains("Invalid quantity", bad.Value?.ToString() ?? string.Empty);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenUpdated()
        {
            var mockService = new Mock<IOrderService>();
            mockService.Setup(s => s.UpdateAsync(It.IsAny<Guid>(), It.IsAny<OrderRequestDto>())).ReturnsAsync(true);

            var controller = new OrdersController(mockService.Object);
            var result = await controller.Update(Guid.NewGuid(), new OrderRequestDto());

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenDeleted()
        {
            var mockService = new Mock<IOrderService>();
            mockService.Setup(s => s.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(true);

            var controller = new OrdersController(mockService.Object);
            var result = await controller.Delete(Guid.NewGuid());

            Assert.IsType<NoContentResult>(result);
        }
    }
}
