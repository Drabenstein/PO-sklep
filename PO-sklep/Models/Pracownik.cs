namespace PO_sklep.Models
{
    public class Pracownik : IEntity
    {
        public int IdPracownika { get; set; }
        public string ImiePracownika { get; set; }
        public string NazwiskoPracownika { get; set; }
        public int IdStanowiska { get; set; }
        public int IdKonta { get; set; }
        public int? IdKategorii { get; set; }

        public Kategoria Kategoria { get; set; }
        public Konto Konto { get; set; }
        public Stanowisko Stanowisko { get; set; }
    }
}
