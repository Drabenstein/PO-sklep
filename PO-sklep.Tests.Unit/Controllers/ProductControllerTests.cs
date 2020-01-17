using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PO_sklep.Controllers;
using PO_sklep.DTO;
using PO_sklep.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PO_sklep.Tests.Unit.Controllers
{
    public class ProductControllerTests
    {
        [Fact]
        public async Task GetAll_HavingItems_ReturnsThem()
        {
            var productsDtos = new List<ProductDto> {
                new ProductDto { CategoryId = 1, ImageUrl = "url", Name = "Pralka", Price = 999.99m, ProducerName = "Bosch", ProductId = 8 },
                new ProductDto { CategoryId = 2, ImageUrl = "url2", Name = "Suszarka", Price = 75.50m, ProducerName = "Indesit", ProductId = 7 }
            };
            var serviceMock = new Mock<IProductService>();
            serviceMock.Setup(x => x.GetAllProductsAsync()).ReturnsAsync(productsDtos);
            var controller = new ProductController(serviceMock.Object);

            var results = await controller.GetAll();

            results.Should().BeEquivalentTo(productsDtos);
        }

        [Fact]
        public async Task GetAll_EmptyCollection_ReturnsIt()
        {
            var expected = new List<ProductDto>();
            var serviceMock = new Mock<IProductService>();
            serviceMock.Setup(x => x.GetAllProductsAsync()).ReturnsAsync(expected);
            var controller = new ProductController(serviceMock.Object);

            var results = await controller.GetAll();

            results.Should().HaveCount(0).And.BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetById_PassingId_ReturnsItem()
        {
            var expected = new ProductDto { CategoryId = 1, ImageUrl = "url", Name = "Pralka", Price = 999.99m, ProducerName = "Bosch", ProductId = 10 };
            var serviceMock = new Mock<IProductService>();
            serviceMock.Setup(x => x.GetProductByIdAsync(It.IsAny<int>())).ReturnsAsync(expected);
            var controller = new ProductController(serviceMock.Object);

            var result = await controller.GetById(10);

            result.Value.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetById_PassingSpecificId_ReturnsItemWithSpecifiedId()
        {
            var expected = new ProductDto { CategoryId = 1, ImageUrl = "url", Name = "Pralka", Price = 999.99m, ProducerName = "Bosch", ProductId = 8 };
            var serviceMock = new Mock<IProductService>();
            serviceMock.Setup(x => x.GetProductByIdAsync(It.Is<int>(x => x == 8))).ReturnsAsync(expected);
            serviceMock.Setup(x => x.GetProductByIdAsync(It.IsNotIn<int>(8))).ReturnsAsync((ProductDto)null);
            var controller = new ProductController(serviceMock.Object);

            var result = await controller.GetById(8);

            result.Value.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetById_PassingId_PassesIdToService()
        {
            var serviceMock = new Mock<IProductService>();
            serviceMock.Setup(x => x.GetProductByIdAsync(It.IsAny<int>())).ReturnsAsync((ProductDto)null);
            var controller = new ProductController(serviceMock.Object);

            await controller.GetById(8);

            serviceMock.Verify(x => x.GetProductByIdAsync(It.Is<int>(x => x == 8)), Times.Once());
        }

        [Fact]
        public async Task GetById_PassingNonExistentProductId_ReturnsNotFound()
        {
            var serviceMock = new Mock<IProductService>();
            serviceMock.Setup(x => x.GetProductByIdAsync(It.IsAny<int>())).ReturnsAsync((ProductDto)null);
            var controller = new ProductController(serviceMock.Object);

            var result = await controller.GetById(8);

            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetByCategoryId_PassingExistentId_ReturnsItems()
        {
            var expected = new List<ProductDto> {
                new ProductDto { CategoryId = 3, ImageUrl = "url8", Name = "Pralka", Price = 999.99m, ProducerName = "Bosch", ProductId = 8 },
                new ProductDto { CategoryId = 3, ImageUrl = "url11", Name = "Suszarka", Price = 560.00m, ProducerName = "Amica", ProductId = 11 },
                new ProductDto { CategoryId = 3, ImageUrl = "url7", Name = "Kuchenka", Price = 748.99m, ProducerName = "Indesit", ProductId = 7 }
            };
            var serviceMock = new Mock<IProductService>();
            serviceMock.Setup(x => x.GetProductsByCategoryIdAsync(It.Is<int>(x => x == 3))).ReturnsAsync(expected);
            serviceMock.Setup(x => x.GetProductsByCategoryIdAsync(It.IsNotIn(3))).ReturnsAsync(new List<ProductDto>());
            var controller = new ProductController(serviceMock.Object);

            var result = await controller.GetByCategory(3);

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetByCategoryId_PassingNonExistentId_ReturnsEmptyCollection()
        {
            var expected = new List<ProductDto> {
                new ProductDto { CategoryId = 3, ImageUrl = "url8", Name = "Pralka", Price = 999.99m, ProducerName = "Bosch", ProductId = 8 },
                new ProductDto { CategoryId = 3, ImageUrl = "url11", Name = "Suszarka", Price = 560.00m, ProducerName = "Amica", ProductId = 11 },
                new ProductDto { CategoryId = 3, ImageUrl = "url7", Name = "Kuchenka", Price = 748.99m, ProducerName = "Indesit", ProductId = 7 }
            };
            var serviceMock = new Mock<IProductService>();
            serviceMock.Setup(x => x.GetProductsByCategoryIdAsync(It.Is<int>(x => x == 3))).ReturnsAsync(expected);
            serviceMock.Setup(x => x.GetProductsByCategoryIdAsync(It.IsNotIn(3))).ReturnsAsync(new List<ProductDto>());
            var controller = new ProductController(serviceMock.Object);

            var result = await controller.GetByCategory(4);

            result.Should().BeEmpty();
        }

        [Fact]
        public async Task CreateReview_PassingCorrectData_InvokesServiceWithCorrectParameters()
        {
            const int id = 3;
            var review = new ReviewDto { Author = "marcin@test.com", Rating = 5 };
            var serviceMock = new Mock<IProductService>();
            var controller = new ProductController(serviceMock.Object);

            await controller.CreateReview(id, review);

            serviceMock.Verify(x => x.AddReviewAsync(It.Is<int>(x => x == id), It.Is<ReviewDto>(x => x.Equals(review))));
        }

        [Fact]
        public async Task CreateReview_PassingCorrectData_ReturnsOk()
        {
            var id = 3;
            var review = new ReviewDto { Author = "marcin@test.com", Rating = 5 };
            var serviceMock = new Mock<IProductService>();
            serviceMock.Setup(x => x.AddReviewAsync(It.IsAny<int>(), It.IsNotNull<ReviewDto>())).ReturnsAsync(1);
            var controller = new ProductController(serviceMock.Object);

            var result = await controller.CreateReview(id, review);

            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task CreateReview_ServiceReturnsNull_ReturnsBadRequest()
        {
            var id = 3;
            var review = new ReviewDto { Author = "marcin@test.com", Rating = 5 };
            var serviceMock = new Mock<IProductService>();
            serviceMock.Setup(x => x.AddReviewAsync(It.IsAny<int>(), It.IsNotNull<ReviewDto>())).ReturnsAsync((int?)null);
            var controller = new ProductController(serviceMock.Object);

            var result = await controller.CreateReview(id, review);

            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task CreateReview_IncorrectReviewDto_ReturnsBadRequest()
        {
            var id = 3;
            ReviewDto review = null;
            var controller = new ProductController(Mock.Of<IProductService>());

            var result = await controller.CreateReview(id, review);

            result.Should().BeOfType<BadRequestResult>();
        }
    }
}