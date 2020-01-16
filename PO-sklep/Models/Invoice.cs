using Dapper.Contrib.Extensions;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PO_sklep.Models
{
    public class Invoice : IEntity
    {
        [Key]
        [Column("Id_faktury")]
        public int Id { get; set; }
        [Column("Seria_faktury")]
        public string InvoiceSeries { get; set; }
        [Column("Data_wystawienia")]
        public DateTime IssueDate { get; set; }
        [ExplicitKey]
        [Column("Id_zamowienia")]
        public int OrderId { get; set; }

        public Order Order { get; set; }
    }
}
