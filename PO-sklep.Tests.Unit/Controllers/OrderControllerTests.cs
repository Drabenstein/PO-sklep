using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PO_sklep.Controllers;
using PO_sklep.DTO;
using PO_sklep.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PO_sklep.Tests.Unit.Controllers
{
    public class OrderControllerTests
    {
        [Fact]
        public async Task CreateById_ValidData_ReturnsOk()
        {
            var id = 1;
            var expected = 10;
            var order = new OrderDto { DeliveryMethodId = 3, PaymentTypeId = 5, OrderItems = new List<OrderItemDto>() { new OrderItemDto { ProductId = 3, Count = 8 } } };
            var serviceMock = new Mock<IOrderService>();
            serviceMock
                .Setup(x => x.CreateOrderAsync(It.Is<int>(x => x == id), It.Is<OrderDto>(x => x.Equals(order))))
                .ReturnsAsync(expected);
            var controller = new OrderController(serviceMock.Object);

            var result = await controller.CreateById(id, order);

            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task CreateById_ValidData_ReturnsId()
        {
            var id = 1;
            var expected = 10;
            var order = new OrderDto { DeliveryMethodId = 3, PaymentTypeId = 5, OrderItems = new List<OrderItemDto>() { new OrderItemDto { ProductId = 3, Count = 8 } } };
            var serviceMock = new Mock<IOrderService>();
            serviceMock
                .Setup(x => x.CreateOrderAsync(It.Is<int>(x => x == id), It.Is<OrderDto>(x => x.Equals(order))))
                .ReturnsAsync(expected);
            var controller = new OrderController(serviceMock.Object);

            var result = await controller.CreateById(id, order);

            var resultData = Assert.IsType<OkObjectResult>(result);
            resultData.Value.Should().Be(expected);
        }

        [Fact]
        public async Task CreateById_InvalidOrder_ReturnsBadRequest()
        {
            var id = 1;
            var serviceMock = Mock.Of<IOrderService>();
            var controller = new OrderController(serviceMock);

            controller.ModelState.AddModelError("", "Invalid orderDto object");
            var result = await controller.CreateById(id, null);

            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task CreateById_ServiceThrowsException_ReturnsBadRequest()
        {
            var id = 1;
            var order = new OrderDto { DeliveryMethodId = 3, PaymentTypeId = 5, OrderItems = new List<OrderItemDto>() { new OrderItemDto { ProductId = 3, Count = 8 } } };
            var serviceMock = new Mock<IOrderService>();
            serviceMock
                .Setup(x => x.CreateOrderAsync(It.IsAny<int>(), It.IsAny<OrderDto>()))
                .ThrowsAsync(new Exception());
            var controller = new OrderController(serviceMock.Object);

            var result = await controller.CreateById(id, order);

            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task CreateById_ServiceReturnsNull_ReturnsBadRequest()
        {
            var id = 1;
            var order = new OrderDto { DeliveryMethodId = 3, PaymentTypeId = 5, OrderItems = new List<OrderItemDto>() { new OrderItemDto { ProductId = 3, Count = 8 } } };
            var serviceMock = new Mock<IOrderService>();
            serviceMock
                .Setup(x => x.CreateOrderAsync(It.IsAny<int>(), It.IsAny<OrderDto>()))
                .ReturnsAsync((int?)null);
            var controller = new OrderController(serviceMock.Object);

            var result = await controller.CreateById(id, order).ConfigureAwait(false);

            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task CreateById_ValidData_CallsService()
        {
            var id = 1;
            var order = new OrderDto { DeliveryMethodId = 3, PaymentTypeId = 5, OrderItems = new List<OrderItemDto>() { new OrderItemDto { ProductId = 3, Count = 8 } } };
            var serviceMock = new Mock<IOrderService>();
            serviceMock
                .Setup(x => x.CreateOrderAsync(It.IsAny<int>(), It.IsAny<OrderDto>()))
                .ReturnsAsync(10);
            var controller = new OrderController(serviceMock.Object);

            var result = await controller.CreateById(id, order);

            serviceMock.Verify(x => x.CreateOrderAsync(It.IsAny<int>(), It.IsAny<OrderDto>()));
        }

        [Fact]
        public async Task CreateByEmail_ValidData_ReturnsOk()
        {
            var email = "test@test.com";
            var expected = 10;
            var order = new OrderDto { DeliveryMethodId = 3, PaymentTypeId = 5, OrderItems = new List<OrderItemDto>() { new OrderItemDto { ProductId = 3, Count = 8 } } };
            var serviceMock = new Mock<IOrderService>();
            serviceMock
                .Setup(x => x.CreateOrderAsync(It.Is<string>(x => x == email), It.Is<OrderDto>(x => x.Equals(order))))
                .ReturnsAsync(expected);
            var controller = new OrderController(serviceMock.Object);

            var result = await controller.CreateByEmail(email, order);

            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task CreateByEmail_ValidData_ReturnsId()
        {
            var email = "test@test.com";
            var expected = 10;
            var order = new OrderDto { DeliveryMethodId = 3, PaymentTypeId = 5, OrderItems = new List<OrderItemDto>() { new OrderItemDto { ProductId = 3, Count = 8 } } };
            var serviceMock = new Mock<IOrderService>();
            serviceMock
                .Setup(x => x.CreateOrderAsync(It.Is<string>(x => x == email), It.Is<OrderDto>(x => x.Equals(order))))
                .ReturnsAsync(expected);
            var controller = new OrderController(serviceMock.Object);

            var result = await controller.CreateByEmail(email, order).ConfigureAwait(false);

            var resultData = Assert.IsType<OkObjectResult>(result);
            resultData.Value.Should().Be(expected);
        }

        [Fact]
        public async Task CreateByEmail_ServiceReturnsNull_ReturnsBadRequest()
        {
            var email = "test@test.com";
            var order = new OrderDto { DeliveryMethodId = 3, PaymentTypeId = 5, OrderItems = new List<OrderItemDto>() { new OrderItemDto { ProductId = 3, Count = 8 } } };
            var serviceMock = new Mock<IOrderService>();
            serviceMock
                .Setup(x => x.CreateOrderAsync(It.Is<string>(x => x == email), It.Is<OrderDto>(x => x.Equals(order))))
                .ReturnsAsync((int?)null);
            var controller = new OrderController(serviceMock.Object);

            var result = await controller.CreateByEmail(email, order).ConfigureAwait(false);

            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task CreateByEmail_InvalidOrder_ReturnsBadRequest()
        {
            var email = "test@test.com";
            var serviceMock = Mock.Of<IOrderService>();
            var controller = new OrderController(serviceMock);

            controller.ModelState.AddModelError("order", "Invalid orderDto object");
            var result = await controller.CreateByEmail(email, null).ConfigureAwait(false);

            result.Should().BeOfType<BadRequestResult>();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public async Task CreateByEmail_InvalidEmail_ReturnsBadRequest(string email)
        {
            var order = new OrderDto { DeliveryMethodId = 3, PaymentTypeId = 5, OrderItems = new List<OrderItemDto>() { new OrderItemDto { ProductId = 3, Count = 8 } } };
            var serviceMock = Mock.Of<IOrderService>();
            var controller = new OrderController(serviceMock);

            controller.ModelState.AddModelError("email", "Invalid email");
            var result = await controller.CreateByEmail(email, null).ConfigureAwait(false);

            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task CreateByEmail_ServiceThrowsException_ReturnsBadRequest()
        {
            var email = "test@test.com";
            var order = new OrderDto { DeliveryMethodId = 3, PaymentTypeId = 5, OrderItems = new List<OrderItemDto>() { new OrderItemDto { ProductId = 3, Count = 8 } } };
            var serviceMock = new Mock<IOrderService>();
            serviceMock
                .Setup(x => x.CreateOrderAsync(It.IsAny<int>(), It.IsAny<OrderDto>()))
                .ThrowsAsync(new Exception());
            var controller = new OrderController(serviceMock.Object);

            var result = await controller.CreateByEmail(email, order).ConfigureAwait(false);

            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task CreateByEmail_ValidData_CallsService()
        {
            var email = "test@test.com";
            var order = new OrderDto { DeliveryMethodId = 3, PaymentTypeId = 5, OrderItems = new List<OrderItemDto>() { new OrderItemDto { ProductId = 3, Count = 8 } } };
            var serviceMock = new Mock<IOrderService>();
            serviceMock
                .Setup(x => x.CreateOrderAsync(It.IsAny<int>(), It.IsAny<OrderDto>()))
                .ReturnsAsync(10);
            var controller = new OrderController(serviceMock.Object);

            var result = await controller.CreateByEmail(email, order).ConfigureAwait(false);

            serviceMock.Verify(x => x.CreateOrderAsync(It.IsAny<string>(), It.IsAny<OrderDto>()));
        }
    }
}
