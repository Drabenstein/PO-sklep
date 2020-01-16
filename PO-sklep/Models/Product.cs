using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PO_sklep.Models
{
    public class Product : IEntity
    {
        [Key]
        [Column("Id_produktu")]
        public int ProductId { get; set; }
        [Column("Producent")]
        public string ProducerName { get; set; }
        [Column("Nazwa_produktu")]
        public string ProductName { get; set; }
        [Column("Opis")]
        public string Description { get; set; }
        [Column("Cena_netto")]
        public decimal NetPrice { get; set; }
        [Column("VAT")]
        public byte Vat { get; set; }
        [ExplicitKey]
        [Column("Id_kategorii")]
        public int CategoryId { get; set; }
        public IList<Review> Reviews { get; set; }
    }
}