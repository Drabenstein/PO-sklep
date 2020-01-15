using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PO_sklep.Models
{
    public class Produkt : IEntity
    {
        [Key]
        [Column("Id_produktu")]
        public int IdProduktu { get; set; }
        [Column("Producent")]
        public string Producent { get; set; }
        [Column("Nazwa_produktu")]
        public string NazwaProduktu { get; set; }
        [Column("Opis")]
        public string Opis { get; set; }
        [Column("Cena_netto")]
        public decimal CenaNetto { get; set; }
        [Column("VAT")]
        public byte Vat { get; set; }
        [ExplicitKey]
        [Column("Id_kategorii")]
        public int IdKategorii { get; set; }
        public IList<Opinia> Opinie { get; set; }
    }
}