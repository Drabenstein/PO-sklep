using System.Collections.Generic;

namespace PO_sklep.DTO
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string ImageUrl { get; set; }
        public string ProducerName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<ReviewDTO> Reviews { get; set; }
        public int CategoryId { get; set; }
    }
}
