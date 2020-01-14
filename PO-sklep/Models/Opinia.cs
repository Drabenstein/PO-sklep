namespace PO_sklep.Models
{
    public partial class Opinia
    {
        public int IdOpinii { get; set; }
        public byte Ocena { get; set; }
        public string Komentarz { get; set; }
        public bool? CzyPotwierdzonaZakupem { get; set; }
        public int IdProduktu { get; set; }
        public int? IdKlienta { get; set; }

        public Klient IdKlientaNavigation { get; set; }
        public virtual Produkt IdProduktuNavigation { get; set; }
    }
}
