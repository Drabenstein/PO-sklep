using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PO_sklep.Models
{
    public class Klient : IEntity
    {
        [Key]
        public int IdKlienta { get; set; }
        public string ImieKlienta { get; set; }
        public string NazwiskoKlienta { get; set; }
        public string Email { get; set; }
        public string Adres { get; set; }
        public DateTime? DataUrodzenia { get; set; }
        [ExplicitKey]
        public int? IdKonta { get; set; }

        public Konto Konto { get; set; }
        public IList<Opinia> Opinie { get; set; }
        public IList<Zamowienie> Zamowienia { get; set; }
    }
}
