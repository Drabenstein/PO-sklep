using System;
using System.Collections.Generic;

namespace PO_sklep.Models
{
    public class Zamowienie : IEntity
    {
        public int IdZamowienia { get; set; }
        public DateTime DataZlozenia { get; set; }
        public int IdStatusu { get; set; }
        public int? IdPlatnosci { get; set; }
        public int IdSposobuDostawy { get; set; }
        public int IdKlienta { get; set; }

        public Klient Klient { get; set; }
        public Platnosc Platnosc { get; set; }
        public SposobDostawy SposobDostawy { get; set; }
        public StatusZamowienia StatusZamowienia { get; set; }
        public Faktura Faktura { get; set; }
        public IList<ZamowienieProdukt> ZamowienieProduktow { get; set; }
    }
}
