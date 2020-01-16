using Dapper.Contrib.Extensions;
using System.ComponentModel.DataAnnotations.Schema;

namespace PO_sklep.Models
{
    public class OrderItem : IEntity
    {
        public int Id { get; set; }

        [ExplicitKey]
        [Column("Id_produktu")]
        public int ProductId { get; set; }
        [ExplicitKey]
        [Column("Id_zamowienia")]
        public int OrderId { get; set; }
        [Column("Ilosc")]
        public int? Count { get; set; }

        public Product Product { get; set; }
        public Order Order { get; set; }
    }
}
