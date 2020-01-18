using AutoMapper;
using PO_sklep.DTO;
using PO_sklep.Extensions;
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

        /// <summary>
        /// Creates a review about product with specified id asynchronously
        /// </summary>
        /// <param name="productId">Id of product under review</param>
        /// <param name="newReview">Review to be saved</param>
        /// <returns>Id of newly added review or null if creation failed</returns>
        public async Task<int?> AddReviewAsync(int productId, ReviewDto newReview)
        {
            if(newReview is null || newReview.Author is null)
            {
                throw new ArgumentNullException(nameof(newReview));
            }

            var product = await _productRepository.GetByIdAsync(productId).ConfigureAwait(false);

            if (product is null)
            {
                return null;
            }

            string email = newReview.Author;
            return await _productRepository.AddProductReviewAsync(productId, email, newReview.Rating, newReview.Comment, false).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets all products asynchronously
        /// </summary>
        /// <returns>All products</returns>
        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync().ConfigureAwait(false);
            return _mapper.Map<IEnumerable<ProductDto>>(products)?.UpdateImageUrls();
        }

        /// <summary>
        /// Gets product with specified id asynchronously
        /// </summary>
        /// <param name="id">Product id</param>
        /// <returns>Found item or null if it's not found</returns>
        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id).ConfigureAwait(false);
            return _mapper.Map<ProductDto>(product)?.UpdateImageUrl();
        }

        /// <summary>
        /// Gets all products belonging to specified category id
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns>Products belonging to provided category</returns>
        public async Task<IEnumerable<ProductDto>> GetProductsByCategoryIdAsync(int categoryId)
        {
            var products = await _productRepository.GetByCategoryIdAsync(categoryId).ConfigureAwait(false);
            return _mapper.Map<IEnumerable<ProductDto>>(products)?.UpdateImageUrls();
        }
    }
}