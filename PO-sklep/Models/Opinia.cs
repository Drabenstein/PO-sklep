using Dapper.Contrib.Extensions;
using System.ComponentModel.DataAnnotations.Schema;

namespace PO_sklep.Models
{
    public class Opinia : IEntity
    {
        [Key]
        [Column("Id_opinii")]
        public int IdOpinii { get; set; }
        [Column("Ocena")]
        public byte Ocena { get; set; }
        [Column("Komentarz")]
        public string Komentarz { get; set; }
        [Column("Czy_potwierdzona_zakupem")]
        public bool? CzyPotwierdzonaZakupem { get; set; }
        [ExplicitKey]
        [Column("Id_produktu")]
        public int IdProduktu { get; set; }
        [ExplicitKey]
        [Column("Id_klienta")]
        public int? IdKlienta { get; set; }

        public Klient Klient { get; set; }
        public Produkt Produkt { get; set; }
    }
}
