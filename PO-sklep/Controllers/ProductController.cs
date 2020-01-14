using Microsoft.AspNetCore.Mvc;
using PO_sklep.DTO;
using PO_sklep.Helpers;
using PO_sklep.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        public async Task<IEnumerable<ProductDto>> Get()
        {
            return await _productService.GetAllProductsAsync();
        }

        [HttpGet]
        [ExactQueryParam("id")]
        public async Task<ActionResult<ProductDto>> Get([FromQuery]int id)
        {
            //var foundItem = await _productService.GetProductByIdAsync(id);

            //if (foundItem is null)
            //{
            //    return NotFound();
            //}

            //return foundItem;
            var foundProducts = await _productService.GetAllProductsAsync();
            return foundProducts.First();
        }

        [HttpGet]
        [ExactQueryParam("categoryId")]
        public async Task<IEnumerable<ProductDto>> GetByCategory([FromQuery]int categoryId)
        {
            //var foundProducts = await _productService.GetProductsByCategoryId(categoryId);
            var foundProducts = await _productService.GetAllProductsAsync();
            return foundProducts;
        }

        [HttpPost("{id}/addReview")]
        public async Task<ActionResult> CreateReview(int id, [FromBody]ReviewDto review)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product is null)
            {
                return NotFound();
            }

            await _productService.AddReviewAsync(id, review);
            return Ok();
        }
    }
}
