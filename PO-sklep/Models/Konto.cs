namespace PO_sklep.Models
{
    public class Konto : IEntity
    {

        public int IdKonta { get; set; }
        public string Login { get; set; }
        public string Haslo { get; set; }

        public Klient Klient { get; set; }
        public Pracownik Pracownik { get; set; }
    }
}
