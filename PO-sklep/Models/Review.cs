using Dapper.Contrib.Extensions;
using System.ComponentModel.DataAnnotations.Schema;

namespace PO_sklep.Models
{
    public class Review : IEntity
    {
        [Key]
        [Column("Id_opinii")]
        public int ReviewId { get; set; }
        [Column("Ocena")]
        public byte Rating { get; set; }
        [Column("Komentarz")]
        public string Comment { get; set; }
        [Column("Czy_potwierdzona_zakupem")]
        public bool? IsBuyerVerifiedReview { get; set; }
        [ExplicitKey]
        [Column("Id_produktu")]
        public int ProductId { get; set; }
        [ExplicitKey]
        [Column("Id_klienta")]
        public int? ClientId { get; set; }

        public Client Client { get; set; }
        public Product Product { get; set; }
    }
}
