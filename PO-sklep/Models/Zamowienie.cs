using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PO_sklep.Models
{
    public class Zamowienie : IEntity
    {
        [Key]
        [Column("Id_zamowienia")]
        public int IdZamowienia { get; set; }
        [Column("Data_zlozenia")]
        public DateTime DataZlozenia { get; set; }
        [ExplicitKey]
        [Column("Id_statusu")]
        public int IdStatusu { get; set; }
        [ExplicitKey]
        [Column("Id_platnosci")]
        public int? IdPlatnosci { get; set; }
        [ExplicitKey]
        [Column("Id_sposobu_dostawy")]
        public int IdSposobuDostawy { get; set; }
        [ExplicitKey]
        [Column("Id_klienta")]
        public int IdKlienta { get; set; }

        public Klient Klient { get; set; }
        public Platnosc Platnosc { get; set; }
        public SposobDostawy SposobDostawy { get; set; }
        public StatusZamowienia StatusZamowienia { get; set; }
        public Faktura Faktura { get; set; }
        public IList<ZamowienieProdukt> ZamowienieProduktow { get; set; }
    }
}
