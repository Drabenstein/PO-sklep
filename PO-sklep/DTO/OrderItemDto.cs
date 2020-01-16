using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace PO_sklep.DTO
{
    public class OrderItemDto
    {
        [Required]
        [BindRequired]
        public int ProductId { get; set; }
        [Required]
        [BindRequired]
        public int Count { get; set; }
    }
}