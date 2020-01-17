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

        [HttpGet]
        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            return await _productService.GetAllProductsAsync();
        }

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

        [HttpGet]
        [ExactQueryParam("categoryId")]
        public async Task<IEnumerable<ProductDto>> GetByCategory([FromQuery]int categoryId)
        {
            var foundProducts = await _productService.GetProductsByCategoryIdAsync(categoryId);
            return foundProducts;
        }

        [HttpPost("{id}/addReview")]
        public async Task<ActionResult> CreateReview(int id, [FromBody]ReviewDto review)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var newReviewId = await _productService.AddReviewAsync(id, review);

            if (newReviewId is null)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}