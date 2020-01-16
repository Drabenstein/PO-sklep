using Dapper;
using Dapper.Contrib.Extensions;
using PO_sklep.Models;
using PO_sklep.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PO_sklep.Repositories.Implementations
{
    public class ProductRepository : GenericRepositoryBase<Produkt>, IProductRepository
    {
        private const string GetAllProductsUsp = "uspGetAllProducts";
        private const string GetProductsByCategoryUsp = "uspGetProductsByCategory";
        private const string GetProductByIdUsp = "uspGetProductById";
        private const string AddReviewUsp = "uspAddReview";

        public ProductRepository(Helpers.ConnectionConfig connectionConfig) : base(connectionConfig)
        {
        }

        public async Task<IEnumerable<Produkt>> GetAllAsync()
        {
            return await QueryAsync(async db =>
            {
                var productDictionary = new Dictionary<int, Produkt>();

                var products = await db.QueryAsync<Produkt, Opinia, Klient, Produkt>(GetAllProductsUsp,
                    (product, review, client) => MapProductWithReviews(productDictionary, product, review, client),
                    splitOn: "Id_opinii, Id_klienta",
                    commandType: System.Data.CommandType.StoredProcedure);
                return products.ToList();
            }).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Produkt>> GetByCategoryIdAsync(int categoryId)
        {
            return await QueryAsync(async db =>
            {
                var productDictionary = new Dictionary<int, Produkt>();

                var param = new DynamicParameters();
                param.Add("@Id_kategorii", categoryId);
                var products = await db.QueryAsync<Produkt, Opinia, Klient, Produkt>(GetProductsByCategoryUsp,
                    (product, review, client) => MapProductWithReviews(productDictionary, product, review, client),
                    param,
                    splitOn: "Id_opinii, Id_klienta",
                    commandType: System.Data.CommandType.StoredProcedure).ConfigureAwait(false);
                return products.ToList();
            }).ConfigureAwait(false);
        }

        public async Task<Produkt> GetByIdAsync(int id)
        {
            return await QueryAsync(async db =>
            {
                var productDictionary = new Dictionary<int, Produkt>();

                var param = new DynamicParameters();
                param.Add("@Id_produktu", id);
                var products = await db.QueryAsync<Produkt, Opinia, Klient, Produkt>(GetProductByIdUsp,
                    (product, review, client) => MapProductWithReviews(productDictionary, product, review, client),
                    param,
                    splitOn: "Id_opinii, Id_klienta",
                    commandType: System.Data.CommandType.StoredProcedure).ConfigureAwait(false);
                return products.FirstOrDefault();
            }).ConfigureAwait(false);
        }

        public async Task<int> AddProductReviewAsync(int id, string authorEmail, int rating, string comment, bool? isBuyerReview)
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
                    );
            });
        }

        private Produkt MapProductWithReviews(IDictionary<int, Produkt> productDictionary, Produkt product, Opinia review, Klient client)
        {
            if (client?.Email is { })
            {
                review.Klient = client;
            }

            if (!productDictionary.TryGetValue(product.IdProduktu, out var productEntry))
            {
                productEntry = product;
                productEntry.Opinie = new List<Opinia>();
                productDictionary.Add(productEntry.IdProduktu, productEntry);
            }

            if (review?.Klient is { })
            {
                productEntry.Opinie.Add(review);
            }
            return productEntry;
        }
    }
}
