using AutoMapper;
using PO_sklep.DTO;
using PO_sklep.Extensions;
using PO_sklep.Repositories.Implementations;
using PO_sklep.Repositories.Interfaces;
using PO_sklep.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PO_sklep.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<int?> AddReviewAsync(int productId, ReviewDto newReview)
        {
            var product = await _productRepository.GetByIdAsync(productId);

            if (product is null)
            {
                return null;
            }

            string email = newReview.Author;
            return await _productRepository.AddProductReviewAsync(productId, email, newReview.Rating, newReview.Comment, false);
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync().ConfigureAwait(false);
            return _mapper.Map<IEnumerable<ProductDto>>(products)?.UpdateImageUrls();
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id).ConfigureAwait(false);
            return _mapper.Map<ProductDto>(product).UpdateImageUrl();
        }

        public async Task<IEnumerable<ProductDto>> GetProductsByCategoryId(int categoryId)
        {
            var products = await _productRepository.GetByCategoryIdAsync(categoryId).ConfigureAwait(false);
            return _mapper.Map<IEnumerable<ProductDto>>(products)?.UpdateImageUrls();
        }
    }
}