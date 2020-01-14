using System.ComponentModel.DataAnnotations;

namespace PO_sklep.DTO
{
    public class ReviewDTO
    {
        [Required]
        public string Author { get; set; }
        [Required]
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
