using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PO_sklep.Models
{
    public class Order : IEntity
    {
        [Key]
        [Column("Id_zamowienia")]
        public int Id { get; set; }
        [Column("Data_zlozenia")]
        public DateTime OrderDate { get; set; }
        [ExplicitKey]
        [Column("Id_statusu")]
        public int StatusId { get; set; }
        [ExplicitKey]
        [Column("Id_platnosci")]
        public int? PaymentId { get; set; }
        [ExplicitKey]
        [Column("Id_sposobu_dostawy")]
        public int DeliveryMethodId { get; set; }
        [ExplicitKey]
        [Column("Id_klienta")]
        public int ClientId { get; set; }

        public Client Client { get; set; }
        public Payment Payment { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public Invoice Invoice { get; set; }
        public IList<OrderItem> OrderItems { get; set; }
    }
}
