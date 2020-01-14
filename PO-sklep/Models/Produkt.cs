using System;
using System.Collections.Generic;

namespace PO_sklep.Models
{
    public partial class Produkt
    {
        public Produkt()
        {
            Opinia = new HashSet<Opinia>();
            ZamowienieProdukt = new HashSet<ZamowienieProdukt>();
        }

        public int IdProduktu { get; set; }
        public string Producent { get; set; }
        public string NazwaProduktu { get; set; }
        public string Opis { get; set; }
        public decimal CenaNetto { get; set; }
        public byte Vat { get; set; }
        public int IdKategorii { get; set; }

        public virtual Kategoria IdKategoriiNavigation { get; set; }
        public ICollection<Opinia> Opinia { get; set; }
        public virtual ICollection<ZamowienieProdukt> ZamowienieProdukt { get; set; }
    }
}
