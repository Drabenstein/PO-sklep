using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PO_sklep.Models
{
    public class OrderStatus : IEntity
    {
        [Key]
        [Column("Id_statusu")]
        public int Id { get; set; }
        [Column("Nazwa_statusu")]
        public string Name { get; set; }

        public IList<Order> Orders { get; set; }
    }
}
