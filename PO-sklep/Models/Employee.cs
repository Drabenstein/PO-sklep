using Dapper.Contrib.Extensions;
using System.ComponentModel.DataAnnotations.Schema;

namespace PO_sklep.Models
{
    public class Employee : IEntity
    {
        [Key]
        [Column("Id_pracownika")]
        public int Id { get; set; }
        [Column("Imie_pracownika")]
        public string Name { get; set; }
        [Column("Nazwisko_pracownika")]
        public string Surname { get; set; }
        [ExplicitKey]
        [Column("Id_stanowiska")]
        public int PositionId { get; set; }
        [ExplicitKey]
        [Column("Id_konta")]
        public int AccountId { get; set; }
        [ExplicitKey]
        [Column("Id_kategorii")]
        public int? CategoryId { get; set; }

        public Category Category { get; set; }
        public Account Account { get; set; }
        public Position Position { get; set; }
    }
}
