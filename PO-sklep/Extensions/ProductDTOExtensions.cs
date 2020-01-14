using PO_sklep.DTO;
using System;
using System.Collections.Generic;

namespace PO_sklep.Extensions
{
    public static class ProductDTOExtensions
    {
        private static string _productImagesPath => "https://localhost:5001/images/products";

        public static IEnumerable<ProductDTO> UpdateImageUrls(this IEnumerable<ProductDTO> products)
        {
            if (products is null)
            {
                throw new ArgumentNullException(nameof(products));
            }

            foreach (var product in products)
            {
                product?.UpdateImageUrl();
            }

            return products;
        }

        public static ProductDTO UpdateImageUrl(this ProductDTO product)
        {
            product.ImageUrl = $"{_productImagesPath}/8.jpg";
            return product;
        }
    }
}
