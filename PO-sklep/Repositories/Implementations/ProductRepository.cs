using Dapper;
using PO_sklep.Models;
using PO_sklep.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PO_sklep.Repositories.Implementations
{
    public class ProductRepository : GenericRepositoryBase<Produkt>
    {
        private const string GetAllProductsUsp = "uspGetAllProducts";

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
                    splitOn: "Id_Opinii, Id_Klienta",
                    commandType: System.Data.CommandType.StoredProcedure);
                return products.ToList();
            }).ConfigureAwait(false);
        }

        //public async Task<IEnumerable<Produkt>> GetByCategoryId(int categoryId)
        //{
        //    return await QueryAsync(async db =>
        //    {
        //        var productDictionary = new Dictionary<int, Produkt>();

        //        var products = await db.QueryAsync<Produkt, Opinia, Klient, Produkt>(GetAllProductsUsp,
        //            (product, review, client) => MapProductWithReviews(productDictionary, product, review, client),
        //            splitOn: "Id_Opinii, Id_Klienta",
        //            commandType: System.Data.CommandType.StoredProcedure).ConfigureAwait(false);
        //        return products.ToList();
        //    }).ConfigureAwait(false);
        //}

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
