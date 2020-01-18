using FluentAssertions;
using Moq;
using PO_sklep.DTO;
using PO_sklep.Models;
using PO_sklep.Repositories.Interfaces;
using PO_sklep.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PO_sklep.Tests.Unit.Services
{
    public class ProductServiceTests
    {
        [Fact]
        public async Task GetProductsByCategoryIdAsync_FoundItems_ReturnsItems()
        {
            var products = new List<Product>
            {
                new Product { CategoryId = 2, Id = 6, NetPrice = 538.80m, ProducerName = "Bosch", ProductName = "Pralka", Vat = 23, Reviews = new List<Review>() },
                new Product { CategoryId = 2, Id = 8, NetPrice = 538.80m, ProducerName = "Bosch", ProductName = "Pralka", Vat = 23, Reviews = new List<Review>() },
                new Product { CategoryId = 2, Id = 11, NetPrice = 538.80m, ProducerName = "Bosch", ProductName = "Pralka", Vat = 23, Reviews = new List<Review>() }
            };
            var expectedDtos = new List<ProductDto>
            {
                new ProductDto { CategoryId = 2, ProductId = 6, Price = 662.72m, ProducerName = "Bosch", Name = "Pralka", Reviews = new List<ReviewDto>() },
                new ProductDto { CategoryId = 2, ProductId = 8, Price = 662.72m, ProducerName = "Bosch", Name = "Pralka", Reviews = new List<ReviewDto>() },
                new ProductDto { CategoryId = 2, ProductId = 11, Price = 662.72m, ProducerName = "Bosch", Name = "Pralka", Reviews = new List<ReviewDto>() }
            };
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock
                .Setup(x => x.GetByCategoryIdAsync(It.Is<int>(x => x == 2)))
                .ReturnsAsync(products);
            var service = new ProductService(productRepositoryMock.Object, MapperTestConfig.CreateTestMapper());

            var results = await service.GetProductsByCategoryIdAsync(2).ConfigureAwait(false);

            results.Should().BeEquivalentTo(expectedDtos, opt => opt.Excluding(x => x.ImageUrl));
        }

        [Fact]
        public async Task GetProductsByCategoryIdAsync_NoItems_ReturnsEmpty()
        {
            var products = new List<Product>();
            var expectedDtos = new List<ProductDto>();
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock
                .Setup(x => x.GetByCategoryIdAsync(It.Is<int>(x => x == 2)))
                .ReturnsAsync(products);
            var service = new ProductService(productRepositoryMock.Object, MapperTestConfig.CreateTestMapper());

            var results = await service.GetProductsByCategoryIdAsync(2).ConfigureAwait(false);

            results.Should().BeEquivalentTo(expectedDtos, opt => opt.Excluding(x => x.ImageUrl));
        }

        [Fact]
        public async Task GetProductsByCategoryId_CallsRepository()
        {
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock
                .Setup(x => x.GetByCategoryIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<Product>());
            var service = new ProductService(productRepositoryMock.Object, MapperTestConfig.CreateTestMapper());

            var results = await service.GetProductsByCategoryIdAsync(2).ConfigureAwait(false);

            productRepositoryMock.Verify(x => x.GetByCategoryIdAsync(It.Is<int>(x => x == 2)));
        }

        [Fact]
        public async Task GetProductsByIdAsync_FoundItem_ReturnsItem()
        {
            var product = new Product { CategoryId = 2, Id = 6, NetPrice = 538.80m, ProducerName = "Bosch", ProductName = "Pralka", Vat = 23, Reviews = new List<Review>() };
            var expectedDto = new ProductDto { CategoryId = 2, ProductId = 6, Price = 662.72m, ProducerName = "Bosch", Name = "Pralka", Reviews = new List<ReviewDto>() };
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock
                .Setup(x => x.GetByIdAsync(It.Is<int>(x => x == 2)))
                .ReturnsAsync(product);
            var service = new ProductService(productRepositoryMock.Object, MapperTestConfig.CreateTestMapper());

            var result = await service.GetProductByIdAsync(2).ConfigureAwait(false);

            result.Should().BeEquivalentTo(result, opt => opt.Excluding(x => x.ImageUrl));
        }

        [Fact]
        public async Task GetProductsByIdAsync_ItemNotFound_ReturnsNull()
        {
            Product product = null;
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(product);
            var service = new ProductService(productRepositoryMock.Object, MapperTestConfig.CreateTestMapper());

            var result = await service.GetProductByIdAsync(2).ConfigureAwait(false);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetProductbyIdAsync_CallsRepository()
        {
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock
                .Setup(x => x.GetByIdAsync(It.Is<int>(x => x == 4)))
                .ReturnsAsync(new Product());
            var service = new ProductService(productRepositoryMock.Object, MapperTestConfig.CreateTestMapper());

            var results = await service.GetProductByIdAsync(4).ConfigureAwait(false);

            productRepositoryMock.Verify(x => x.GetByIdAsync(It.Is<int>(x => x == 4)));
        }

        [Fact]
        public async Task GetAllProductsAsync_FoundItems_ReturnsItems()
        {
            var products = new List<Product>
            {
                new Product { CategoryId = 2, Id = 6, NetPrice = 538.80m, ProducerName = "Bosch", ProductName = "Pralka", Vat = 23, Reviews = new List<Review>() },
                new Product { CategoryId = 2, Id = 8, NetPrice = 538.80m, ProducerName = "Bosch", ProductName = "Pralka", Vat = 23, Reviews = new List<Review>() },
                new Product { CategoryId = 2, Id = 11, NetPrice = 538.80m, ProducerName = "Bosch", ProductName = "Pralka", Vat = 23, Reviews = new List<Review>() }
            };
            var expectedDtos = new List<ProductDto>
            {
                new ProductDto { CategoryId = 2, ProductId = 6, Price = 662.72m, ProducerName = "Bosch", Name = "Pralka", Reviews = new List<ReviewDto>() },
                new ProductDto { CategoryId = 2, ProductId = 8, Price = 662.72m, ProducerName = "Bosch", Name = "Pralka", Reviews = new List<ReviewDto>() },
                new ProductDto { CategoryId = 2, ProductId = 11, Price = 662.72m, ProducerName = "Bosch", Name = "Pralka", Reviews = new List<ReviewDto>() }
            };
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(products);
            var service = new ProductService(productRepositoryMock.Object, MapperTestConfig.CreateTestMapper());

            var results = await service.GetAllProductsAsync().ConfigureAwait(false);

            results.Should().BeEquivalentTo(expectedDtos, opt => opt.Excluding(x => x.ImageUrl));
        }

        [Fact]
        public async Task GetAllProductsAsync_NoItems_ReturnsEmpty()
        {
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<Product>());
            var service = new ProductService(productRepositoryMock.Object, MapperTestConfig.CreateTestMapper());

            var results = await service.GetAllProductsAsync().ConfigureAwait(false);

            results.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllProductsAsync_CallsRepository()
        {
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<Product>());
            var service = new ProductService(productRepositoryMock.Object, MapperTestConfig.CreateTestMapper());

            var results = await service.GetAllProductsAsync().ConfigureAwait(false);

            productRepositoryMock.Verify(x => x.GetAllAsync());
        }
    }
}
