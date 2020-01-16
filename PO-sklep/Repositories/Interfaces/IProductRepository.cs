using PO_sklep.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PO_sklep.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId);
        Task<Product> GetByIdAsync(int id);
        Task<int> AddProductReviewAsync(int id, string authorEmail, int rating, string comment, bool? isBuyerReview);
    }
}