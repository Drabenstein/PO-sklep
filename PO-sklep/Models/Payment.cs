using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PO_sklep.Models
{
    public class Payment : IEntity
    {
        [Key]
        [Column("Id_platnosci")]
        public int Id { get; set; }
        [Column("Data_platnosci")]
        public DateTime PaymentDate { get; set; }
        [ExplicitKey]
        [Column("Id_typu_platnosci")]
        public int PaymentTypeId { get; set; }

        public PaymentType PaymentType { get; set; }
        public IList<Order> Orders { get; set; }
    }
}
