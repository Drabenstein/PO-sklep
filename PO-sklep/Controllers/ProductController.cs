using Microsoft.AspNetCore.Mvc;
using PO_sklep.DTO;
using PO_sklep.Helpers;
using PO_sklep.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace PO_sklep.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        /// <summary>
        /// Gets all products asynchronously
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            return await _productService.GetAllProductsAsync();
        }

        /// <summary>
        /// Gets product by specified id asynchronously
        /// </summary>
        /// <param name="id">Product id</param>
        /// <returns>Item or NotFound</returns>
        [HttpGet]
        [ExactQueryParam("id")]
        public async Task<ActionResult<ProductDto>> GetById([FromQuery]int id)
        {
            var foundItem = await _productService.GetProductByIdAsync(id);

            if (foundItem is null)
            {
                return NotFound();
            }

            return foundItem;
        }

        /// <summary>
        /// Gets products belonging to specific category asynchronously
        /// </summary>
        /// <param name="categoryId">Category id</param>
        /// <returns></returns>
        [HttpGet]
        [ExactQueryParam("categoryId")]
        public async Task<IEnumerable<ProductDto>> GetByCategory([FromQuery]int categoryId)
        {
            var foundProducts = await _productService.GetProductsByCategoryIdAsync(categoryId);
            return foundProducts;
        }

        /// <summary>
        /// Creates new review asynchronously
        /// </summary>
        /// <param name="id">Product id</param>
        /// <param name="review">Review data</param>
        /// <returns>Ok if review is added or BadRequest if there is an error</returns>
        [HttpPost("{id}/addReview")]
        public async Task<ActionResult> CreateReview(int id, [FromBody]ReviewDto review)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var newReviewId = await _productService.AddReviewAsync(id, review);

                if (newReviewId is null)
                {
                    return BadRequest();
                }

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}