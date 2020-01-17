using PO_sklep.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PO_sklep.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<IEnumerable<ProductDto>> GetProductsByCategoryIdAsync(int categoryId);
        Task<int?> AddReviewAsync(int productId, ReviewDto newReview);
    }
}
