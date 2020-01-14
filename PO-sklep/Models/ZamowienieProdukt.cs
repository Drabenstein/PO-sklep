using System;
using System.Collections.Generic;

namespace PO_sklep.Models
{
    public partial class ZamowienieProdukt
    {
        public int IdProduktu { get; set; }
        public int IdZamowienia { get; set; }
        public int? Ilosc { get; set; }

        public virtual Produkt IdProduktuNavigation { get; set; }
        public virtual Zamowienie IdZamowieniaNavigation { get; set; }
    }
}
