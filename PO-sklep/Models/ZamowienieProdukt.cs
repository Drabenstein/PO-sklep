namespace PO_sklep.Models
{
    public class ZamowienieProdukt : IEntity
    {
        public int IdProduktu { get; set; }
        public int IdZamowienia { get; set; }
        public int? Ilosc { get; set; }

        public Produkt Produkt { get; set; }
        public Zamowienie Zamowienie { get; set; }
    }
}
