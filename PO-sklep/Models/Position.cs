using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PO_sklep.Models
{
    public class Position : IEntity
    {
        [Key]
        [Column("Id_stanowiska")]
        public int Id { get; set; }
        [Column("Nazwa_stanowiska")]
        public string Name { get; set; }

        public IList<Employee> Employees { get; set; }
    }
}
