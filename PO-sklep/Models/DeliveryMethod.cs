using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PO_sklep.Models
{
    public class DeliveryMethod : IEntity
    {
        [Key]
        [Column("Id_sposobu_dostawy")]
        public int Id { get; set; }
        [Column("Nazwa_sposobu_dostawy")]
        public string Name { get; set; }
        [Column("Cena_dostawy")]
        public decimal? DeliveryPrice { get; set; }

        public IList<Order> Orders { get; set; }
    }
}
