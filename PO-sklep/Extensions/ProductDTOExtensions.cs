using PO_sklep.DTO;
using System;
using System.Collections.Generic;

namespace PO_sklep.Extensions
{
    public static class ProductDtoExtensions
    {
        private static string _productImagesPath => "https://localhost:5001/images/products";
        private static string _productImageExtension => "png";

        public static IEnumerable<ProductDto> UpdateImageUrls(this IEnumerable<ProductDto> products)
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

        public static ProductDto UpdateImageUrl(this ProductDto product)
        {
            if(product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            product.ImageUrl = $"{_productImagesPath}/{product.ProductId}.{_productImageExtension}";
            return product;
        }
    }
}
