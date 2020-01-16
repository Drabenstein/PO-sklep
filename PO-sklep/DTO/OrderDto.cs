using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PO_sklep.DTO
{
    public class OrderDto
    {
        public int? PaymentTypeId { get; set; }
        [Required]
        [BindRequired]
        public int DeliveryMethodId { get; set; }
        [Required]
        [BindRequired]
        public IList<OrderItemDto> OrderItems { get; set; }
    }
}