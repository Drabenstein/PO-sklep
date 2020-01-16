using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PO_sklep.Models
{
    public class Client : IEntity
    {
        [Key]
        [Column("Id_klienta")]
        public int Id { get; set; }
        [Column("Imie_klienta")]
        public string Name { get; set; }
        [Column("Nazwisko_klienta")]
        public string Surname { get; set; }
        [Column("Email")]
        public string Email { get; set; }
        [Column("Adres")]
        public string Address { get; set; }
        [Column("Data_urodzenia")]
        public DateTime? DateOfBirth { get; set; }
        [ExplicitKey]
        [Column("Id_konta")]
        public int? AccountId { get; set; }

        public Account Account { get; set; }
        public IList<Review> Reviews { get; set; }
        public IList<Order> Orders { get; set; }
    }
}
