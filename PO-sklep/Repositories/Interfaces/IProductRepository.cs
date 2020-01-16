using PO_sklep.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PO_sklep.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Produkt>> GetAllAsync();
        Task<IEnumerable<Produkt>> GetByCategoryIdAsync(int categoryId);
        Task<Produkt> GetByIdAsync(int id);
        Task<int> AddProductReviewAsync(int id, string authorEmail, int rating, string comment, bool? isBuyerReview);
    }
}