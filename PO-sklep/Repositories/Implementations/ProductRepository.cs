using Dapper;
using PO_sklep.Models;
using PO_sklep.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PO_sklep.Repositories.Implementations
{
    public class ProductRepository : GenericRepositoryBase<Product>, IProductRepository
    {
        private const string GetAllProductsUsp = "uspGetAllProducts";
        private const string GetProductsByCategoryUsp = "uspGetProductsByCategory";
        private const string GetProductByIdUsp = "uspGetProductById";
        private const string AddReviewUsp = "uspAddReview";

        public ProductRepository(Helpers.ConnectionConfig connectionConfig) : base(connectionConfig)
        {
        }

        /// <summary>
        /// Gets all products asynchronously
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await QueryAsync(async db =>
            {
                var productDictionary = new Dictionary<int, Product>();

                var products = await db.QueryAsync<Product, Review, Client, Product>(GetAllProductsUsp,
                    (product, review, client) => MapProductWithReviews(productDictionary, product, review, client),
                    splitOn: "Id_opinii, Id_klienta",
                    commandType: System.Data.CommandType.StoredProcedure).ConfigureAwait(false);
                return products.Distinct().ToList();
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Get all products belonging to specified category id asynchronously
        /// </summary>
        /// <param name="categoryId">Product category id</param>
        /// <returns></returns>
        public async Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId)
        {
            return await QueryAsync(async db =>
            {
                var productDictionary = new Dictionary<int, Product>();

                var param = new DynamicParameters();
                param.Add("@Id_kategorii", categoryId);
                var products = await db.QueryAsync<Product, Review, Client, Product>(GetProductsByCategoryUsp,
                    (product, review, client) => MapProductWithReviews(productDictionary, product, review, client),
                    param,
                    splitOn: "Id_opinii, Id_klienta",
                    commandType: System.Data.CommandType.StoredProcedure).ConfigureAwait(false);
                return products.Distinct().ToList();
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets product by it's id asynchronously
        /// </summary>
        /// <param name="id">Product id</param>
        /// <returns></returns>
        public async Task<Product> GetByIdAsync(int id)
        {
            return await QueryAsync(async db =>
            {
                var productDictionary = new Dictionary<int, Product>();

                var param = new DynamicParameters();
                param.Add("@Id_produktu", id);
                var products = await db.QueryAsync<Product, Review, Client, Product>(GetProductByIdUsp,
                    (product, review, client) => MapProductWithReviews(productDictionary, product, review, client),
                    param,
                    splitOn: "Id_opinii, Id_klienta",
                    commandType: System.Data.CommandType.StoredProcedure).ConfigureAwait(false);
                return products.FirstOrDefault();
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Adds product review asynchronously
        /// </summary>
        /// <param name="id">Product's id</param>
        /// <param name="authorEmail">Review author's email</param>
        /// <param name="rating">Product's rating</param>
        /// <param name="comment">Review's comment</param>
        /// <param name="isBuyerReview">Flag describing if review is verified (default: null)</param>
        /// <returns>Id of newly created review</returns>
        public async Task<int> AddProductReviewAsync(int id, string authorEmail, int rating, string comment, bool? isBuyerReview = null)
        {
            var param = new DynamicParameters();
            param.Add("@Id_produktu", id);
            param.Add("@Ocena", rating);
            param.Add("@Email", authorEmail);

            if (!string.IsNullOrWhiteSpace(comment))
            {
                param.Add("@Komentarz", comment);
            }

            if (isBuyerReview is { })
            {
                param.Add("@Czy_potwierdzona_zakupem", isBuyerReview);
            }

            return await QueryAsync(async db =>
            {
                return await db.ExecuteScalarAsync<int>(AddReviewUsp,
                    param,
                    commandType: System.Data.CommandType.StoredProcedure
                    ).ConfigureAwait(false);
            }).ConfigureAwait(false);
        }

        private Product MapProductWithReviews(IDictionary<int, Product> productDictionary, Product product, Review review, Client client)
        {
            if (client?.Email is { })
            {
                review.Client = client;
            }

            if (!productDictionary.TryGetValue(product.Id, out var productEntry))
            {
                productEntry = product;
                productEntry.Reviews = new List<Review>();
                productDictionary.Add(productEntry.Id, productEntry);
            }

            if (review?.Client is { })
            {
                productEntry.Reviews.Add(review);
            }
            return productEntry;
        }
    }
}
