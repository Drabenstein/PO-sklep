using System.ComponentModel.DataAnnotations;

namespace PO_sklep.DTO
{
    public class ReviewDto
    {
        [Required]
        [EmailAddress]
        public string Author { get; set; }
        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
