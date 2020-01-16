using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PO_sklep.Models
{
    public class PaymentType : IEntity
    {
        [Key]
        [Column("Id_typu_platnosci")]
        public int Id { get; set; }
        [Column("Nazwa_typu_platnosci")]
        public string Name { get; set; }

        public IList<Payment> Payments { get; set; }
    }
}