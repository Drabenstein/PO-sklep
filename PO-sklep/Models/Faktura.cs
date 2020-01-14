using System;

namespace PO_sklep.Models
{
    public class Faktura : IEntity
    {
        public int IdFaktury { get; set; }
        public string SeriaFaktury { get; set; }
        public DateTime DataWystawienia { get; set; }
        public int IdZamowienia { get; set; }

        public Zamowienie Zamowienie { get; set; }
    }
}
