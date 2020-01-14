using System;
using System.Collections.Generic;

namespace PO_sklep.Models
{
    public partial class Zamowienie
    {
        public Zamowienie()
        {
            Faktura = new HashSet<Faktura>();
            ZamowienieProdukt = new HashSet<ZamowienieProdukt>();
        }

        public int IdZamowienia { get; set; }
        public DateTime DataZlozenia { get; set; }
        public int IdStatusu { get; set; }
        public int? IdPlatnosci { get; set; }
        public int IdSposobuDostawy { get; set; }
        public int IdKlienta { get; set; }

        public virtual Klient IdKlientaNavigation { get; set; }
        public virtual Platnosc IdPlatnosciNavigation { get; set; }
        public virtual SposobDostawy IdSposobuDostawyNavigation { get; set; }
        public virtual StatusZamowienia IdStatusuNavigation { get; set; }
        public virtual ICollection<Faktura> Faktura { get; set; }
        public virtual ICollection<ZamowienieProdukt> ZamowienieProdukt { get; set; }
    }
}
