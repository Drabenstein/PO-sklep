using Dapper.Contrib.Extensions;
using System.ComponentModel.DataAnnotations.Schema;

namespace PO_sklep.Models
{
    public class Opinia : IEntity
    {
        [Key]
        public int IdOpinii { get; set; }
        public byte Ocena { get; set; }
        public string Komentarz { get; set; }
        public bool? CzyPotwierdzonaZakupem { get; set; }
        [ExplicitKey]
        public int IdProduktu { get; set; }
        [ExplicitKey]
        public int? IdKlienta { get; set; }

        public Klient Klient { get; set; }
        public Produkt Produkt { get; set; }
    }
}
