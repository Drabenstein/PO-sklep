using System.Collections.Generic;

namespace PO_sklep.Models
{
    public class Kategoria : IEntity
    {
        public int IdKategorii { get; set; }
        public string NazwaKategorii { get; set; }
        public int? IdNadkategorii { get; set; }

        public Kategoria Nadkategoria { get; set; }
        public IList<Kategoria> Podkategorie { get; set; }
        public IList<Pracownik> Pracownicy { get; set; }
        public IList<Produkt> Produkty { get; set; }
    }
}
