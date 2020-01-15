using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PO_sklep.Models
{
    public class Klient : IEntity
    {
        [Key]
        [Column("Id_klienta")]
        public int IdKlienta { get; set; }
        [Column("Imie_klienta")]
        public string ImieKlienta { get; set; }
        [Column("Nazwisko_klienta")]
        public string NazwiskoKlienta { get; set; }
        [Column("Email")]
        public string Email { get; set; }
        [Column("Adres")]
        public string Adres { get; set; }
        [Column("Data_urodzenia")]
        public DateTime? DataUrodzenia { get; set; }
        [ExplicitKey]
        [Column("Id_konta")]
        public int? IdKonta { get; set; }

        public Konto Konto { get; set; }
        public IList<Opinia> Opinie { get; set; }
        public IList<Zamowienie> Zamowienia { get; set; }
    }
}
