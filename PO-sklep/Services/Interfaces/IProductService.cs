using PO_sklep.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PO_sklep.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
        Task<ProductDTO> GetProductByIdAsync(int id);
        Task<IEnumerable<ProductDTO>> GetProductsByCategoryId(int categoryId);
        Task AddReviewAsync(int productId, ReviewDTO newReview);
    }
}
