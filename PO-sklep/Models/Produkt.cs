using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PO_sklep.Models
{
    public class Produkt : IEntity
    {
        [Key]
        public int IdProduktu { get; set; }
        public string Producent { get; set; }
        public string NazwaProduktu { get; set; }
        public string Opis { get; set; }
        public decimal CenaNetto { get; set; }
        public byte Vat { get; set; }
        [ExplicitKey]
        public int IdKategorii { get; set; }
        public IList<Opinia> Opinie { get; set; }
    }
}
