using Dapper.Contrib.Extensions;
using System.ComponentModel.DataAnnotations.Schema;

namespace PO_sklep.Models
{
    public class Account : IEntity
    {
        [Key]
        [Column("Id_konta")]
        public int Id { get; set; }
        [Column("Login")]
        public string Login { get; set; }
        [Column("Haslo")]
        public string Password { get; set; }

        public Client Client { get; set; }
        public Employee Employee { get; set; }
    }
}
